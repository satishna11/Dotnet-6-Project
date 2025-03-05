using inventory_re.Controllers;
using McavesDataAccess;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;


namespace Inventory.Controllers
{
    public class UnitController : BaseController
    {

        public UnitController()
        {

        }

        public IActionResult Create()
        {
            return View();
        }

        public object SaveData(int id, string unitname, string unitCode)
        {
            string query = @"if(@id = 0)
                              begin
                              	insert into Unit(UnitName,UnitCode,IsActive)
                              	values (@unitName,@unitCode,1)
                              end
                              else
                              begin
                              	update unit set unitName = @unitName , unitCode = @unitCode
                              	where unitID = @id
                              end";

            SqlParameter[] param = new SqlParameter[] {
                    new SqlParameter("@unitName",unitname),
                    new SqlParameter("@unitCode",unitCode),
                new SqlParameter("@id",id),
                };


            int result = DAOHelper.IUD(query, param, CommandType.Text);
            if (result > 0)
            {
                return Ok(new
                {
                    Success = true,
                    Message = "Unit Save Success"
                });
            }
            else
            {
                return Ok(new
                {
                    Success = true,
                    Message = "Query Execution Failes"
                });
            }
        }


        public object GetData(string groupName, string groupCode)
        {
            string query = @"select *from Unit";
            DataTable dt = DAOHelper.GetTable(query, null, CommandType.Text);
            return Ok(new
            {
                Success = true,
                Message = JsonConvert.SerializeObject(dt)
            });
        }

    }
}
