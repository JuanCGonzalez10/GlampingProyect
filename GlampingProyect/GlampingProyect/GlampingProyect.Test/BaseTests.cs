using GlampingProyect.Data;
using GlampingProyect.Web.Core;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlampingProyect.Web.Test
{
    internal class BaseTests
    {
        protected DataContext BuildContext(string dbName)
        {

            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase("dbName")
             .Options;
            DataContext datacontext = new DataContext(options);
            return datacontext;
        }

        protected IMapper ConfigureAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapperProfiles());
            });

            return config.CreateMapper();
        }
    }
}
