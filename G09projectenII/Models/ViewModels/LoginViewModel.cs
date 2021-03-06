﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace G09projectenII.Models.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Gebruikersnaam")]
        [Required(ErrorMessage = "Gebruikersnaam is verplicht in te vullen!")]
        public string Username { get; set; }

        [Display(Name = "Paswoord")]
        [Required(ErrorMessage = "Paswoord is verplicht in te vullen!")]
        [HiddenInput]
        public string Password { get; set; }

        [Display(Name = "Herinner mij")]
        public bool RememberMe { get; set; }
    }
}
