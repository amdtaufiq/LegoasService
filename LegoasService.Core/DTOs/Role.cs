using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoasService.Core.DTOs
{
    public class RoleResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<MenuAccessResponse> MenuAccesses { get; set; }
    }

    public class CreateRoleRequest
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<CreateMenuAccessRequest> MenuAccesses { get; set; }
    }

    public class UpdateRoleRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<UpdateMenuAccessRequest> MenuAccesses { get; set; }
    }
}
