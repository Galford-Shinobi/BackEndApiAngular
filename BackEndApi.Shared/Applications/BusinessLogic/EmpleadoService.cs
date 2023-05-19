using BackEndApi.Shared.Applications.Contract;
using BackEndApi.Shared.DataContext;
using BackEndApi.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEndApi.Shared.Applications.BusinessLogic
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly DBEmpleadoContext _dataContext;

        public EmpleadoService(DBEmpleadoContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<int> AddAsync(Empleado entity)
        {
            _dataContext.Empleados.Add(entity);
            var r = await SaveAllAsync();
            return r == true ? 1 : 0;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var avatar = await _dataContext.Empleados.FirstOrDefaultAsync(c => c.IdEmpleado == id);

            if (avatar is null) { return 0; }

            _dataContext.Empleados.Remove(avatar);
            var r = await SaveAllAsync();
            return r == true ? 1 : 0;
        }

        public async Task<IReadOnlyList<Empleado>> GetAllAsync()
        {
            return await _dataContext.Empleados.Include(p => p.IdDepartamentoNavigation)
               .ToListAsync();
        }

        public async Task<Empleado> GetByAddAsync(Empleado model)
        {
            try
            {
                _dataContext.Empleados.Update(model);
                var r = await SaveAllAsync();

                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Empleado> GetByIdAsync(int id)
        {
            return await _dataContext
              .Empleados.Include(p => p.IdDepartamentoNavigation)
              .FirstOrDefaultAsync(c => c.IdDepartamento == id);
        }

        public async Task<Empleado> GetByIdsAsync(int id)
        {
            return await _dataContext
               .Empleados.Include(p => p.IdDepartamentoNavigation)
               .FirstOrDefaultAsync(c => c.IdDepartamento == id);
        }

        public async Task<List<Empleado>> GetFullAsync()
        {
            return await _dataContext.Empleados.Include(p=> p.IdDepartamentoNavigation)
               .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<int> UpdateAsync(Empleado entity)
        {
            _dataContext.Empleados.Update(entity);
            var r = await SaveAllAsync();
            return r == true ? 1 : 0;
        }
    }
}
