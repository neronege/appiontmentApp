using AppointmentHospitalApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppointmentHospitalApp.Server.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Policlinic> Policlinics { get; set; }
        public DbSet<Meet> Meets { get; set; }
    }
}
