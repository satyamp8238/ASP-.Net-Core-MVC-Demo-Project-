using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Models
{
    public class Products
    {
        private readonly AppDbContext moAppDbContext;
        public Products(AppDbContext foAppDbContext)
        {
            moAppDbContext = foAppDbContext;
        }

        public List<Product> getProducts(int? fiProductId)
        {
            return moAppDbContext.Set<Product>().FromSqlInterpolated($"EXEC getProducts @Id={fiProductId}").AsEnumerable().ToList();
        }

        public int saveProduct(Product foProduct)
        {
            int liSuccess = 0;

            SqlParameter loSuccess = new SqlParameter("@inSuccess", liSuccess) { Direction = System.Data.ParameterDirection.Output };

            moAppDbContext.Database.ExecuteSqlInterpolated($@"EXEC saveProduct 
                                @inId = {foProduct.inId}, 
                                @stName = {foProduct.stName}, 
                                @inPrice = {foProduct.inPrice}, 
                                @inSuccess = {loSuccess} OUT");

            if (loSuccess != null && loSuccess.Value != null)
                liSuccess = Convert.ToInt32(loSuccess.Value);

            return liSuccess;
        }

        public int deleteProduct(int fiId)
        {
            int liSuccess = 0;

            SqlParameter loSuccess = new SqlParameter("@inSuccess", liSuccess) { Direction = System.Data.ParameterDirection.Output };
            moAppDbContext.Database.ExecuteSqlInterpolated($"EXEC deleteProduct @inId = {fiId}, @inSuccess = {loSuccess} OUT");

            if (loSuccess != null && loSuccess.Value != null)
                liSuccess = Convert.ToInt32(loSuccess.Value);

            return liSuccess;
        }
    }
}
