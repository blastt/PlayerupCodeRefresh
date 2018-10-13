using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Models.AccountInfo
{
    public class AccountInfoViewModel
    {
        [Required]
        public string AccountLogin { get; set; }

        [Required]
        public string AccountPassword { get; set; }

        [Required]
        [Compare("SteamPassword", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmSteamPassword { get; set; }
        public string AccountEmail { get; set; }

        public string EmailPassword { get; set; }
        [Compare("EmailPassword", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmEmailPassword { get; set; }

        public string AdditionalInformation { get; set; }
        [Required]
        public int ModeratorId { get; set; }
        [Required]
        public int BuyerId { get; set; }
        [Required]
        public int SellerId { get; set; }

        public int OrderId { get; set; }
    }
}