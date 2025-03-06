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
        public object DeleteData(int id)
        {
            string query = @"delete from Unit where unitID = @id";

            SqlParameter[] param = new SqlParameter[] {
        new SqlParameter("@id", id),
    };

            int result = DAOHelper.IUD(query, param, CommandType.Text);
            if (result > 0)
            {
                return Ok(new
                {
                    Success = true,
                    Message = "Unit Delete Success"
                });
            }
            else
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Query Execution Failed"
                });
            }
        }
        public object GetDataById(int id)
        {
            string query = @"select * from Unit where unitID = @id";

            SqlParameter[] param = new SqlParameter[] {
        new SqlParameter("@id", id),
    };

            DataTable dt = DAOHelper.GetTable(query, param, CommandType.Text);

            if (dt.Rows.Count > 0)
            {
                // Convert DataTable to a list of dictionaries
                var data = dt.AsEnumerable()
                    .Select(row => new
                    {
                        UnitID = row["UnitID"],
                        UnitName = row["UnitName"],
                        UnitCode = row["UnitCode"],
                        IsActive = row["IsActive"]
                    }).FirstOrDefault(); // Get the first (and only) unit

                return Ok(new
                {
                    Success = true,
                    Data = data
                });
            }
            else
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Unit not found"
                });
            }
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


        public object GetData(string groupName, string groupCode, string unitName = null, string unitCode = null, int? unitID = null, bool? isActive = null)
        {
            // Base query
            string query = @"SELECT * FROM Unit WHERE 1=1";

            // Add search conditions dynamically
            var parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(unitName))
            {
                query += " AND UnitName LIKE @UnitName";
                parameters.Add(new SqlParameter("@UnitName", $"%{unitName}%"));
            }

            if (!string.IsNullOrEmpty(unitCode))
            {
                query += " AND UnitCode LIKE @UnitCode";
                parameters.Add(new SqlParameter("@UnitCode", $"%{unitCode}%"));
            }

            if (unitID.HasValue)
            {
                query += " AND UnitID = @UnitID";
                parameters.Add(new SqlParameter("@UnitID", unitID.Value));
            }

            if (isActive.HasValue)
            {
                query += " AND IsActive = @IsActive";
                parameters.Add(new SqlParameter("@IsActive", isActive.Value));
            }

            // Execute the query
            DataTable dt = DAOHelper.GetTable(query, parameters.ToArray(), CommandType.Text);

            // Return the result
            return Ok(new
            {
                Success = true,
                Message = JsonConvert.SerializeObject(dt)
            });
        }
    }
}
