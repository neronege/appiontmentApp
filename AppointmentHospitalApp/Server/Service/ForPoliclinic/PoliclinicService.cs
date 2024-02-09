using AppointmentHospitalApp.Server.Context;
using AppointmentHospitalApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppointmentHospitalApp.Server.Service.ForPoliclinic
{
    public class PoliclinicService : IPoliclinicService
    {
        private readonly DataContext _context;
        public PoliclinicService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Policlinic>> AddPoly(Policlinic policlinic)
        {
            var result = await _context.Policlinics.FirstOrDefaultAsync(x => x.PoliclinicName.ToLower() == policlinic.PoliclinicName.ToLower());
            if (result == null)
            {
                _context.Policlinics.Add(policlinic);
                await _context.SaveChangesAsync();
                return new ServiceResponse<Policlinic>
                {
                    Data = policlinic,
                    Message = "Success",
                    Success = true
                };

            }
            else
            {
                return new ServiceResponse<Policlinic>
                {
                    Success = false,
                    Message = "Policlinic name already exist"
                };
            }


        }

        public async Task<ServiceResponse<int>> DeletePoly(int policlinicId)
        {
            var result = await _context.Policlinics.FindAsync(policlinicId);
            if (result == null)
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Policlinic is not found"
                };
            }
            else
            {
                _context.Policlinics.Remove(result);
                await _context.SaveChangesAsync();
                return new ServiceResponse<int>
                {
                    Data = result.Id,
                    Success = true,
                    Message = "Policlinic is deleted"

                };
            }

        }

        public async Task<ServiceResponse<List<Policlinic>>> GetAll()
        {
            var result = await _context.Policlinics.ToListAsync();
            var response = new ServiceResponse<List<Policlinic>>();
            if (result.Count == 0)
            {

                response.Success = false;
                response.Message = "Policlinic is not found";
            }

            else
            {
                return new ServiceResponse<List<Policlinic>>
                {
                    Data = result,
                    Success = true,
                };
            }
            return response;
        }

        public async Task<ServiceResponse<Policlinic>> GetById(int policlinicId)
        {
            var result = await _context.Policlinics.FirstOrDefaultAsync(x => x.Id == policlinicId);
            if (result == null)
            {
                return new ServiceResponse<Policlinic>
                {
                    Success = false,
                    Message = "Policlinic is not found"
                };
            }
            return new ServiceResponse<Policlinic>
            {
                Data = result,
                Success = true,
            };
        }

        public async Task<ServiceResponse<Policlinic>> UpdatePoly(Policlinic policlinic)
        {
            var result = await _context.Policlinics.FindAsync(policlinic.Id);
            if (result == null)
            {
                return new ServiceResponse<Policlinic>
                {
                    Success = false,
                    Message = "Policlinic is not found"
                };
            }
            result.PoliclinicName = policlinic.PoliclinicName;
            return new ServiceResponse<Policlinic>
            {
                Success = true,
                Data = result,
                Message = "Update is success"
            };
        }
    }
}
