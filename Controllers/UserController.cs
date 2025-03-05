using inventory_re.DAO;

using inventory_re.Model;
using inventory_re.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace inventory_re.Controllers
{
    public class UserController : Controller
    {
        appDbContext _context;

        public UserController(appDbContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {

            return View();
        }

        public object GetData(string UsrName, int UsrGroupId, string UsrContact)
        {
            List<inventory_re.Model.Users> obj = _context.Users
                               .Where(x =>
                               x.IsActive == true
                               && (string.IsNullOrEmpty(UsrName) || x.Username.Contains(UsrName))
                                       && (UsrGroupId == 0 || x.UserGroupID == UsrGroupId)


                                      && (string.IsNullOrEmpty(UsrContact) || x.ContactNo.Contains(UsrContact))


                                    )
                               .ToList();

            return obj;
        }

        public object GetByID(int id)
        {
            var dbInfo = _context.Users.Where(x => x.UserID == id).FirstOrDefault();
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

        [HttpPost]
        public object SaveData([FromBody] UsersVM model)
        {
            if (string.IsNullOrEmpty(model.Fullname))
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Fullname not found"
                });
            }
            else if (string.IsNullOrEmpty(model.Email))
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Email not found"
                });
            }
            //else if (model.Password != model.ConfirmPassword)
            //{
            //    return Ok(new
            //    {
            //        Success = false,
            //        Message = "Email not found"
            //    });
            //}
            //else
            //{
            //    if (model.UserID == 0)
            //    {

            //        {
            //            Users u;
            //            u = new Users();
            //            u.Fullname = model.Fullname;
            //            u.ContactNo = model.ContactNo;
            //            u.Email = model.Email;
            //            u.Password = model.Password;
            //            u.UserGroupID = model.UserGroupID;
            //            u.Username = model.Username;
            //            u.ValidFrom = null;
            //            u.ValidTo = null;
            //            u.IsActive = true;
            //            _context.Users.Add(u);
            //            _context.SaveChanges();
            //            return Ok(new
            //            {
            //                success = true,
            //                message = "data saved successfully"
            //            });
            //        }
            //    }
                else
                {
                    var dbinfo = _context
                            .Users
                            .Where(x => x.UserID== model.UserID)
                            .FirstOrDefault();
                    if (dbinfo == null)
                    {
                        return Ok(new
                        {
                            Success = false,
                            Message = "No Records Found!!"
                        });

                    }
                    else
                    {
                        dbinfo.Fullname = model.Fullname;
                        dbinfo.Email = model.Email;
                        dbinfo.ContactNo = model.ContactNo;
                       
                        dbinfo.UserGroupID = model.UserGroupID;
                        dbinfo.Username = model.Username;
                       
                        dbinfo.IsActive = true;
                        _context.SaveChanges();
                        return Ok(new
                        {
                            Success = true,
                            Message = "Data Modified Successfully!!"
                        });
                    }
                }
            }


            //    var existUser = _context
            //                .Users
            //                .Where(x => x.Username == u.Username)
            //                .FirstOrDefault();
            //if (existUser != null)
            //{
            //    return Ok(new
            //    {
            //        Success = false,
            //        Message = "Username Exists Already!!!"
            //    });
            //}


            //_context.Users.Add(u);
            //_context.SaveChanges();


            //return Ok(new
            //{
            //    Success = true,
            //    Message = "User Registered Successfully!!!"
            //});
        //}


        public object DeleteData(int UsrID)
        {
            inventory_re.Model.Users dbInfo = _context
                             .Users
                              .Where(x => x.UserID == UsrID)
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
   



