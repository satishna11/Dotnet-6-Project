using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Models.Common;
using inventory_re.Models.ViewModel;
using McavesDataAccess;
using Microsoft.AspNetCore.Mvc;

namespace inventory_re.Controllers
{
    public class SalesController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public object SaveSales([FromBody] SalesMasterVM model)
        {
            if (model == null)
            {
                // Handle the null model
                return BadRequest("Model is null");
            }
            else if (string.IsNullOrEmpty(model.Narration))
            {
                return Ok(new ResponseData
                {
                    Success = false,
                    Message = "Enter Narration"
                });
            }
            else if (model.Detail.Count == 0)
            {
                return Ok(new ResponseData
                {
                    Success = false,
                    Message = "Inventory Item Not Found"
                });
            }
            else
            {
                DataTable tvp = new DataTable();
                tvp.Columns.Add("InventoryItemID", typeof(int));
                tvp.Columns.Add("Quantity", typeof(int));
                tvp.Columns.Add("UnitID", typeof(int));
                tvp.Columns.Add("Rate", typeof(int));

                DataRow dr;
                foreach (var item in model.Detail)
                {
                    dr = tvp.NewRow();
                    dr["InventoryItemID"] = item.InventoryItemID;
                    dr["Quantity"] = item.Quantity;
                    dr["UnitID"] = item.UnitID;
                    dr["Rate"] = item.Rate;

                    tvp.Rows.Add(dr);
                }

                DataTable dt = DAOHelper.GetTableWithTVP("sp_save_sales", new SqlParameter[]
                {
                    new SqlParameter("@customerid", model.customerID),
                    new SqlParameter("@referenceNo", model.ReferenceNo),
                    new SqlParameter("@tranDate", model.TransactionDate),
                    new SqlParameter("@narration", model.Narration),
                    new SqlParameter("@createdBy", SessionUser.UserID),
                }, CommandType.StoredProcedure, tvp, "@detail");

                if (dt == null || dt.Rows.Count == 0)
                {
                    return Ok(new ResponseData
                    {
                        Success = false,
                        Message = "Error Occured During Query Execution"
                    });
                }
                else
                {
                    return Ok(new ResponseData
                    {
                        Success = true,
                        Message = dt.Rows[0]["message"].ToString()
                    });
                }
            }
        }
        public IActionResult GetSalesDetails(int salesMasterID)
        {
            string query = "sp_pop_sales_details";
            CommandType cmdType = CommandType.StoredProcedure;
            SqlParameter[] param = new SqlParameter[] {
        new SqlParameter("@SalesMasterID", salesMasterID) // Pass the PurchaseMasterID
    };

            // Use the DAOHelper to execute the stored procedure and return a DataTable
            DataTable dt = DAOHelper.GetTable(query, param, cmdType);

            // Convert DataTable to a list of objects that can be serialized to JSON
            var salesDetails = dt.AsEnumerable().Select(row => new
            {
                ItemName = row["ItemName"].ToString(),
                Quantity = Convert.ToInt32(row["Quantity"]),
                Rate = Convert.ToDecimal(row["Rate"]),
                Total = Convert.ToDecimal(row["Total"]),
                UnitName = row["UnitName"].ToString()
            }).ToList();

            // Return the result as JSON with a Success flag and the data
            return Ok(new
            {
                Success = true,
                message = salesDetails
            });
        }


        public object GetData(string customer, string referenceNumber)
        {
            string query = @"sp_get_sales";

            CommandType cmdType = CommandType.StoredProcedure;
            SqlParameter[] param = new SqlParameter[] {
                    new SqlParameter("@CustomerName", customer),
                    new SqlParameter("@ReferenceNo", referenceNumber),
                };

            DataTable dt = DAOHelper.GetTable(query, param, cmdType);

            return Ok(new
            {
                Success = true,
                Message = Newtonsoft.Json.JsonConvert.SerializeObject(dt)
            });
        }

        public object DeleteData(int id)
        {
            string query = @"sp_GD_sales";
            int dec = 1;
            CommandType cmdType = CommandType.StoredProcedure;
            SqlParameter[] param = new SqlParameter[] {
                    new SqlParameter("@id", id),
                    new SqlParameter("@dec", dec),
                };

            int result = DAOHelper.IUD(query, param, cmdType);

            if (result > 0)
            {
                return Ok(new
                {
                    Success = true,
                    Message = "Data Deleted Successfully!!"
                });
            }
            else
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Failed to delete data!!"
                });
            }
        }
    }
}
