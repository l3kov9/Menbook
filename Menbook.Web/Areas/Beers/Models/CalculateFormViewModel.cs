namespace Menbook.Web.Areas.Beers.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CalculateFormViewModel
    {
        [Required]
        [Range(15,100)]
        public int Age { get; set; }

        [Required]
        [Range(50, 150)]
        public int Weight { get; set; }

        [Range(0.5, 12)]
        [Display(Name = "Drinking Hours")]
        public double HoursDrinking { get; set; }

        [Range(1,20)]
        [Display(Name = "Total Drinks")]
        public int TotalDrinks { get; set; }

        [Display(Name = "Beer")]
        public int BeerId { get; set; }

        public IEnumerable<SelectListItem> BeerNames { get; set; }
    }
}
