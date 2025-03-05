using inventory_re.DAO;
using inventory_re.Models;
using inventory_re.Models.ViewModel;
using inventory_re.Controllers;
using inventory_re.Models;
using inventory_re.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace inventory_re.Controllers
{
    public class InventoryItemController : BaseController
    {
        appDbContext _context;
        public InventoryItemController(appDbContext context)
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
        [HttpPost]
        public object SaveData([FromBody] InventoryItemsVM model)
        {
            if (string.IsNullOrEmpty(model.ItemName))
            {
                return Ok(new
                {
                    Success = false,
                    Message = "invalid Item  name"
                });
            }
            else if (string.IsNullOrEmpty(model.ItemCode))
            {
                return Ok(new
                {
                    Success = false,
                    Message = "invalid Item Code"
                });
            }
            else if (model.ProductCategoryID == 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "invalid Product Category "
                });
            }
            else if (model.SmallestUnitID == 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Please enter a valid unit "
                });
            }
            else
            {
                if (model.InventoryItemsId == 0)
                {

                    InventoryItems itms = new InventoryItems();
                    itms.ItemName = model.ItemName;
                    itms.ItemCode = model.ItemCode;
                    itms.ProductCategoryID = model.ProductCategoryID;
                    itms.MinAlertQty = model.MinAlertQty;
                    itms.SmallestUnitID = model.SmallestUnitID;
                    itms.IsActive = true;

                    itms.CreatedBy = SessionUser.UserID;
                    itms.CreatedDate = DateTime.Now;

                    _context.InventoryItems.Add(itms);
                    _context.SaveChanges();
                    return Ok(new
                    {
                        Success = true,
                        Message = "Data saved Successfully!!"
                    });



                }
                else
                {
                    var dbInfo = _context.InventoryItems
                         .Where(x => x.InventoryItemsId == model.InventoryItemsId)
                         .FirstOrDefault();

                    if (dbInfo == null)
                    {
                        return Ok(new
                        {
                            Success = false,
                            Message = "No Records Found!!"
                        });

                    }
                    else
                    {
                        dbInfo.ItemName = model.ItemName;
                        dbInfo.ItemCode = model.ItemCode;
                        dbInfo.ProductCategoryID = model.ProductCategoryID;
                        dbInfo.MinAlertQty = model.MinAlertQty;
                        dbInfo.SmallestUnitID = model.SmallestUnitID;
                        dbInfo.IsActive = true;

                        dbInfo.ModifiedBy = SessionUser.UserID;
                        dbInfo.ModifiedDate = DateTime.Now;

                        _context.SaveChanges();
                        return Ok(new
                        {
                            Success = true,
                            Message = "Data Modified Successfully!!"
                        });
                    }

                }
            }

        }

        public object GetData()
        {
            List<InventoryItemsVM> dbInfo = _context.InventoryItems
                .Where(x => x.IsActive == true)
                .Include(i => i.ProductCategory)
                .Include(i => i.Unit)
                .AsEnumerable()
                .Select(s => new InventoryItemsVM
                {
                    InventoryItemsId = s.InventoryItemsId,
                    ItemCode = s.ItemCode,
                    ItemName = s.ItemName,
                    MinAlertQty = s.MinAlertQty,
                    ProductCategoryID = s.ProductCategoryID,
                    SmallestUnitID = s.SmallestUnitID,
                    ProductCategoryName = s.ProductCategory == null ? "" : s.ProductCategory.CategoryName,
                    SmallestUnitName = s.Unit == null ? "" : s.Unit.UnitName
                })
                .ToList();
            return dbInfo;
        }

        public object DeleteData(int id)
        {
            InventoryItems dbInfo = _context.InventoryItems
                .Where(x => x.InventoryItemsId == id)
                .FirstOrDefault();
            if (dbInfo == null)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "No Items Found!!"
                });
            }
            else
            {
                dbInfo.IsActive = false;

                dbInfo.ModifiedBy = SessionUser.UserID;
                dbInfo.ModifiedDate = DateTime.Now;

                _context.SaveChanges();
                return Ok(new
                {
                    Success = true,
                    Message = "Data deleted Successfully!!!"
                });
            }
        }
        public object GetByID(int id)
        {
            InventoryItems dbInfo = _context.InventoryItems.Where(x => x.InventoryItemsId == id).FirstOrDefault();
            if (dbInfo == null)
            {
                return (new
                {
                    Success = false,
                    Message = "Data not found!!!"
                });
            }
            else
            {
                return new
                {
                    Success = true,
                    Message = "record found!!",
                    Data = dbInfo
                };
            }
        }


    }
}
