using System;

namespace TourManagement.API.Dtos
{
    public class Tour : TourAbstractBase
    {
        public int TourId { get; set; }
        public string Band { get; set; }
    }
}