using System.Linq;
using System.Web.Mvc;
using DotNetBay.Core;
using DotNetBay.Data.EF;
using DotNetBay.Data.Entity;
using DotNetBay.WebApp.ViewModel;

namespace DotNetBay.WebApp.Controllers
{
    public class AuctionsController : Controller
    {
        private readonly IAuctionService service;
        private EFMainRepository mainRepository;

        public AuctionsController()
        {
            this.mainRepository = new EFMainRepository();

            this.service = new AuctionService(this.mainRepository, new SimpleMemberService(this.mainRepository));

        }

        // GET: Auctions
        public ActionResult Index()
        {
            return View(this.service.GetAll().ToList());
        }

        // GET: Auctions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auctions/Create
        [HttpPost]
        public ActionResult Create(NewAuctionViewModel auction)
        {
            if (this.ModelState.IsValid)
            {
                var members = new SimpleMemberService(this.mainRepository);

                var newAuction = new Auction()
                {
                    Title = auction.Title,
                    Description = auction.Description,
                    StartDateTimeUtc = auction.StartDateTimeUtc,
                    EndDateTimeUtc = auction.EndDateTimeUtc,
                    StartPrice = auction.StartPrice,
                    Seller = members.GetCurrentMember()
                };

                // Get File Contents
                if (auction.Image != null)
                {
                    byte[] fileContent = new byte[auction.Image.InputStream.Length];
                    auction.Image.InputStream.Read(fileContent, 0, fileContent.Length);

                    newAuction.Image = fileContent;
                }

                this.service.Save(newAuction);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Image(int auctionId)
        {
            var auction = this.mainRepository.GetAuctions().FirstOrDefault(a => a.Id == auctionId);

            if (auction == null)
            {
                return this.HttpNotFound();
            }

            if (auction.Image != null)
            {
                return new FileContentResult(auction.Image, "image/jpg");
            }

            return new EmptyResult();
        }

    }
}
