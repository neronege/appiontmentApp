using AppointmentHospitalApp.Server.Context;
using AppointmentHospitalApp.Shared;
using AppointmentHospitalApp.Shared.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppointmentHospitalApp.Server.Service.ForDoctor
{
    public class DoctorService : IDoctorService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public DoctorService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<DoctorDTO>> AddDoctor(DoctorDTO doctor)
        {   //DoctorDto'dan doctor'u kullanrak custom tip'i maplereyerek değşitiridk ve bunu add obj Doctor tipi çağrıyor
            var result = _mapper.Map<DoctorDTO, Doctor>(doctor);
            var addobj = _context.Doctors.Add(result);
            var maprevers = _mapper.Map<Doctor, DoctorDTO>(addobj.Entity);
            await _context.SaveChangesAsync();

            return new ServiceResponse<DoctorDTO>
            {
                Data = maprevers,
                Message = "Doctor is added",
                Success = true
            };
        }
        public async Task<ServiceResponse<int>> DeleteDoc(int doctorId)
        {
            var result = await _context.Doctors.FindAsync(doctorId);
            var mapReverse = _mapper.Map<Doctor, DoctorDTO>(result);
            if (mapReverse == null)
            {
                new ServiceResponse<DoctorDTO>()
                {
                    Message = "Doctor isn't found",
                    Success = false
                };
            }
            _context.Doctors.Remove(result);
            _context.SaveChanges();
            return new ServiceResponse<int>()
            {
                Message = " Doctor is deleted",
                Success = false,
                Data = result.DoctorId
            };

        }

        public async Task<ServiceResponse<List<Doctor>>> GetAll()
        {
            var result = await _context.Doctors.ToListAsync();
            var response = new ServiceResponse<List<Doctor>>();
            if (result.Count == 0)
            {
                response.Message = "Doctors are not found";
                response.Success = false;
            };
            return new ServiceResponse<List<Doctor>>
            {
                Data = result,
                Success = true,
                Message = "Doctors are founded"
            };
        }

        public async Task<ServiceResponse<List<Doctor>>> GetByDoctor(int policlinicId)
        {
            var result = await _context.Doctors.Where(x => x.PoliclinicId == policlinicId).ToListAsync();
            if (result == null)
            {
                new ServiceResponse<Doctor>()
                {
                    Success = false,
                    Message = "Doctor is not found",
                };
            }
            return new ServiceResponse<List<Doctor>>
            {
                Data = result,
                Success = true,
            };

        }

        public async Task<ServiceResponse<DoctorDTO>> UpdateDoctor(DoctorDTO doctor)
        {
            if (doctor == null)
            {
                new ServiceResponse<DoctorDTO>()
                {
                    Success = false,
                    Message = "Doctor is not found"
                };
            }
            var result = _mapper.Map<DoctorDTO, Doctor>(doctor);
            var updateobj = _context.Doctors.Update(result);
            var maprevers = _mapper.Map<Doctor, DoctorDTO>(updateobj.Entity);
            _context.SaveChanges();

            return new ServiceResponse<DoctorDTO>()
            {
                Data = maprevers,
                Success = true,
                Message = "Doctor is updated"
            };
        }

    }




}
