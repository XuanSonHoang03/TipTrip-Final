using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipTrip.Common.Models
{
	public class SearchTourModel
	{
		[Required(ErrorMessage = "Please select a pick up point")]
		public string PickupPoint { get; set; }

		[Required(ErrorMessage = "Please select a location.")]
		public string Location { get; set; }

		[Required(ErrorMessage = "Please select a start date.")]
		public DateTime StartDate { get; set; }

		[Required(ErrorMessage = "Please select a end date.")]
		public DateTime EndDate { get; set; }

		[Range(1, 100, ErrorMessage = "Number of people must be between 1 and 100.")]
		public int NumberOfPeople { get; set; }

		public double Budget { get; set; }

		
		public string? Avoid { get; set; }

		public string? Transpotation {  get; set; }

		public bool SaveSearch { get; set; }
	}
}
