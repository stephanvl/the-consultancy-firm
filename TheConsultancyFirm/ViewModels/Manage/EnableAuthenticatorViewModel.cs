﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TheConsultancyFirm.ViewModels.Manage
{
    public class EnableAuthenticatorViewModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "Het {0} moet ten minste {2} en maximaal {1} karakters lang zijn.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Verificatie Code")]
        public string Code { get; set; }

        [ReadOnly(true)]
        public string SharedKey { get; set; }

        public string AuthenticatorUri { get; set; }
    }
}
