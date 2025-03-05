using System;
using Microsoft.AspNetCore.Mvc;

namespace inventory_re.Controllers
{
	public class Profilecontroller :Controller
	{
        public IActionResult Create()
        {
            return View();
        }
    }

}

