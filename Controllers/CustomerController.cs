using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventory_re.DAO;
using inventory_re.Models;
using inventory_re.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace inventory_re.Controllers
{
    public class CustomerController : BaseController
    {
        appDbContext _context;
        public CustomerController(appDbContext context)
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
        public object SaveData([FromBody] CustomerVM model)
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
            else if (model.VAT <= 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Please enter a valid unit "
                });
            }
            else
            {
                if (model.CustomerId == 0)
                {

                    Customer c1 = new Customer();
                    c1.FullName = model.FullName;
                    c1.Email = model.Email;
                    c1.Contact = model.Contact;
                    c1.VAT = model.VAT;
                    c1.IsActive = true;

                    c1.CreatedBy = SessionUser.UserID;
                    c1.CreatedDate = DateTime.Now;

                    _context.Customer.Add(c1);
                    _context.SaveChanges();
                    return Ok(new
                    {
                        Success = true,
                        Message = "Data saved Successfully!!"
                    });



                }
                else
                {
                    var dbInfo = _context.Customer.Where(x => x.CustomerId == model.CustomerId)
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
                        dbInfo.FullName = model.FullName;
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
        public object GetData()
        {

            List<Customer> obj = _context.Customer
                    .Where(x => x.IsActive == true)
                    .ToList();
            return obj;

        }
        public object GetByID(int id)
        {
            Customer dbInfo = _context.Customer.Where(x => x.CustomerId == id).FirstOrDefault();
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
            Customer dbInfo = _context.Customer
                .Where(x => x.CustomerId == id)
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
                    Message = "Customer Deleted Successfully!!"
                };


            }

        }

    }
}
