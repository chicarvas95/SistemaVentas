using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaPuntoDeVentas.Areas.Users.Models;
using SistemaPuntoDeVentas.Data;
using SistemaPuntoDeVentas.Library;
using SistemaPuntoDeVentas.Models;

namespace SistemaPuntoDeVentas.Areas.Users.Controllers
{
    [Area("Users")]

    public class UsersController : Controller
    {

        private SignInManager<IdentityUser> _signInManager;
        private LUser _user;
        private static DataPaginador<InputModelRegistro> models;

        //Metodo constructor
        public UsersController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager,RoleManager<IdentityRole> roleManager,ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _user = new LUser(userManager, signInManager, roleManager, context);
        }
        public IActionResult Users(int id, String filtrar)
        {
            //if (_signInManager.IsSignedIn(User))
            //{
                Object[] objects = new Object[3];
                var data = _user.getTUsuariosAsync(filtrar, 0);
                if (0 < data.Result.Count)
                {
                    var url = Request.Scheme + "://" + Request.Host.Value;
                    objects = new LPaginador<InputModelRegistro>().paginador(data.Result, id, 10, "", "Users", "Users", url);
                }
                else
                {
                    objects[0] = "No hay datos que mostrar";
                    objects[1] = "No hay datos que mostrar";
                    objects[2] = new List<InputModelRegistro>();
                }
                models = new DataPaginador<InputModelRegistro>
                {
                    List            = (List<InputModelRegistro>)objects[2],
                    Pagi_info       = (String)objects[0],
                    Pagi_navegacion = (String)objects[1],
                    Input           = new InputModelRegistro(),
                };
                return View(models);
            //}
            //else
            //{
            //    return Redirect("/");
            //}
        }
    }
}
