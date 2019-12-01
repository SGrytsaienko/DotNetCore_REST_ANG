using System.ComponentModel.DataAnnotations;

namespace TourManagement.API.Dtos
{
    public class TourForUpdate : TourAbstractBase
    {
        [MaxLength(2000, ErrorMessage = "Description is too long.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "When updating a tour, the description is required.")]
        public override string Description
        {
            get => base.Description;
            set => base.Description = value;
        }
    }
}