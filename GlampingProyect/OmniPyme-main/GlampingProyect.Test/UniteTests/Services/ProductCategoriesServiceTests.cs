using AutoMapper;
using GlampingProyect.Data;
using GlampingProyect.Web.Data.Entities;
using GlampingProyect.Web.Services;
using GlampingProyect.Web.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GlampingProyect.Test.UniteTests.Services
{
    [TestClass]
    
    internal class ProductCategoriesServiceTests : BaseTests
    {
        [TestMethod]
        public async Task GetPagination_ReturnAllCategories()
        {
            //Arrange
            string dbName = Guid.NewGuid().ToString();
            DataContext  context = BuildContext(dbName);
            IMapper mapper = ConfigureAutoMapper();

            context.AddRange(new List<ProductCategory>
            { 
                new ProductCategory { Name = "Category A"},
                new ProductCategory { Name = "Category B"},
                new ProductCategory { Name = "Category C"},
            });
            
            await context.SaveChangesAsync();

            //Act
            DataContext context2 = BuildContext(dbName);
           // IProductCategoriesService service = new ProductCategory();

            //Assert

        
        
        }
    }
}
