using System;

namespace TourManagement.API.Dtos
{
    public abstract class TourAbstractBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}