﻿using Microsoft.EntityFrameworkCore;
using GlampingProyect.Web.Data.Entities;
using GlampingProyect.web.Data.Entities;


namespace GlampingProyect.Web.Data.Seeders
{
    public class SectionsSeeder
    {
        private readonly DataContext _context;

        public SectionsSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            List<Section> sections = new List<Section>
            {
                new Section { Name  = "General", Descripcion = "Información basica de la compañia" },
                new Section { Name  = "Telecomunicaciones" },
                new Section { Name  = "Hacking" },
                new Section { Name  = "Clases", IsHidden = true },
                new Section { Name  = "Informática" },
                new Section { Name  = "Pentesting" },
            };

            foreach(Section section in sections)
            {
                bool exists = await _context.Sections.AnyAsync(s => s.Name == section.Name);

                if (!exists)
                {
                    await _context.Sections.AddAsync(section);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
