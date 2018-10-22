using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNetBay.WebApp.ViewModel
{
    public class NewAuctionViewModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Start Price")]
        [Range(1, 1000)]
        [Required]
        public double StartPrice { get; set; }

        /// <summary>
        /// Gets or sets the UTC DateTime values to avoid wrong data when serializing the values
        /// </summary>
        [Display(Name = "Start Date and Time (UTC)")]
        [Required]
        public DateTime StartDateTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the UTC DateTime values to avoid wrong data when serializing the values
        /// </summary>
        [Display(Name = "End Date and Time (UTC)")]
        [Required]
        public DateTime EndDateTimeUtc { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }
}