using AppointmentHospitalApp.Server.Context;
using AppointmentHospitalApp.Server.Service.Auth;
using AppointmentHospitalApp.Shared;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AppointmentHospitalApp.Server.Service.ForMeet
{
    public class MeetService : IMeetService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;
        private readonly IEmailSender _emailSender;
        public MeetService(DataContext context, IAuthService authService, IEmailSender emailSender)
        {
            _context = context;
            _authService = authService;
            _emailSender = emailSender;
        }
        public async Task<ServiceResponse<Meet>> Cancel(int id)
        {
            var result = await _context.Meets.FirstOrDefaultAsync(x => x.Id == id);
            var response = result.MeetDate - DateTime.UtcNow;
            var hour = response.Hours;
            var day = response.Days;
            if (result.Status != false)
            {
                if (hour < 6 && day < 1)
                {
                    return new ServiceResponse<Meet>
                    {
                        Success = false,
                        Message = "You can cancel before 6 hours of meeting "
                    };
                }
                else
                {
                    result.Status = false;
                    await _context.SaveChangesAsync();
                    return new ServiceResponse<Meet>
                    {
                        Success = true,

                    };

                }

            }
            return null;
        }

        public async Task<ServiceResponse<Meet>> CreateMeet(Meet meet)
        {
            var result = await _context.Meets.Where(x => x.DoctorId == meet.DoctorId).ToListAsync();
            var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.DoctorId == meet.DoctorId);
            var poly = await _context.Policlinics.FirstOrDefaultAsync(x => x.Id == doctor.PoliclinicId);
            var mDate = await _context.Meets.Where(x => x.MeetDate.Month == meet.MeetDate.Month).ToListAsync();
            var user = _authService.GetUserId();
            var User = await _context.Users.FirstOrDefaultAsync(x => x.Id == user);
            var checkUser = await _context.Meets.Where(x => x.UserId == user).ToListAsync();
            var list = new ServiceResponse<Meet>();

            if (meet.MeetTime.Hours == 00)
            {
                return new ServiceResponse<Meet>
                {
                    Success = false,
                    Message = "Randevu saati girmeniz zorunludur",
                };
            }



            foreach (var item in checkUser)
            {
                if (item.Status == true)
                {
                    return new ServiceResponse<Meet>
                    {
                        Message = "Zaten aktif bir randevuya sahipsiniz",
                        Success = false,
                    };
                }

            }
            foreach (var item in result)
            {
                if (item.MeetDate == meet.MeetDate && item.MeetTime == meet.MeetTime)
                {
                    return new ServiceResponse<Meet>
                    {
                        Success = false,
                        Message = "Seçili alanda ilgili randevu doludur"
                    };
                }
            }
            if (doctor != null && poly != null)
            {
                meet.DoctorId = doctor.DoctorId;
                meet.UserId = user;
                meet.PoliclinicName = poly.PoliclinicName;
                meet.DoctorName = doctor.DoctorName;
                var addedObj = _context.Meets.Add(meet);
                await _context.SaveChangesAsync();
                await _emailSender.SendEmailAsync(User.Email, "Randevunuz alınmıştır.Randevu saatinden 15dk önce poliklinkte bulunuz",
                    meet.MeetTime.ToString() + "saatine randevunuz ayarlanmıştır");
                return new ServiceResponse<Meet>
                {
                    Success = true,
                    Data = addedObj.Entity,
                    Message = "Randevunuz başarıyla oluşturulmuştur",
                };
            }




            return list;
        }


        public Task<ServiceResponse<bool>> ExistMeet()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<Meet>>> GetMeets()
        {
            var user = _authService.GetUserId();
            var result = await _context.Meets.Where(x => x.UserId == user).ToListAsync();
            if (result.Count == 0)
            {
                new ServiceResponse<List<Meet>>
                {
                    Message = "Meet is not found",
                    Success = false,
                };
            }
            var response = new ServiceResponse<List<Meet>>
            {
                Data = result,
                Success = true,
            };
            return response;
        }

        public Task<ServiceResponse<Meet>> UpdateMeet(Meet meet)
        {
            throw new NotImplementedException();
        }
    }
}
