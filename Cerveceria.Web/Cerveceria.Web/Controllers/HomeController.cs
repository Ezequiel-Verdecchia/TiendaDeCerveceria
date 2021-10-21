using Cerveceria.Web.DAO.Models;
using Cerveceria.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Cerveceria.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration _config;
        private cerveceriaContext _context;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, cerveceriaContext context)
        {
            _logger = logger;
            this._context = context;
            this._config = configuration;

        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListarProductos()
        {
            ProductosViewModel model = new ProductosViewModel();

            model.listadoProductos = this._context.Productos.ToList();

            return View(model);
        }
        [Route("AgregarAlCarrito")]
        public IActionResult AgregarAlCarrito(int id)
        {
            CarritoViewModel model = new CarritoViewModel();
            try
            {
                List<ProductosCarrito> listadoProductosCarrito = Helper.SessionExtensions.GetObject<List<ProductosCarrito>>(HttpContext.Session, "Carrito") ?? new List<ProductosCarrito>();

                var listadoProductos = this._context.Productos.ToList();
                var productoToAdd = listadoProductos.Where(a => a.Id.Equals(id)).FirstOrDefault();
                if (productoToAdd != null)
                {
                    var productosCarritoEncontrado = listadoProductosCarrito.Where(a => a.Id.Equals(productoToAdd.Id)).FirstOrDefault();
                    if (productosCarritoEncontrado != null)
                    {
                        productosCarritoEncontrado.Cantidad++;
                        productosCarritoEncontrado.SubTotal = productosCarritoEncontrado.Cantidad * productoToAdd.Precio;
                    }
                    else
                    {
                        ProductosCarrito productosCarrito = new ProductosCarrito();
                        productosCarrito.Id = productoToAdd.Id;
                        productosCarrito.Cantidad = 1;
                        productosCarrito.SubTotal = productoToAdd.Precio;
                        listadoProductosCarrito.Add(productosCarrito);
                    }
                    Helper.SessionExtensions.SetObject(HttpContext.Session, "Carrito", listadoProductosCarrito);

                    foreach (var productosCarrito in listadoProductosCarrito)
                    {
                        var producto = listadoProductos.Where(a => a.Id.Equals(productosCarrito.Id)).FirstOrDefault();
                        productosCarrito.Imagen = producto.Imagen;
                        productosCarrito.Medida = producto.Medida;
                        productosCarrito.Precio = producto.Precio;
                        productosCarrito.Stock = producto.Stock;
                        productosCarrito.Tipo = producto.Tipo;
                        productosCarrito.Descripcion = producto.Descripcion;
                    }
                    model.productosCarritos = listadoProductosCarrito;

                }
                else
                {
                    return View("ListarProductos", new ProductosViewModel());

                }

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

            return VerCarrito(model);
        }
        [Route("VerCarrito")]
        public IActionResult VerCarrito(CarritoViewModel model)
        {
            if (model.productosCarritos != null)
            {
                return View("Carrito", model);
            }

            model = new CarritoViewModel();

            try
            {
                List<ProductosCarrito> listadoProductosCarrito = Helper.SessionExtensions.GetObject<List<ProductosCarrito>>(HttpContext.Session, "Carrito") ?? new List<ProductosCarrito>();

                var listadoProductos = this._context.Productos.ToList();

                foreach (var productosCarrito in listadoProductosCarrito)
                {
                    var producto = listadoProductos.Where(a => a.Id.Equals(productosCarrito.Id)).FirstOrDefault();
                    productosCarrito.Imagen = producto.Imagen;
                    productosCarrito.Medida = producto.Medida;
                    productosCarrito.Precio = producto.Precio;
                    productosCarrito.Stock = producto.Stock;
                    productosCarrito.Tipo = producto.Tipo;
                    productosCarrito.Descripcion = producto.Descripcion;
                }
                model.productosCarritos = listadoProductosCarrito;

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Carrito", model);
        }
        public IActionResult RemoverProductoCarrito(int id)
        {
            CarritoViewModel model = new CarritoViewModel();
            try
            {
                List<ProductosCarrito> listadoProductosCarrito = Helper.SessionExtensions.GetObject<List<ProductosCarrito>>(HttpContext.Session, "Carrito") ?? new List<ProductosCarrito>();

                if (listadoProductosCarrito.Any(a=>a.Id.Equals(id)))
                {
                    var productoToRemove = listadoProductosCarrito.Where(a => a.Id.Equals(id)).FirstOrDefault();
                    listadoProductosCarrito.Remove(productoToRemove);
                    Helper.SessionExtensions.SetObject(HttpContext.Session, "Carrito", listadoProductosCarrito);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

            return VerCarrito(model);
        }

        [Route("DetalleProducto")]
        public IActionResult DetallarProducto(int id)
        {
            var producto = this._context.Productos.Where(a => a.Id.Equals(id)).FirstOrDefault();

            var model = new ProductosViewModel();

            model.RutaImagen = producto.Imagen;
            model.Nombre = producto.Descripcion;
            model.Precio = producto.Precio;
            if (producto.Stock > 0)
            {
                model.StockInformacion = "Disponible";
            }
            model.Tipo = producto.Tipo;
            model.Medida = producto.Medida;
            model.IdProducto = producto.Id;

            return View(model);

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
