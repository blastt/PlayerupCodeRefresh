﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Models.Offer
{
    public class OfferViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Основная игра")]
        public string Game { get; set; }

        [Display(Name = "Заголовок")]
        public string Header { get; set; }

        public string UserName { get; set; }

        [Display(Name = "Описание")]
        public string Discription { get; set; }

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

        public string ShortUrl { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Платите ли вы за гаранта?")]
        public bool SellerPaysMiddleman { get; set; }

        public string UserAvatar32 { get; set; }
        public string UserAvatar64 { get; set; }
        public string UserAvatar96 { get; set; }
    }
}