using Cerveceria.Web.BLL;
using Cerveceria.Web.DAO.Models;
using Cerveceria.Web.Helper;
using Cerveceria.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cerveceria.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly cerveceriaContext _dbContext;
        private readonly MailManager _mailManager;

        public LoginController(cerveceriaContext dbcontext)
        {
            this._dbContext = dbcontext;
            this._mailManager = new MailManager();
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index","home");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            if (Autenticar(model.Username, model.Password))
            {
                var user = _dbContext.Usuarios
                    .Where(a => a.Mail.Equals(model.Username)).FirstOrDefault();

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Sid);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Nombre));
                identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id));
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                    new AuthenticationProperties { ExpiresUtc = DateTime.Now.AddDays(1), IsPersistent = true });

                return RedirectToAction("Index", "Home");

            }
            else
            {
                ModelState.AddModelError("login", "El 'Usuario' o 'Contraseña' es incorrecto.");

                return View();
            }
        }
        [HttpGet]
        public IActionResult Registrar()
        {
            RegistroViewModel model = new RegistroViewModel();

            return View(model);
        }
        [HttpPost]
        public IActionResult Registrar(RegistroViewModel modelRegistro)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return View("Login");
            //}
            if (!BuscarUsuario(modelRegistro.Email))
            {
                string token = Helper.RandomHelper.Random();
                Usuario usuario = GetUsuario(modelRegistro);

                HttpContext.Session.SetInt32("Contador", 0);
                HttpContext.Session.SetString("TokenRegistro",token);
                Helper.SessionExtensions.SetObject(HttpContext.Session, "NuevoUsuario", usuario);

                var asunto = "Verificación de cuenta";
                var body = "Estimado, ingrese al siguiente codigo para verificar su cuenta: " + token;
                this._mailManager.SendMail(modelRegistro.Email, asunto, body);

            }
            else
            {
                ModelState.AddModelError("registro", "El email ya se encuentra en la base, ingresá otro email.");
                return View(modelRegistro);
            }

            return RedirectToAction("ConfirmarCuenta","Login");
        }

        public IActionResult ConfirmarCuenta()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ConfirmacionCuentaViewModel model = new ConfirmacionCuentaViewModel();

            return View(model);
        }
        [HttpPost]
        public IActionResult ConfirmarCuenta(ConfirmacionCuentaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var token = HttpContext.Session.GetString("TokenRegistro");

            if (!model.TokenRegistro.Equals(token))
            {
                int contador = HttpContext.Session.GetInt32("Contador") != null? (int)HttpContext.Session.GetInt32("Contador") : 0;
               
                HttpContext.Session.SetInt32("Contador", ++contador);

                if (contador > 3)
                {
                    HttpContext.Session.Remove("TokenRegistro");
                    ModelState.AddModelError("verificacionIncorrecta", "Se han superado los 3 intentos por favor comuníquese con el administrador.");
                    return View(model);
                }
               
                ModelState.AddModelError("verificacionIncorrecta", "El código ingresado no es válido.");
                return View(model);
            }
            else
            {
                
                Usuario usuario = Helper.SessionExtensions.GetObject<Usuario>(HttpContext.Session, "NuevoUsuario");

                if (usuario != null)
                {
                    this._dbContext.Add(usuario);
                    this._dbContext.SaveChanges();
                }

            }
            
            return RedirectToAction("Index","Home"); 
        }


        public IActionResult RecuperarContraseña() 
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public IActionResult RecuperarContraseña(RecuperarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
           
            if (BuscarUsuario(model.Email))
            {
                var newPass = RandomHelper.Random();

                var asunto = "Recupero de Contraseña";
                var body = "Estimado, " + Environment.NewLine +
                    "se realizó correctamente el cambio de contraseña, su nueva contraseña es: " + newPass;
                this._mailManager.SendMail(model.Email, asunto, body);

                var user = this._dbContext.Usuarios.Where(a => a.Mail.Equals(model.Email)).FirstOrDefault();
                user.Password = GetContraseña(model.Email, newPass);

                this._dbContext.SaveChanges();

                ModelState.AddModelError("registroExitoso", "Se envío el mail correctamente, verifique su casilla.");
            }
            else
            {
                ModelState.AddModelError("recuperar", "El Email no está en la base de datos.");
            }

            return View();
            
        }

        public IActionResult CambiarContraseña()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("Login");
            }
            return View();
        }
        [HttpPost]
        public IActionResult CambiarContraseña(CambiarContraseñaViewModel modelCambiar)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!User.Identity.IsAuthenticated)
            {
                return View("Login");
            }
            
            var sid = SessionHelper.GetSid(User);
            var user = this._dbContext.Usuarios.Where(a=>a.Id.Equals(sid)).FirstOrDefault();

            user.Password = GetContraseña(user.Mail,modelCambiar.ConfirmarPassword);

            this._dbContext.SaveChanges();

            ModelState.AddModelError("cambiarExitoso", "Se cambió la contraseña correctamente");
        

            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index","Login");
        }

        public bool Autenticar(string usuario, string pass)
        {
            var passHashed = GetContraseña(usuario, pass);

            var isAutenthic = _dbContext.Usuarios
                .Any(a => a.Mail.Equals(usuario) &&
                    a.Password.Equals(passHashed));

            return isAutenthic;
        }

        public bool BuscarUsuario(string usuario)
        {
            var isInDB = _dbContext.Usuarios
                .Any(a => a.Mail.Equals(usuario));

            return isInDB;
        }
        public string GetContraseña(string usuario, string pass)
        {
            var hash = new HashHelper();
            var passHashed = hash.ConvertToHash(String.Concat(usuario, pass));

            return passHashed;
        }
        private Usuario GetUsuario(RegistroViewModel modelRegistro)
        {
            Usuario usuario = new Usuario();
            usuario.Id = Guid.NewGuid().ToString();
            usuario.Nombre = modelRegistro.Nombre;
            usuario.Apellido = modelRegistro.Apellido;
            usuario.Mail = modelRegistro.Email;
            usuario.Password = GetContraseña(modelRegistro.Email, modelRegistro.ConfirmarPassword); ;
            usuario.Rol = nameof(SystemEnums.Roles.Cliente);

            return usuario;
        }
    }
}
