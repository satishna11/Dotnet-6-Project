using Inventory.Models.Common;
using inventory_re.Controllers;
using inventory_re.Models.ViewModel;
using McavesDataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Inventory.Controllers
{
    public class PurchaseController : BaseController
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
        public object SavePurchase([FromBody] PurchaseMasterVM model)
        {
            if (string.IsNullOrEmpty(model.Narration))
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


                DataTable dt = DAOHelper.GetTableWithTVP("sp_save_purchase", new SqlParameter[]
                {
                    new SqlParameter("@vendorid",model.VendorID),
                    new SqlParameter("@referenceNo",model.ReferenceNo),
                    new SqlParameter("@tranDate",model.TransactionDate),
                    new SqlParameter("@narration",model.Narration),
                    new SqlParameter("@createdBy",SessionUser.UserID),
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
        public object GetData(string vendor, string referenceNumber)
        {
           
            string query = @"sp_get_purchase";

            CommandType cmdType = CommandType.StoredProcedure;
            SqlParameter[] param = new SqlParameter[] {
                    new SqlParameter("@Vendor",vendor),
                    new SqlParameter("@ReferenceNo",referenceNumber),
                };

            DataTable dt = DAOHelper.GetTable(query, param, cmdType);

            return Ok(new
            {
                Success = true,
                Message = Newtonsoft.Json.JsonConvert.SerializeObject(dt)
            });


        }
        //    public object GetPurchaseDetails(int purchaseMasterID)
        //    {
        //        string query = "sp_pop_purchase_details"; 
        //        CommandType cmdType = CommandType.StoredProcedure;
        //        SqlParameter[] param = new SqlParameter[] {
        //        new SqlParameter("@PurchaseMasterID", purchaseMasterID) // Pass the PurchaseMasterID
        //};

        //        // Use the DAOHelper (assumed existing method) to execute the stored procedure and return a DataTable
        //        DataTable dt = DAOHelper.GetTable(query, param, cmdType);

        //        // Return the result as JSON using Ok() with a message containing the serialized DataTable
        //        return Ok(new
        //        {
        //            Success = true,
        //            Message = Newtonsoft.Json.JsonConvert.SerializeObject(dt)
        //        });
        //    }
        public IActionResult GetPurchaseDetails(int purchaseMasterID)
        {
            string query = "sp_pop_purchase_details";
            CommandType cmdType = CommandType.StoredProcedure;
            SqlParameter[] param = new SqlParameter[] {
        new SqlParameter("@PurchaseMasterID", purchaseMasterID) // Pass the PurchaseMasterID
    };

            // Use the DAOHelper to execute the stored procedure and return a DataTable
            DataTable dt = DAOHelper.GetTable(query, param, cmdType);

            // Convert DataTable to a list of objects that can be serialized to JSON
            var purchaseDetails = dt.AsEnumerable().Select(row => new
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
                message = purchaseDetails
            });
        }

        public object DeleteData(int id)
        {
            string query = @"sp_GD_purchase";
            int dec = 1;
            CommandType cmdType = CommandType.StoredProcedure;
            SqlParameter[] param = new SqlParameter[] {
                    new SqlParameter("@id",id),
                    new SqlParameter("@dec",dec),
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
