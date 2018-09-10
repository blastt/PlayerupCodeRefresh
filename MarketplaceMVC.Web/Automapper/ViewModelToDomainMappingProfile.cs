using AutoMapper;
using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Web.Areas.Admin.Models.Game;
using MarketplaceMVC.Web.Models.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Automapper
{
    public partial class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        public ViewModelToDomainMappingProfile()
        {
            #region Offer

            CreateMap<CreateOfferViewModel, Offer>()
                .ForMember(o => o.Header, map => map.MapFrom(vm => vm.Header))
                .ForMember(o => o.SellerPaysMiddleman, map => map.MapFrom(vm => vm.SellerPaysMiddleman))
                .ForMember(o => o.Discription, map => map.MapFrom(vm => vm.Discription))
                .ForMember(o => o.AccountLogin, map => map.MapFrom(vm => vm.AccountLogin))
                .ForMember(o => o.Price, map => map.MapFrom(vm => vm.Price))
                .ForMember(o => o.CountOfGames, map => map.MapFrom(vm => vm.CountOfGames))
                .ForMember(o => o.CreatedAccountDate, map => map.MapFrom(vm => vm.CreatedAccountDate))
                .ForMember(o => o.PersonalAccount, map => map.MapFrom(vm => vm.PersonalAccount))
                .ForMember(o => o.Url, map => map.MapFrom(vm => vm.Url))
                .ForAllOtherMembers(opt => opt.Ignore());

            #endregion

            CreateMap<CreateGameViewModel, Game>()
                .ForMember(o => o.Name, map => map.MapFrom(vm => vm.Name))
               .ForMember(o => o.Value, map => map.MapFrom(vm => vm.Value));
        }
    }
}