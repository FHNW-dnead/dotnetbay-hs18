using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetBay.Core;
using DotNetBay.Data.EF;
using DotNetBay.Interfaces;
using DotNetBay.WebApp.ViewModel;

namespace DotNetBay.WebApp.Controllers
{
    public class BidsController : Controller
    {
        private IMainRepository mainRepository;

        private IAuctionService service;

        public BidsController()
        {
            this.mainRepository = new EFMainRepository();
            this.service = new AuctionService(this.mainRepository, new SimpleMemberService(this.mainRepository));
        }

        // GET: Bids
        public ActionResult Create(int auctionId)
        {
            var auction = this.service.GetAll().FirstOrDefault(a => a.Id == auctionId);

            if (auction == null)
            {
                return this.HttpNotFound();
            }

            var vm = new NewBidViewModel()
            {
                AuctionId = auctionId,
                AuctionTitle = auction.Title,
                AuctionDescription = auction.Description,
                StartPrice = auction.StartPrice,
                CurrentPrice = auction.CurrentPrice,
                BidAmount = Math.Max(auction.StartPrice, auction.CurrentPrice)
            };

            return View(vm);
        }

        // GET: Bids
        [HttpPost]
        public ActionResult Create(NewBidViewModel bid)
        {
            if (this.ModelState.IsValid)
            {
                var auction = this.service.GetAll().FirstOrDefault(a => a.Id == bid.AuctionId);

                this.service.PlaceBid(auction, bid.BidAmount);
            }

            return this.RedirectToAction("Index", "Auctions");
        }
    }
}
