using AutoMapper;
using MarketplaceMVC.Web.Areas.Admin.Automapper;
using MarketplaceMVC.Web.Areas.User.Automapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Automapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {

                x.AddProfile<DomainToViewModelMappingProfile>();
                x.AddProfile<ViewModelToDomainMappingProfile>();

                x.AddProfile<DomainToViewModelUserMappingProfile>();
                x.AddProfile<ViewModelToDomainUserMappingProfile>();

                x.AddProfile<DomainToViewModelAdminMappingProfile>();
                //x.AddProfile<ViewModelToDomainAdminMappingProfile>();

            });
        }
    }
}