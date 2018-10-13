using Hangfire;
using MarketplaceMVC.Service;
using MarketplaceMVC.Web.HtmlHelpers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Hangfire
{
    public class DeactivateOfferJob
    {
        private readonly IOfferService offerService;
        private readonly IIdentityMessageService identityMessageService;
        public DeactivateOfferJob(IOfferService offerService, IIdentityMessageService identityMessageService)
        {
            this.offerService = offerService;
            this.identityMessageService = identityMessageService;
        }

        [DisableConcurrentExecution(10 * 60)]
        public void Do(int itemId, string callbackUrl)
        {

            var offer = offerService.GetOffer(itemId, i => i.UserProfile, i => i.UserProfile.User);
            if (offer != null)
            {
                offerService.DeactivateOffer(offer, offer.UserProfileId);
                offerService.SaveOffer();
                string body = EmailHelpers.ActivateForm($"Здравствуйте {offer.UserProfile.Name}, ваше объявление {offer.Header} деактивировано.", "Активировать", callbackUrl).ToString();
                identityMessageService.SendAsync(new IdentityMessage()
                {
                    Body = body,
                    Subject = "Ваше объявление деактивировано",

                    Destination = offer.UserProfile.User.Email
                }).Wait();

            }

        }
    }
}