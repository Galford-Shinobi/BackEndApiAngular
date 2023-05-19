using AutoMapper;
using BackEndApi.Shared.Dtos;
using BackEndApi.Shared.Entities;
using System.Globalization;

namespace BackEndApi.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            #region Departamento
            CreateMap<Departamento, DepartamentoDTO>().ReverseMap();
            #endregion
            #region Empleados
            CreateMap<Empleado, EmpleadoDTO>()
                .ForMember(destino => destino.NombreDepartamento, opt => opt.MapFrom(origen => origen.IdDepartamentoNavigation.Nombre))
                .ForMember(destino => destino.FechaContrato, opt => opt.MapFrom(origen => origen.FechaContrato.ToString("dd/MM/yyyy")));

            CreateMap<EmpleadoDTO, Empleado>()
                .ForMember(destino => destino.IdDepartamentoNavigation, opt => opt.Ignore())
                .ForMember(destino => destino.FechaContrato, opt => opt.MapFrom(origen => DateTime.ParseExact(origen.FechaContrato, "dd/MM/yyyy",CultureInfo.InvariantCulture)));
            #endregion
        }
    }
}
