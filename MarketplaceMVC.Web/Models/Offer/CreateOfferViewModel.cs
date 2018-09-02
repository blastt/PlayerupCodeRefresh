using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceMVC.Web.Models.Offer
{
    public class CreateOfferViewModel
    {
        [Required]
        [Display(Name = "Основная игра")]
        public string Game { get; set; }

        public List<SelectListItem> Games { get; set; } = new List<SelectListItem>();

        [Required]
        [Display(Name = "Заголовок")]
        public string Header { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Discription { get; set; }

        [Required]
        [Display(Name = "Логин продаваемого аккаунта")]
        public string AccountLogin { get; set; }

        [Required]
        [Display(Name = "Являестся ли аккаунт вашим основным (личный)?")]
        public bool PersonalAccount { get; set; }

        [Display(Name = "Колличество игр *")]
        public int? CountOfGames { get; set; }

        [Display(Name = "Дата регистрации аккаунта *")]
        public DateTime? CreatedAccountDate { get; set; }

        [Display(Name = "Есль ли бан на аккаунте? *")]
        public bool IsBanned { get; set; }

        [Display(Name = "Ссылка на аккаунт *")]
        public string Url { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Платите ли вы за гаранта?")]
        public bool SellerPaysMiddleman { get; set; }
    }
}