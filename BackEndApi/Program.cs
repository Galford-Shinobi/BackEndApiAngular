using AutoMapper;
using BackEndApi.Shared.Applications.BusinessLogic;
using BackEndApi.Shared.Applications.Contract;
using BackEndApi.Shared.Dtos;
using BackEndApi.Shared.Entities;
using BackEndApi.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
      builder =>
      {
          builder.AllowAnyOrigin();
          builder.AllowAnyHeader();
          builder.AllowAnyMethod();
          builder.SetIsOriginAllowed(origin => true);
      });
});
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region PETICIONES API REST

app.MapGet("/departamento/lista", async (
    IDepartamentoService _departamentoService, IMapper _mapper) =>
{
    var listaDepartamento = await _departamentoService.GetAllAsync();
    var listaDepartamentoDto = _mapper.Map<List<DepartamentoDTO>>(listaDepartamento);
    if (listaDepartamentoDto.Count == 0)
    {
        return Results.NotFound();
    }
    return Results.Ok(listaDepartamentoDto);
});

//video 01:02:20
app.MapGet("/Empleado/lista", async (
    IEmpleadoService _empleadoService, IMapper _mapper) =>
{
    var listEmpleados = await _empleadoService.GetAllAsync();
    var listEmpleadoDto = _mapper.Map<List<EmpleadoDTO>>(listEmpleados);
    if (listEmpleadoDto.Count == 0)
    {
        return Results.NotFound();
    }
    return Results.Ok(listEmpleadoDto);
});

app.MapGet("/Empleado/{id}", async (int id, IEmpleadoService _empleadoService, IMapper _mapper) =>
{
    var _empleado = await _empleadoService.GetByIdAsync(id);
    if (_empleado is null)
    {
        return Results.NotFound();
    }

    var emmpleadoDto = _mapper.Map<EmpleadoDTO>(_empleado);

    return Results.Ok(emmpleadoDto);
});

app.MapPost("/Empleado/guardar", async (EmpleadoDTO modelo, IEmpleadoService _empleadoService, IMapper _mapper) =>
{
    var empleado = _mapper.Map<Empleado>(modelo);
    var empleadocreado = await _empleadoService.GetByAddAsync(empleado);
    if (empleadocreado is null)
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }
    return Results.Created($"/Empleado/{empleadocreado.IdEmpleado}", empleadocreado);
});

app.MapPut("/Empleado/actualizar/{id}", async (int id, EmpleadoDTO modelo, IEmpleadoService _empleadoService, IMapper _mapper) =>
{
    if (id != modelo.IdEmpleado)
    {
        return Results.BadRequest();
    }

    var emplToUpdate = await _empleadoService.GetByIdsAsync(id);

    if (emplToUpdate == null)
        return Results.NotFound();

    var empleado = _mapper.Map<Empleado>(modelo);

    emplToUpdate.NombreCompleto = (emplToUpdate.NombreCompleto == empleado.NombreCompleto) ? emplToUpdate.NombreCompleto : empleado.NombreCompleto;

    emplToUpdate.Email = (emplToUpdate.Email == empleado.Email) ? emplToUpdate.NombreCompleto : empleado.NombreCompleto;

    emplToUpdate.Sueldo = (emplToUpdate.Sueldo == empleado.Sueldo) ? emplToUpdate.Sueldo : empleado.Sueldo;

    emplToUpdate.IdDepartamento = (emplToUpdate.IdDepartamento == empleado.IdDepartamento) ? emplToUpdate.IdDepartamento : empleado.IdDepartamento;

    emplToUpdate.FechaContrato = empleado.FechaContrato;

    if (await _empleadoService.UpdateAsync(emplToUpdate) == 0)
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }

    return Results.Created($"/Empleado/{emplToUpdate.IdEmpleado}", modelo);
});

app.MapDelete("/Empleado/eliminar/{IdEmpleado}", async (int IdEmpleado, IEmpleadoService _empleadoService) =>
{
    var emplTodelete = await _empleadoService.GetByIdsAsync(IdEmpleado);

    if (emplTodelete == null)
        return Results.NotFound();
    if (await _empleadoService.DeleteAsync(emplTodelete.IdEmpleado) == 0)
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }
    return Results.NoContent();
});

#endregion


app.UseCors("MyAllowSpecificOrigins");

app.Run();

