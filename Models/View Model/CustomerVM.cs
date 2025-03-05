using System;
namespace inventory_re.Models.ViewModel
{
	public class CustomerVM
	{

        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public double VAT { get; set; }
    }
}

