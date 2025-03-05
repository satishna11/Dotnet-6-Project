using inventory_re.DAO;
using inventory_re.Models;
using McavesDataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace inventory_re.Controllers
{
    public class ProductCategoryController : Controller
    {
        appDbContext _context;

        public ProductCategoryController(appDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public object SaveData(string ProductCategoryname, string ProductCategorycode, int ProductCategoryID)
        {
            string query = @"if(@id =0)
                                begin
                             Insert into ProductCategory(CategoryName,CategoryCode,IsActive)
                               values(@CategoryName,@CategoryCode,1)
                                end
                                 else
                                    begin
                                    Update ProductCategory set CategoryName = @CategoryName, CategoryCode= @CategoryCode
                                      where ProductCategoryID = @id
                                       end";

            SqlParameter[] param = new SqlParameter[] {

                new SqlParameter("@CategoryName",ProductCategoryname),
                    new SqlParameter("@CategoryCode",ProductCategorycode),
                    new SqlParameter("@id",ProductCategoryID),
            };

            CommandType cmdType = CommandType.Text;

            int result = DAOHelper.IUD(query, param, cmdType);

            if (result > 0)
            {
                return Ok(new
                {
                    Success = true,
                    Message = "Data inserted successfully!!"
                });
            }
            else
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Data store failed!!"
                });
            }

        }
        public object DeleteData(int ProductCategoryid)
        {
            ProductCategory dbInfo = _context
                             .ProductCategory
                              .Where(x => x.ProductCategoryID == ProductCategoryid)
                               .FirstOrDefault();
            if (dbInfo == null)
            {
                return new
                {
                    Success = false,
                    Message = "Data Information Not Found!"
                };
            }

            dbInfo.IsActive = false;



            _context.SaveChanges();

            return new
            {
                Success = true,
                Message = "Data Deleted Successfully!"
            };
        }
        public object GetById(int id)
        {
            string query = @"select * from ProductCategory where ProductCategoryID = @id";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id" , id),
            };
            CommandType cmdType = CommandType.Text;

            DataTable dt = DAOHelper.GetTable(query, param, cmdType);

            return Ok(new
            {
                Success = true,
                Message = Newtonsoft.Json.JsonConvert.SerializeObject(dt)
            });
        }

        public object GetData()
        {
            List<ProductCategory> obj = _context.ProductCategory.Where(x => x.IsActive == true)
                .ToList();

            return obj;

        }
        //public object GetData()
        //{
        //    List<ProductCategory> obj = _context.ProductCategory.Where(x => x.IsActive == true)
        //        .ToList();

        //    return obj;

        //}
    }
}
