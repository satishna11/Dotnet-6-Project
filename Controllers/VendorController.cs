using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using inventory_re.DAO;
using inventory_re.Models;
using inventory_re.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace inventory_re.Controllers
{

    public class VendorController : BaseController
    {
        appDbContext _context;
        public VendorController(appDbContext context)
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
        public object SaveData([FromBody] VendorVM model)
        {
            if (string.IsNullOrEmpty(model.FullName))
            {
                return Ok(new
                {
                    Success = false,
                    Message = "invalid name"
                });
            }
            else if (string.IsNullOrEmpty(model.Email))
            {
                return Ok(new
                {
                    Success = false,
                    Message = "invalid Item Code"
                });
            }
            else if (string.IsNullOrEmpty(model.Contact))
            {
                return Ok(new
                {
                    Success = false,
                    Message = "invalid Product Category "
                });
            }
            else if (string.IsNullOrEmpty(model.VAT))
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Please enter a valid unit "
                });
            }
            else
            {
                if (model.VendorId == 0)
                {

                    Vendor v1 = new Vendor();
                    v1.Fullname = model.FullName;
                    v1.Email = model.Email;
                    v1.Contact = model.Contact;
                    v1.VAT = model.VAT;
                    v1.CreatedBy = SessionUser.UserID;
                    v1.CreatedDate = DateTime.Now;
                    v1.IsActive = true;
                    _context.Vendor.Add(v1);
                    _context.SaveChanges();
                    return Ok(new
                    {
                        Success = true,
                        Message = "Data saved Successfully!!"
                    });



                }
                else
                {
                    var dbInfo = _context.Vendor.Where(x => x.VendorId == model.VendorId)
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
                        dbInfo.Fullname = model.FullName;
                        dbInfo.Email = model.Email;
                        dbInfo.Contact = model.Contact;
                        dbInfo.VAT = model.VAT;
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
        public object GetData(string fullName, string email, string contact, string VAT)
        {

            List<Vendor> obj = _context.Vendor
                    .Where(x => x.IsActive == true
                    && (string.IsNullOrEmpty(fullName) || x.Fullname.Contains(fullName))
                    && (string.IsNullOrEmpty(email) || x.Email.Contains(email))
                    && (string.IsNullOrEmpty(contact) || x.Contact.Contains(contact))
                    && (string.IsNullOrEmpty(VAT) || x.Contact.Contains(VAT))
            )
            .ToList();
            return obj;

        }
        public object GetByID(int id)
        {
            Vendor dbInfo = _context.Vendor.Where(x => x.VendorId == id).FirstOrDefault();
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
        public object DeleteData(int id)
        {
            Vendor dbInfo = _context.Vendor
                .Where(x => x.VendorId == id)
                .FirstOrDefault();

            if (dbInfo == null)
            {
                return new
                {
                    Success = false,
                    Message = "No data found!!"
                };
            }
            else
            {
                dbInfo.IsActive = false;
                dbInfo.ModifiedBy = SessionUser.UserID;
                dbInfo.ModifiedDate = DateTime.Now;
                _context.SaveChanges();
                return new
                {
                    Success = true,
                    Message = "Vendor Deleted Successfully!!"
                };


            }

        }
    }
}

