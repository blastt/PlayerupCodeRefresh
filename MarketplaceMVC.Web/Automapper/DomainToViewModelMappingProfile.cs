using AutoMapper;
using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Web.Areas.User.Models.Dialog;
using MarketplaceMVC.Web.Models.Offer;
using MarketplaceMVC.Web.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Automapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        private string GetSplitedUrl(string str, char ch, int number)
        {
            try
            {
                return str.Split(ch)[number].ToString();
            }
            catch (Exception)
            {

                return "Url скрыт";
            }
            
        }

        public DomainToViewModelMappingProfile()
        {
            CreateMap<Offer, OfferViewModel>()
                .ForMember(o => o.Header, map => map.MapFrom(vm => vm.Header))
               .ForMember(o => o.SellerPaysMiddleman, map => map.MapFrom(vm => vm.SellerPaysMiddleman))
               .ForMember(o => o.IsBanned, map => map.MapFrom(vm => vm.IsBanned))
               .ForMember(o => o.PersonalAccount, map => map.MapFrom(vm => vm.PersonalAccount))
               .ForMember(o => o.Url, map => map.MapFrom(vm => vm.Url))
               .ForMember(o => o.Discription, map => map.MapFrom(vm => vm.Discription))
               .ForMember(o => o.CreatedAccountDate, map => map.MapFrom(vm => vm.CreatedAccountDate))
               .ForMember(o => o.Price, map => map.MapFrom(vm => vm.Price))
                .ForMember(o => o.ShortUrl, map => map.MapFrom((vm) => GetSplitedUrl(vm.Url,'/',4)));

            CreateMap<Offer, DetailsOfferViewModel>()
                .ForMember(o => o.ShortUrl, map => map.MapFrom(vm => GetSplitedUrl(vm.Url, '/', 4)))
                .ForPath(o => o.UserId, map => map.MapFrom(vm => vm.UserProfile.Id))
                .ForPath(o => o.UserName, map => map.MapFrom(vm => vm.UserProfile.Name));

            CreateMap<Dialog, DialogViewModel>()
               .ForMember(o => o.Id, map => map.MapFrom(vm => vm.Id))

               .ForMember(o => o.Messages, map => map.MapFrom(vm => vm.Messages))
               .ForMember(o => o.Companion, map => map.MapFrom(vm => vm.Companion))
               .ForMember(o => o.Creator, map => map.MapFrom(vm => vm.Creator))
               .ForMember(o => o.CountOfNewMessages, map => map.MapFrom(vm => vm.Messages.Where(m => !m.ToViewed)));

            CreateMap<Dialog, DetailsDialogViewModel>()
               .ForMember(o => o.Id, map => map.MapFrom(vm => vm.Id));

            CreateMap<UserProfile, UserProfileViewModel>()
               .ForMember(o => o.Id, map => map.MapFrom(vm => vm.Id));
        }
    }
}