using AppointmentHospitalApp.Server.Service.ForMeet;
using AppointmentHospitalApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentHospitalApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetController : ControllerBase
    {
        private readonly IMeetService _meetService;
        public MeetController(IMeetService meetService)
        {
            _meetService = meetService;
        }
        [HttpPost("createMeet"), Authorize]
        public async Task<ActionResult<ServiceResponse<Meet>>> CreateMeet(Meet meet)
        {
            var result = await _meetService.CreateMeet(meet);
            return Ok(result);
        }

        [HttpDelete("cancelMeet/{meetId}"), Authorize]
        public async Task<ActionResult<ServiceResponse<Meet>>> Cancel([FromRoute] int id)
        {
            var result = await _meetService.Cancel(id);
            return Ok(result);
        }
        [HttpGet("meets")]
        public async Task<ActionResult<ServiceResponse<List<Meet>>>> GetMeets()
        {
            var result = await _meetService.GetMeets();
            return Ok(result);
        }

    }
}
