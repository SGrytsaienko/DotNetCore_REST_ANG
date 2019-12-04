using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourManagement.API.Dtos;
using TourManagement.API.Helpers;
using TourManagement.API.Services;

namespace TourManagement.API.Controllers
{
    [Route("api/tours/{tourId}/showcollections")]
    [Authorize]
    public class ShowCollectionsController : Controller
    {
        private readonly ITourManagementRepository _tourManagementRepository;

        public ShowCollectionsController(ITourManagementRepository tourManagementRepository)
        {
            _tourManagementRepository = tourManagementRepository;
        }

        [HttpGet("({showIds})", Name = "GetShowCollection")]
        [RequestHeaderMatchesMediaType
        ("Accept",
            new string[]
            {
                "application/json",
                "application/vnd.marvin.showcollectionforcreation+json"
            })]
        public async Task<IActionResult> GetShowCollection(
            int tourId,
            [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<int> showIds)
        {
            if (showIds == null)
            {
                return BadRequest();
            }

            if (!await _tourManagementRepository.TourExists(tourId))
            {
                return NotFound();
            }

            var showEntities = await _tourManagementRepository.GetShows(tourId, showIds);

            if (showIds.Count() != showEntities.Count())
            {
                return NotFound();
            }

            var showCollectionToReturn = Mapper.Map<IEnumerable<Show>>(showEntities);

            return Ok(showCollectionToReturn);
        }

        [HttpPost]
        [RequestHeaderMatchesMediaType
        ("Content-Type",
            new string[]
            {
                "application/json",
                "application/vnd.marvin.showcollectionforcreation+json"
            })]
        public async Task<IActionResult> CreateShowCollection(
            int tourId,
            [FromBody] IEnumerable<ShowForCreation> showCollection)
        {
            if (showCollection == null)
            {
                return BadRequest();
            }

            if (!await _tourManagementRepository.TourExists(tourId))
            {
                return NotFound();
            }

            var showEntities = Mapper.Map<IEnumerable<Entities.Show>>(showCollection);

            foreach (var show in showEntities)
            {
                await _tourManagementRepository.AddShow(tourId, show);
            }

            if (!await _tourManagementRepository.SaveAsync())
            {
                throw new Exception("Adding a collection of shows failed on save");
            }

            var showCollectionToReturn = Mapper.Map<IEnumerable<Show>>(showEntities);

            var showIdsAsString = string.Join(",", showCollectionToReturn.Select(a => a.ShowId));

            return CreatedAtRoute("GetShowCollection",
                new {tourId, showIds = showIdsAsString},
                showCollectionToReturn);
        }
    }
}