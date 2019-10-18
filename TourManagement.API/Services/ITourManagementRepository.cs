using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourManagement.API.Entities;

namespace TourManagement.API.Services
{
    public interface ITourManagementRepository
    {
        Task AddTour(Tour tour);
        Task DeleteTour(Tour tour);
        Task<Tour> GetTour(int tourId, bool includeShows = false);
        Task<IEnumerable<Tour>> GetTours(bool includeShows = false);
        Task<IEnumerable<Tour>> GetToursForManager(int managerId, bool includeShows = false);
        Task<bool> IsTourManager(int tourId, int managerId);
        Task<bool> SaveAsync();
        Task<bool> TourExists(int tourId);
        Task UpdateTour(Tour tour);
        Task<IEnumerable<Show>> GetShows(int tourId);
        Task<IEnumerable<Show>> GetShows(int tourId, IEnumerable<int> showIds);
        Task AddShow(int tourId, Show show);
        Task<IEnumerable<Band>> GetBands();
        Task<IEnumerable<Manager>> GetManagers();
    }
}