using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using  GlampingProyect.Web.Data;

namespace  GlampingProyect.Web.Helpers
{
    public interface ICombosHelper
    {
        public Task<IEnumerable<SelectListItem>> GetComboCategories();
        public Task<IEnumerable<SelectListItem>> GetComboRoles();
    }

    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCategories()
        {
            List<SelectListItem> list = await _context.Categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una categoría...]",
                Value = "0"
            });

            return list;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboRoles()
        {
            List<SelectListItem> list = await _context.GlampingRoles.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un rol..]",
                Value = "0"
            });

            return list;
        }
    }
}
