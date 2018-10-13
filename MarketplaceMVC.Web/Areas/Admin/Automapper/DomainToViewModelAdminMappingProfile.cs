using AutoMapper;
using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Web.Areas.Admin.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Areas.Admin.Automapper
{
    public class DomainToViewModelAdminMappingProfile : Profile
    {
        public DomainToViewModelAdminMappingProfile()
        {
            CreateMap<UserProfile, UserProfileViewModel>()
                .ForMember(o => o.Id, map => map.MapFrom(vm => vm.User.Id))
                .ForMember(o => o.LockoutReason, map => map.MapFrom(vm => vm.LockoutReason))
                .ForMember(o => o.Name, map => map.MapFrom(vm => vm.Name))
                .ForMember(o => o.IsBanned, map => map.MapFrom(vm => vm.User.LockoutEnabled))
                .ForMember(o => o.NegativeFeedbackCount, map => map.MapFrom(vm => vm.NegativeFeedbackCount))
                .ForMember(o => o.PositiveFeedbackCount, map => map.MapFrom(vm => vm.PositiveFeedbackCount))
                .ForMember(o => o.Avatar32Path, map => map.MapFrom(vm => vm.Avatar32))
                .ForMember(o => o.Balance, map => map.MapFrom(vm => vm.Balance))
                .ForMember(o => o.Email, map => map.MapFrom(vm => vm.User.Email))
                .ForMember(o => o.EmailConfirmed, map => map.MapFrom(vm => vm.User.EmailConfirmed));

        }
    }
}