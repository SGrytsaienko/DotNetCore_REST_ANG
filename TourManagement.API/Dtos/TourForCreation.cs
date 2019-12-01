using System.ComponentModel.DataAnnotations;

namespace TourManagement.API.Dtos
{
    public class TourForCreation : TourAbstractBase
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "required|You must choose a band.")]
        public int BandId { get; set; }
    }
}