using  GlampingProyect.Web.Data.Entities;
using  GlampingProyect.Web.Data.Entities;

namespace  GlampingProyect.Web.Data.Entities;


public class RoleCategory
{
    public int RoleId { get; set; }
    public GlampingRole Role { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
