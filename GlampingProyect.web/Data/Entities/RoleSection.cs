namespace GlampingProyect.web.Data.Entities
{
    public class RoleSection
    {
        public int RoleId { get; set; }
        public GlampingRole Role { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; internal set; }
    }
}
