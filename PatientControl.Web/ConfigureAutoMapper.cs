using AutoMapper;
using ClassLibrary1;
using PatientControl.Web.Models;

namespace PatientControl.Web
{
    public class ConfigureAutoMapper : IBootstrapperTask
    {
        public void Run()
        {
            Mapper.CreateMap<Room, RoomModel>();
        }
    }
}