﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPuntoDeVentas.Areas.Users.Models
{
    public class InputModelRegistro
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo apellido es obligatorio.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo nid es obligatorio.")]
        public string NID { get; set; }

        [Required(ErrorMessage = "El campo telefono es obligatorio.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",ErrorMessage="El formato telefono ingresado no es valido.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "El campo correo electronico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo correo electronico no es una dirección de correo electrónico válido")]
        public string Email       { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo contraseña es obligatorio.")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos{2}.", MinimumLength = 6)]
        public string Password    { get; set; }

        [Required(ErrorMessage = "Seleccione un rol.")]
        public string Role               { get; set; }
        public string ID                 { get; set; }
        public int Id                    { get; set; }
        public byte[] Image              { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
