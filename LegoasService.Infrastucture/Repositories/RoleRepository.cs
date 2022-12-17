using LegoasService.Core.DAOs;
using LegoasService.Core.Interfaces.Repositories;
using LegoasService.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;

namespace LegoasService.Infrastucture.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {

        public RoleRepository(BEDBContext context) : base(context)
        {

        }

        public IEnumerable<Role> GetAllRoleDetail()
        {
            return _ctx.Roles
                .Include(x => x.MenuAccesses.Where(x => x.IsDelete == false))
                .ThenInclude(x => x.Menu)
                .Where(x => x.IsDelete == false)
                .AsEnumerable();
        }

        public async Task<Role> GetByIdRoleDetail(Guid id)
        {
            return await _ctx.Roles
                .Include(x => x.MenuAccesses
                .Where(x => x.IsDelete == false))
                .ThenInclude(x => x.Menu)
                .Where(x => x.IsDelete == false && x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateRole(Role role)
        {
            var model = await _ctx.Roles
                .Include(d => d.MenuAccesses.Where(x => x.IsDelete == false))
                .Where(c => c.IsDelete == false && c.Id == role.Id)
                .SingleOrDefaultAsync();

            if (model != null)
            {
                //Update role
                _ctx.Entry(model).CurrentValues.SetValues(role);

                //Delete MenuAccess
                foreach (var menuAccess in model.MenuAccesses.ToList())
                {
                    if (!role.MenuAccesses.Any(c => c.Id == menuAccess.Id))
                        _ctx.MenuAccesses.Remove(menuAccess);
                }

                //Update and Insert MenuAccess
                foreach (var menuAccessModel in role.MenuAccesses)
                {
                    var existingMenuAccesse = model.MenuAccesses
                        .Where(c => c.Id == menuAccessModel.Id)
                        .FirstOrDefault();

                    if (existingMenuAccesse != null)
                    {
                        //update menuAccess
                        menuAccessModel.RoleId = model.Id;
                        _ctx.Entry(existingMenuAccesse).CurrentValues.SetValues(menuAccessModel);
                    }
                    else
                    {
                        //Insert new MenuAccess
                        var menuAccess = new MenuAccess
                        {
                            CreateAccess = menuAccessModel.CreateAccess,
                            ReadAccess = menuAccessModel.ReadAccess,
                            UpdateAccess = menuAccessModel.UpdateAccess,
                            DeleteAccess = menuAccessModel.DeleteAccess,
                            MenuId = menuAccessModel.MenuId,
                            RoleId = model.Id
                        };
                        model.MenuAccesses.Add(menuAccess);
                    }
                }
            }
        }
    }
}
