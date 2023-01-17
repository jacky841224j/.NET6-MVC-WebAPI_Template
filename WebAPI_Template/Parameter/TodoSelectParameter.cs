using System.Text.RegularExpressions;
using WebAPI_Template.Models;

namespace WebAPI_Template.Parameter
{
    public class TodoSelectParameter
    {
        public int? Id { get; set; }
        public string? Event { get; set; } 
        public string? Enable { get; set; } 
        public DateTime? InsertTime { get; set; }
        public string? InsertEmployeeId { get; set; } 
        public DateTime? UpdateTime { get; set; }
        public string? UpdateEmployeeId { get; set; }
        public int? minOrder { get; set; }
        public int? maxOrder { get; set; }
        public string Order
        {
            get { return "0"; }
            set
            {
                Regex regex = new Regex(@"^\d*-\d$");
                if (regex.Match(value).Success)
                {
                    minOrder = Int32.Parse(value.Split('-')[0]);
                    maxOrder = Int32.Parse(value.Split('-')[1]);
                }
            }
        }
    }
}
