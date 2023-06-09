﻿using BackEndApi.Shared.Applications.Contract;
using BackEndApi.Shared.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackEndApi.Shared.Applications.BusinessLogic
{
    public static class ServiceExtensions
    {
        public static void AddApplication(this IServiceCollection Services, IConfiguration Configuration)
        {

            Services.AddDbContext<DBEmpleadoContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DockerConnection"));
            });

            Services.AddScoped<IEmpleadoService, EmpleadoService>();
            Services.AddScoped<IDepartamentoService, DepartamentoService>();
            //Services.AddScoped<IIvaRepository, IvaRepository>();
            //Services.AddScoped<IMedidumRepository, MedidumRepository>();
            //Services.AddScoped<IProductoRepository, ProductoRepository>();
            //Services.AddScoped<IRolRepository, RolRepository>();
            //Services.AddScoped<IUserFactoryRepository, UserFactoryRepository>();
            //Services.AddScoped<IProveedorRepository, ProveedorRepository>();
            //Services.AddScoped<IGenderRepository, GenderRepository>();
        }
    }
}
