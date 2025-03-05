using inventory_re.Controllers;
using inventory_re.DAO;
using inventory_re.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Inventory.Controllers
{
    public class HomeController : BaseController
    {
        appDbContext _context;
        public HomeController(appDbContext context)
        {
            _context = context;
        }

        public void Jump()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public object GetUserGroupChartData()
        {
            var usr = _context
                .Users
                .Where(x => x.IsActive == true)
                .ToList();
            var usrGroup = _context
                            .UserGroup
                            .Where(x => x.IsActive == true)
                            .ToList();

            var obj = new
            {
                User = usr,
                UserGroup = usrGroup
            };

            addNumber(5, 6);

            return Ok(obj);
        }

        int addNumber(int fn, int sn, string tn = "asdfasf")
        {
            int result = fn + sn;
            return result;
        }


    }



    public partial class Bird
    {
        public string Fullname { get; set; }
        public string Color { get; set; }

    }


    public partial class Bird
    {
        public decimal Weight { get; set; }
        public bool CanFLy { get; set; }
    }
}
