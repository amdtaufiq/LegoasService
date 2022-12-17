using LegoasService.Core.DAOs;
using LegoasService.Core.Interfaces.Repositories;
using LegoasService.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;

namespace LegoasService.Infrastucture.Repositories
{
    public class OfficerRepository : BaseRepository<Officer>, IOfficerRepository
    {

        public OfficerRepository(BEDBContext context) : base(context)
        {

        }

        public IEnumerable<Officer> GetAllOfficerDetail()
        {
            return _ctx.Officers
                .Include(x => x.OfficerRoles.Where(x => x.IsDelete == false))
                .ThenInclude(x => x.Role)
                .ThenInclude(x => x.MenuAccesses.Where(x => x.IsDelete == false))
                .ThenInclude(x => x.Menu)
                .Include(x => x.OfficerOffices.Where(x => x.IsDelete == false))
                .ThenInclude(x => x.Office)
                .Where(x => x.IsDelete == false)
                .AsEnumerable();
        }

        public async Task<Officer> GetByIdOfficerDetail(Guid id)
        {
            return await _ctx.Officers
                .Include(x => x.OfficerRoles.Where(x => x.IsDelete == false))
                .ThenInclude(x => x.Role)
                .ThenInclude(x => x.MenuAccesses.Where(x => x.IsDelete == false))
                .ThenInclude(x => x.Menu)
                .Include(x => x.OfficerOffices.Where(x => x.IsDelete == false))
                .ThenInclude(x => x.Office)
                .Where(x => x.IsDelete == false && x.Id == id)
                .FirstAsync();
        }

        public async Task UpdateOfficer(Officer officer)
        {
            var model = await _ctx.Officers
                .Include(d => d.OfficerRoles.Where(x => x.IsDelete == false))
                .Include(x => x.OfficerOffices.Where(x => x.IsDelete == false))
                .Where(c => c.IsDelete == false && c.Id == officer.Id)
                .SingleOrDefaultAsync();

            if (model != null)
            {
                //Update officer
                _ctx.Entry(model).CurrentValues.SetValues(officer);

                //Delete OfficerRole
                foreach (var officerRole in model.OfficerRoles.ToList())
                {
                    if (!officer.OfficerRoles.Any(c => c.Id == officerRole.Id))
                        _ctx.OfficerRoles.Remove(officerRole);
                }

                //Update and Insert OfficerRole
                foreach (var officerRoleModel in officer.OfficerRoles)
                {
                    var existingOfficerRolee = model.OfficerRoles
                        .Where(c => c.Id == officerRoleModel.Id)
                        .FirstOrDefault();

                    if (existingOfficerRolee != null)
                    {
                        //update officerRole
                        officerRoleModel.OfficerId = model.Id;
                        _ctx.Entry(existingOfficerRolee).CurrentValues.SetValues(officerRoleModel);
                    }
                    else
                    {
                        //Insert new OfficerRole
                        var officerRole = new OfficerRole
                        {
                            RoleId = officerRoleModel.RoleId,
                            OfficerId = model.Id
                        };
                        model.OfficerRoles.Add(officerRole);
                    }
                }

                //Delete OfficerOffice
                foreach (var officerOffice in model.OfficerOffices.ToList())
                {
                    if (!officer.OfficerOffices.Any(c => c.Id == officerOffice.Id))
                        _ctx.OfficerOffices.Remove(officerOffice);
                }

                //Update and Insert OfficerOffice
                foreach (var officerOfficeModel in officer.OfficerOffices)
                {
                    var existingOfficerOfficee = model.OfficerOffices
                        .Where(c => c.Id == officerOfficeModel.Id)
                        .FirstOrDefault();

                    if (existingOfficerOfficee != null)
                    {
                        //update officerOffice
                        officerOfficeModel.OfficerId = model.Id;
                        _ctx.Entry(existingOfficerOfficee).CurrentValues.SetValues(officerOfficeModel);
                    }
                    else
                    {
                        //Insert new OfficerOffice
                        var officerOffice = new OfficerOffice
                        {
                            OfficeId = officerOfficeModel.OfficeId,
                            OfficerId = model.Id
                        };
                        model.OfficerOffices.Add(officerOffice);
                    }
                }
            }
        }

        public async Task<Officer> GetOfficerByEmail(string email)
        {
            return await _ctx.Officers
                .Include(x => x.OfficerRoles.Where(x => x.IsDelete == false))
                .ThenInclude(x => x.Role)
                .ThenInclude(x => x.MenuAccesses.Where(x => x.IsDelete == false))
                .ThenInclude(x => x.Menu)
                .Include(x => x.OfficerOffices.Where(x => x.IsDelete == false))
                .ThenInclude(x => x.Office)
                .Where(x => x.Email == email &&
                            x.IsDelete == false)
                .FirstAsync();
        }

        public async Task<Officer> GetOfficerByToken(string token)
        {
            return await _ctx.Officers
                .Include(x => x.OfficerRoles.Where(x => x.IsDelete == false))
                .ThenInclude(x => x.Role)
                .ThenInclude(x => x.MenuAccesses.Where(x => x.IsDelete == false))
                .ThenInclude(x => x.Menu)
                .Include(x => x.OfficerOffices.Where(x => x.IsDelete == false))
                .ThenInclude(x => x.Office)
                .Where(x => x.Token == token &&
                            x.IsDelete == false)
                .FirstAsync();
        }
    }
}
