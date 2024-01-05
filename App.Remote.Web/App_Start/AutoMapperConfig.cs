using AutoMapper;
using Jabil.Pico.Web.ViewModels;
using Jabil.Pico.Common.ViewModels;
using Jabil.Pico.Web.Models;
using Jabil.Pico.Web.DAL.Models;

namespace Jabil.Pico.Web.App_Start
{
    public static class AutoMapperConfig
    {
        public static Mapper InitAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRole, UserVM>();
                cfg.CreateMap<UserVM, UserRole>();
                cfg.CreateMap<Ticket, TicketVM>()
                    .ForMember(vm => vm.MachineName, opt => opt.MapFrom(src => src.Machine.Name))
                    .ForMember(vm => vm.ClassificationValue, opt => opt.MapFrom(src => src.Classification.Value));
                cfg.CreateMap<Device, DeviceVM>()
                    .ForMember(vm => vm.MachineName, opt => opt.MapFrom(src => src.Machine.Name));
            });

            var mapper = new Mapper(config);

            return mapper;
        }
    }
}