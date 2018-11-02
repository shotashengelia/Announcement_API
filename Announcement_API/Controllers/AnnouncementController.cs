using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Announcement_API.Services;
using AutoMapper;
using Announcement_API.Model;
using AnnouncementDatabase;
using Microsoft.AspNetCore.Http;


namespace Announcement_API.Controllers
{
    [Route("api/Announcement")]
    public class AnnouncementController : Controller
    {
        private IAnnouncementRepository _AnnouncementRepository;
        

        public AnnouncementController(IAnnouncementRepository AnnouncementRepository)
        {
            _AnnouncementRepository = AnnouncementRepository;
        }
        [HttpGet("api/Announcement")]
        public IActionResult GetAnnouncements()
        {
            var AnnouncementFromRepository = _AnnouncementRepository.GetAnnouncements();

            var Announcement = Mapper.Map<IEnumerable<Get_Announcement>>(AnnouncementFromRepository);

            return new JsonResult(AnnouncementFromRepository);
        }

        [HttpGet("{id}", Name = "GetAnnouncement")]
        public IActionResult GetAnnouncement(int id)
        {
            var AnnouncementFromRepository = _AnnouncementRepository.GetAnnouncement(id);

            if (AnnouncementFromRepository == null)
            {
                return NotFound();
            }
            var Announcement = Mapper.Map<Get_Announcement>(AnnouncementFromRepository);
            return Ok(Announcement);
            
        }

        [HttpPost]
        public IActionResult CreateAnnouncement([FromBody]Get_Announcement announcement)
        {
            if (announcement == null)
            {
                return BadRequest();
            }

            var AnnouncementEntity = Mapper.Map<Announcement>(announcement);
            _AnnouncementRepository.AddAnnouncement(AnnouncementEntity);


            if (_AnnouncementRepository.Save())
            {
                return StatusCode(500, "Problem with handling Request!");
            }
            
            return Save_Get_Announcement(AnnouncementEntity);
        }

        [HttpPost]
        public IActionResult BlockAnnouncementCreation(int id)
        {
            if (_AnnouncementRepository.AnnouncementExists(id))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAnnouncement(int id)
        {
            if (!_AnnouncementRepository.AnnouncementExists(id))
            {
                return NotFound();
            }

            var AnnouncementFromRepository = _AnnouncementRepository.GetAnnouncement(id);
            if (AnnouncementFromRepository == null)
            {
                return NotFound();
            }
            _AnnouncementRepository.DeleteAnnouncement(AnnouncementFromRepository);

            if (_AnnouncementRepository.Save())                                            
            {
                return StatusCode(500, "Problem with handling Request!");
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAnnouncement(int id, [FromBody] Update_Announcement announcement)
        {
            if (announcement == null)
            {
                return BadRequest();
            }
            var AnnouncementFromRepository = _AnnouncementRepository.GetAnnouncement(id);
            if (AnnouncementFromRepository == null)
            {
                var AnnoucementToCreate = Mapper.Map<Announcement>(announcement);
                AnnoucementToCreate.ID = id;

                _AnnouncementRepository.AddAnnouncement(AnnoucementToCreate);

                if (_AnnouncementRepository.Save())                                    
                {
                    return StatusCode(500, "Problem with handling Request!");
                }

                return Save_Get_Announcement(AnnoucementToCreate);
            }
            Mapper.Map(announcement, AnnouncementFromRepository);

            _AnnouncementRepository.UpdateAnnouncement(AnnouncementFromRepository);

            if (_AnnouncementRepository.Save())                                                                                    
            {
                return StatusCode(500, "Problem with handling Request!");
            }

            return Save_Get_Announcement(AnnouncementFromRepository);
        }

        
        [NonAction]
        private CreatedAtRouteResult Save_Get_Announcement(Announcement announcement)
        {
           
            var AnnouncementForGetting = Mapper.Map<Get_Announcement>(announcement);

            return CreatedAtRoute("GetAnnouncement", new { id = AnnouncementForGetting.ID }, AnnouncementForGetting);
        }
    }
}
