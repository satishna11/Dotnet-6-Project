using inventory_re.DAO;
using inventory_re.Model;
using inventory_re.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    public class AuthController : Controller
    {
        appDbContext _context;
        public AuthController(appDbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            HttpContext.Session.Clear();

            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public object RegisterNewUser([FromBody] UsersVM model)
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
            else if (model.Password != model.ConfirmPassword)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Email not found"
                });
            }
            else
             if (model.UserID == 0)
            {
                Users u;
                u = new Users()
                {
                    Fullname = model.Fullname,
                    ContactNo = model.ContactNo,
                    Email = model.Email,
                    Password = model.Password,
                    UserGroupID = model.UserGroupID,
                    Username = model.Username,

                    ValidFrom = null,
                    ValidTo = null,
                    IsActive = true
                };

                //var existUser = _context
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


                _context.Users.Add(u);
                _context.SaveChanges();


                return Ok(new
                {
                    Success = true,
                    Message = "User Registered Successfully!!!"
                });
            }
            else
            {
                var dbinfo = _context
                            .Users
                            .Where(x => x.UserID == model.UserID)
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
                    dbinfo.Password = model.Password;
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

        public object GetUserGroupData(string groupName, string groupCode)
        {

            List<UserGroup> obj = _context
                                    .UserGroup
                                    //.Where(x => x.IsActive == true)
                                    .Where(x =>
                                        x.IsActive == true
                                        && (string.IsNullOrEmpty(groupName) || x.UserGroupName.Contains(groupName))
                                        && (string.IsNullOrEmpty(groupCode) || x.UserGroupCode.Contains(groupCode))
                                    )
                                    .ToList();

            return obj;

        }
        [HttpPost]
        public object DoLogin([FromBody] LoginVM model)
        {
            if (string.IsNullOrEmpty(model.Username))
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Username Not Found!!!"
                });
            }
            else if (string.IsNullOrEmpty(model.Password))
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Password Not Found!!!"
                });
            }
            else
            {
                var dbUser = _context
                                .Users
                                .Where(x => x.Username == model.Username)
                                .FirstOrDefault();

                if (dbUser == null)
                {
                    return Ok(new
                    {
                        Success = false,
                        Message = "Username does not exists!!!"
                    });
                }
                else
                {
                    if (dbUser.IsActive == false)
                    {
                        return Ok(new
                        {
                            Success = false,
                            Message = "User Not Active!!!"
                        });
                    }
                    else if (dbUser.Password != model.Password)
                    {
                        return Ok(new
                        {
                            Success = false,
                            Message = "Password Not Matched!!!"
                        });
                    }
                    else
                    {
                        // store user info in SESSION
                        HttpContext.Session.SetString("USER_INFO", Newtonsoft.Json.JsonConvert.SerializeObject(dbUser));

                        return Ok(new
                        {
                            Success = true,
                            Message = "User Found!!!",
                            data = dbUser
                        });

                    }

                }
            }
        }
    }
}