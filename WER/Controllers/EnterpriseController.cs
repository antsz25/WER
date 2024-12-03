using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BussinessLogic.ViewModels;
namespace WER.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnterpriseController : ControllerBase
    {
        private readonly IEnterpriseService _enterpriseService;

        public EnterpriseController(IEnterpriseService enterpriseService)
        {
            _enterpriseService = enterpriseService;
        }

        [HttpGet("GetEnterprises")]
        public async Task<IActionResult> GetEnterprises()
        {
            try
            {
                List<EnterpriseVM> enterprises = await _enterpriseService.GetEnterprises();
                return Ok(enterprises);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("SaveEnterprise")]
        public async Task<IActionResult> SaveEnterprise([FromBody] EnterpriseVM enterprise)
        {
            try
            {
                bool result = await _enterpriseService.SaveEnterprise(enterprise);
                if (!result)
                {
                    return Conflict("La empresa ya existe");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
