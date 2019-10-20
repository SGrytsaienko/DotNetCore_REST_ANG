namespace TourManagement.API.Dtos
{
    public class TourWithManagerForCreation : TourForCreation
    {
        public int ManagerId { get; set; }
    }
}