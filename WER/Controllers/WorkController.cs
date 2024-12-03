using BussinessLogic.Interfaces;
using BussinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WER.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkController : ControllerBase
    {
        private readonly IWorkService _workService;

        public WorkController(IWorkService workService)
        {
            _workService = workService;
        }

        [HttpGet("GetWorksOfUser")]
        public async Task<IActionResult> GetWorksOfUser(int userId)
        {
            try
            {
                List<WorkVM> works = await _workService.GetWorksOfUser(userId);
                return Ok(works);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("SaveWork")]
        public async Task<IActionResult> SaveWork([FromBody] WorkVM work)
        {
            try
            {
                return Ok(await _workService.SaveWork(work));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("UpdateWork")]
        public async Task<IActionResult> UpdateWork([FromBody] WorkVM work)
        {
            try
            {
                return Ok(await _workService.UpdateWork(work));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
