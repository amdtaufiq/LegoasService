using AutoMapper;
using LegoasService.Core.DAOs;
using LegoasService.Core.DTOs;

namespace LegoasService.Infrastucture.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Mapping Menu
            CreateMap<Menu, MenuResponse>();

            CreateMap<CreateMenuRequest, Menu>();

            CreateMap<UpdateMenuRequest, Menu>();

            //Mapping Role
            CreateMap<Role, RoleResponse>();

            CreateMap<CreateRoleRequest, Role>();

            CreateMap<UpdateRoleRequest, Role>();

            //Mapping Office
            CreateMap<Office, OfficeResponse>();

            CreateMap<CreateOfficeRequest, Office>();

            CreateMap<UpdateOfficeRequest, Office>();

            //Mapping MenuAccess
            CreateMap<MenuAccess, MenuAccessResponse>();

            CreateMap<CreateMenuAccessRequest, MenuAccess>();

            CreateMap<UpdateMenuAccessRequest, MenuAccess>();

            //Mapping Officer
            CreateMap<Officer, OfficerResponse>();

            CreateMap<CreateOfficerRequest, Officer>();

            CreateMap<UpdateOfficerRequest, Officer>();

            //Mapping OfficerRole
            CreateMap<OfficerRole, OfficerRoleResponse>();

            CreateMap<CreateOfficerRoleRequest, OfficerRole>();

            CreateMap<UpdateOfficerRoleRequest, OfficerRole>();

            //Mapping OfficerOffice
            CreateMap<OfficerOffice, OfficerOfficeResponse>();

            CreateMap<CreateOfficerOfficeRequest, OfficerOffice>();

            CreateMap<UpdateOfficerOfficeRequest, OfficerOffice>();

        }
    }
}
