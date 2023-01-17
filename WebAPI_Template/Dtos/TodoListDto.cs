using WebAPI_Template.Models;

namespace WebAPI_Template.Dtos
{
    public class TodoListDto
    {
        public int Id { get; set; }
        public string Event { get; set; } 
        public string Enable { get; set; } 
        //public DateTime InsertTime { get; set; }
        public string InsertEmployeeId { get; set; } 
        //public DateTime UpdateTime { get; set; }
        public string UpdateEmployeeId { get; set; } 

        public ICollection<UploadFileDto> UploadFiles { get; set; }
    }
}
