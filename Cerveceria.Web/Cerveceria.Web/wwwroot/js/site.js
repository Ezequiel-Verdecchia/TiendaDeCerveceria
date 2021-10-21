// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var MenuItems = document.getElementById("MenuItems");
MenuItems.style.maxHeight == "0px";
        function menuToggle(){
        if(MenuItems.style.maxHeight == "0px"){
    MenuItems.style.maxHeight = "200px";
            }
        else{
    MenuItems.style.maxHeight = "0px";
            }
        }
