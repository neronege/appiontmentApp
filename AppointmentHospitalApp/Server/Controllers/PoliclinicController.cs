using AppointmentHospitalApp.Server.Service.ForPoliclinic;
using AppointmentHospitalApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentHospitalApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliclinicController : ControllerBase
    {
        private readonly IPoliclinicService _policlinicService;
        public PoliclinicController(IPoliclinicService policlinicService)
        {
            _policlinicService = policlinicService;
        }

        [HttpPost("add"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Policlinic>>> CreatePoliclinic(Policlinic policlinic)
        {
            var result = await _policlinicService.AddPoly(policlinic);
            return Ok(result);
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<ServiceResponse<List<Policlinic>>>> GetPoliclinics()
        {
            var result = await _policlinicService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<ServiceResponse<Policlinic>>> GetById([FromRoute] int id)
        {
            var result = await _policlinicService.GetById(id);
            return Ok(result);

        }

        [HttpDelete("delete/{policlinicId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<int>>> Delete([FromRoute] int policlinicId)
        {
            var result = await _policlinicService.DeletePoly(policlinicId);
            return Ok(result);
        }
        [HttpPut("update"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Policlinic>>> Update(Policlinic policlinic)
        {
            var result = await _policlinicService.UpdatePoly(policlinic);
            return Ok(result);
        }
    }
}


