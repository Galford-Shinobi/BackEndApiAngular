using BackEndApi.Shared.Applications.Contract;
using BackEndApi.Shared.DataContext;
using BackEndApi.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEndApi.Shared.Applications.BusinessLogic
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly DBEmpleadoContext _dataContext;

        public DepartamentoService(DBEmpleadoContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<int> AddAsync(Departamento entity)
        {
            _dataContext.Departamentos.Add(entity);
            var r = await SaveAllAsync();
            return r == true ? 1 : 0;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var avatar = await _dataContext.Departamentos.FirstOrDefaultAsync(c => c.IdDepartamento == id);

            if (avatar is null) { return 0; }

            _dataContext.Departamentos.Remove(avatar);
            var r = await SaveAllAsync();
            return r == true ? 1 : 0;
        }

        public async Task<IReadOnlyList<Departamento>> GetAllAsync()
        {
            return await _dataContext.Departamentos
               .ToListAsync();
        }

        public async Task<Departamento> GetByIdAsync(int id)
        {
            return await _dataContext
                .Departamentos
                .FirstOrDefaultAsync(c => c.IdDepartamento == id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<int> UpdateAsync(Departamento entity)
        {
            _dataContext.Departamentos.Update(entity);
            var r = await SaveAllAsync();
            return r == true ? 1 : 0;
        }
    }
}
