using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNetBay.WebApp.ViewModel
{
    public class NewBidViewModel
    {
        [Key]
        public int AuctionId { get; set; }

        [Display(Name = "Title")]
        public string AuctionTitle { get; set; }

        [Display(Name = "Description")]
        public string AuctionDescription { get; set; }

        [Display(Name = "Start Price")]
        public double StartPrice { get; set; }

        [Display(Name = "Current Price")]
        public double CurrentPrice { get; set; }

        [Required]
        [Display(Name = "Your Bid")]
        public double BidAmount { get; set; }
    }
}