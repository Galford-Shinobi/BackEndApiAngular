using BackEndApi.Shared.Entities;

namespace BackEndApi.Shared.Applications.Contract
{
    public interface IEmpleadoService : IGenericRepository<Empleado>
    {
        Task<Empleado> GetByIdsAsync(int id);
        Task<List<Empleado>> GetFullAsync();
        Task<Empleado> GetByAddAsync(Empleado model);
    }
}
