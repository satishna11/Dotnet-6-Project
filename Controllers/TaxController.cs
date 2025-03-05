using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Controllers;
using inventory_re.DAO;
using inventory_re.Models;
using inventory_re.Controllers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace inventory_re.Controllers
{
    public class TaxController : BaseController
    {
        appDbContext _context;

        public TaxController(appDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult create()
        {
            return View();
        }
        public object SaveData(int id, string taxname, string taxcode, int taxpercent)
        {
            if (id == 0)
            {
                Tax t;
                t = new Tax();
                t.TaxName = taxname;
                t.TaxID = id;
                t.TaxCode = taxcode;
                t.Percent = taxpercent;
                t.IsActive = true;
                _context.Tax.Add(t);
                _context.SaveChanges();
                return Ok(new
                {
                    Success = true,
                    Message = "Data save success"

                });
            }
            else
            {
                var dbInfo = _context.Tax.Where(x => x.TaxID == id).FirstOrDefault();
                if (dbInfo == null)
                {
                    return Ok(new
                    {
                        Success = false,
                        Message = "Data information not found"

                    });

                }
                else
                {
                    dbInfo.TaxName = taxname;
                    dbInfo.TaxCode = taxcode;
                    dbInfo.Percent = taxpercent;
                    _context.SaveChanges();
                    return Ok(new
                    {
                        Success = true,
                        Message = "Data information  found"

                    });


                }
            }

        }

        public object GetData(string txName, string txCode, int txPercent)
        {

            List<Tax> obj = _context.Tax
                           .Where(x =>
                                    x.IsActive == true
                                    && (string.IsNullOrEmpty(txName) || x.TaxName.Contains(txName))
                                    && (string.IsNullOrEmpty(txCode) || x.TaxCode.Contains(txCode))
                                    && (txPercent == 0 || x.Percent == txPercent)


                                )
                           .ToList();

            return obj;
        }

        public object GetByID(int id)
        {
            var dbInfo = _context.Tax.Where(x => x.TaxID == id).FirstOrDefault();
            if (dbInfo == null)
            {
                return new
                {
                    Success = false,
                    Message = "Data Information Not Found!"
                };
            }
            else
            {
                return new
                {
                    Success = true,
                    Message = "Data Found",
                    Data = dbInfo
                };
            }
        }
        public object DeleteData(int txID)
        {
            Tax dbInfo = _context
                             .Tax
                              .Where(x => x.TaxID == txID)
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


    }

}

