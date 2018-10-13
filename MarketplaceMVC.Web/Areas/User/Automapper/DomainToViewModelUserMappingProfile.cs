using AutoMapper;
using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Web.Areas.User.Models.Dialog;
using MarketplaceMVC.Web.Areas.User.Models.Offer;
using MarketplaceMVC.Web.Areas.User.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Areas.User.Automapper
{
    public partial class DomainToViewModelUserMappingProfile : Profile
    {
        public DomainToViewModelUserMappingProfile()
        {
            CreateMap<Offer, OfferViewModel>()
                .ForPath(o => o.Game, map => map.MapFrom(vm => vm.Game.Name))
                .ForMember(o => o.Header, map => map.MapFrom(vm => vm.Header))
                .ForMember(o => o.SellerPaysMiddleman, map => map.MapFrom(vm => vm.SellerPaysMiddleman))
                .ForMember(o => o.IsBanned, map => map.MapFrom(vm => vm.IsBanned))
                .ForMember(o => o.PersonalAccount, map => map.MapFrom(vm => vm.PersonalAccount))
                .ForMember(o => o.Url, map => map.MapFrom(vm => vm.Url))
                .ForMember(o => o.Discription, map => map.MapFrom(vm => vm.Discription))
                .ForMember(o => o.CreatedAccountDate, map => map.MapFrom(vm => vm.CreatedAccountDate))
                .ForMember(o => o.Price, map => map.MapFrom(vm => vm.Price));

            CreateMap<Dialog, DialogViewModel>()
               .ForMember(o => o.Id, map => map.MapFrom(vm => vm.Id))

               .ForMember(o => o.Messages, map => map.MapFrom(vm => vm.Messages))
               .ForMember(o => o.Companion, map => map.MapFrom(vm => vm.Companion))
               .ForMember(o => o.Creator, map => map.MapFrom(vm => vm.Creator))
               .ForMember(o => o.CountOfNewMessages, map => map.MapFrom(vm => vm.Messages.Where(m => !m.ToViewed)));

            CreateMap<Dialog, DetailsDialogViewModel>()
               .ForMember(o => o.Id, map => map.MapFrom(vm => vm.Id));
            //CreateMap<Order, OrderViewModel>()
            //    .ForMember(o => o.ShortUrl, map => map.MapFrom(vm => GetSplitedUrl(vm.Url, '/', 4)))
            //    .ForPath(o => o.UserId, map => map.MapFrom(vm => vm.UserProfile.Id))
            //    .ForPath(o => o.UserName, map => map.MapFrom(vm => vm.UserProfile.Name));
        }
    }
}