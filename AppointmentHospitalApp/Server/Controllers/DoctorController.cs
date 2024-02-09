using AppointmentHospitalApp.Server.Service.ForDoctor;
using AppointmentHospitalApp.Shared;
using AppointmentHospitalApp.Shared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentHospitalApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;

        }

        [HttpGet("getAll")]
        public async Task<ActionResult<ServiceResponse<List<Doctor>>>> GetDoctors()
        {
            var result = await _doctorService.GetAll();
            return Ok(result);
        }
        [HttpPut("update")]
        public async Task<ActionResult<ServiceResponse<DoctorDTO>>> Updates(DoctorDTO doctor)
        {
            var result = await _doctorService.UpdateDoctor(doctor);
            return Ok(result);
        }
        [HttpPost("addDoctor"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<DoctorDTO>>> CreateDoctor(DoctorDTO doctor)
        {
            var result = await _doctorService.AddDoctor(doctor);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<List<Doctor>>>> GetByDoctor([FromRoute] int id)
        {
            var result = await _doctorService.GetByDoctor(id);
            return Ok(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ServiceResponse<int>>> DeleteDoctor([FromRoute] int id)
        {
            var result = await _doctorService.DeleteDoc(id);
            return Ok(result);
        }

    }
}
