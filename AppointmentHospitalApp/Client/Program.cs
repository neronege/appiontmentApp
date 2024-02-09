using AppointmentHospitalApp.Client;
using AppointmentHospitalApp.Client.Service.ForAuth;
using AppointmentHospitalApp.Client.Service.ForDoctor;
using AppointmentHospitalApp.Client.Service.ForMeet;
using AppointmentHospitalApp.Client.Service.ForPoliclinic;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }); ;
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IPoliclinicService, PoliclinicService>();
builder.Services.AddScoped<IMeetService, MeetService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddOptions();


await builder.Build().RunAsync();
