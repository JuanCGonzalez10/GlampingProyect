using Microsoft.EntityFrameworkCore;

namespace  GlampingProyect.Web.Data.Entities
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public GlampingRole Role { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
