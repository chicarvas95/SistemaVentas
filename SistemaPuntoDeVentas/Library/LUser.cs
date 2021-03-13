using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaPuntoDeVentas.Areas.Users.Models;
using SistemaPuntoDeVentas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPuntoDeVentas.Library
{
    public class LUser: ListObject
    {
        public LUser(UserManager<IdentityUser>   userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole>   roleManager,ApplicationDbContext        context)
        {
            _userManager    = userManager;
            _roleManager    = roleManager;
            _signInManager  = signInManager;
            _context        = context;
            _userRoles = new LUsersRoles();
        }

        public async Task<List<InputModelRegistro>> getTUsuariosAsync(String valor, int id)
        {
            List<TUsers>          listUser;
            List<SelectListItem> _listRoles;
            List<InputModelRegistro> userList = new List<InputModelRegistro>();
            if (valor == null && id.Equals(0))
            {
                listUser = _context.TUsers.ToList();
                
            }
            else
            {
                if (id.Equals(0))
                {
                    listUser = _context.TUsers.Where(u => u.NID.StartsWith(valor) || u.Name.StartsWith(valor) || u.LastName.StartsWith(valor) || u.Email.StartsWith(valor)).ToList();
                }
                else
                {
                    listUser = _context.TUsers.Where(u => u.ID.Equals(id)).ToList();
                } 
            }
            if (!listUser.Count.Equals(0))
            {
                foreach (var item in listUser)
                {
                    _listRoles = await _userRoles.getRole(_userManager, _roleManager, item.IdUser);
                    var user = _context.Users.Where(u => u.Id.Equals(item.IdUser)).ToList().Last();
                    userList.Add(new InputModelRegistro
                    {
                        Id = item.ID,
                        ID = item.IdUser,
                        NID = item.NID,
                        Name = item.Name,
                        LastName = item.LastName,
                        Email = item.Email,
                        Role = _listRoles[0].Text,
                        Image = item.Image,
                        IdentityUser = user
                    });
                    _listRoles.Clear();
                }
            }
            return userList;
        }
    }
}
