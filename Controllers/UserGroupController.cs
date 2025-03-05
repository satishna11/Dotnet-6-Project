using inventory_re.DAO;
using inventory_re.Model;
using inventory_re.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace inventory_re.Controllers
{
    public class UserGroupController : BaseController
    {
        appDbContext _context;


        public UserGroupController(appDbContext context)
        {
            _context = context;
        }


        public IActionResult Create()
        {
            return View();
        }



        public IActionResult Index()
        {
            //List<UserGroup> datas =
            //    _context
            //    .UserGroup
            //    .Where(x => x.UserGroupCode == "TCH" || x.UserGroupCode == "AD")
            //    .OrderByDescending(o => o.UserGroupName)
            //    .ThenBy(t => t.UserGroupCode)
            //    .ToList();


            //var fst = _context.Users.First();

            ////.First()    .FirstOrDefault()    .Last()     .LastOrDefault()



            return View();
        }


        public object SaveData(int id, string groupname, string groupcode)
        {
            if (id == 0)
            {
                UserGroup ug;
                ug = new UserGroup();

                ug.UserGroupName = groupname;
                ug.UserGroupCode = groupcode;
                ug.IsActive = true;

                ug.CreatedBy = SessionUser.UserID;
                ug.CreatedDate = DateTime.Now;

                _context.UserGroup.Add(ug);
                _context.SaveChanges();


                return Ok(new
                {
                    Success = true,
                    Message = "User Group Data Save Success"
                });
            }
            else
            {
                // update case
                var dbInfo = _context.UserGroup.Where(x => x.UserGroupID == id).FirstOrDefault();
                if (dbInfo == null)
                {
                    return Ok(new
                    {
                        Success = false,
                        Message = "Data Information Not Found!"
                    });
                }
                else
                {
                    dbInfo.UserGroupName = groupname;
                    dbInfo.UserGroupCode = groupcode;

                    dbInfo.ModifiedBy = SessionUser.UserID;
                    dbInfo.ModifiedDate = DateTime.Now;

                    _context.SaveChanges();

                    return Ok(new
                    {
                        Success = true,
                        Message = "Data Modified Successfully!"
                    });
                }
            }
        }

        public object GetData(string groupName, string groupCode)
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
        public object DeleteData(int userGroupID)  // userGroupID = 20
        {
            UserGroup dbInfo = _context
                             .UserGroup
                              .Where(x => x.UserGroupID == userGroupID)
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

            dbInfo.ModifiedBy = SessionUser.UserID;
            dbInfo.ModifiedDate = DateTime.Now;

            _context.SaveChanges();

            return new
            {
                Success = true,
                Message = "Data Deleted Successfully!"
            };
        }


        public object GetByID(int id)
        {
            var dbInfo = _context.UserGroup.Where(x => x.UserGroupID == id).FirstOrDefault();
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
    }
}
