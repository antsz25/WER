using BussinessLogic.Interfaces;
using BussinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WER.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationService _publicationService;
        public PublicationController(IPublicationService publicationService)
        {
            _publicationService = publicationService;
        }
        [HttpGet("GetExperiences")]
        public async Task<IActionResult> GetExperiences()
        {
            return Ok(await _publicationService.GetExperiences());
        }
        [HttpGet("GetMyExperiences")]
        public async Task<IActionResult> GetMyExperiences(int userId)
        {
            return Ok(await _publicationService.GetExperiencesByUser(userId));
        }
        [HttpPost("SaveExperience")]
        public async Task<IActionResult> SaveExperience(PublicationVM publication)
        {
            try
            {
                return Ok(await _publicationService.SaveExperience(publication));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
