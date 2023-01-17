using System;
using System.Collections.Generic;

namespace WebAPI_Template.Models;

public partial class TodoList
{
    public int Id { get; set; }

    public string Event { get; set; } = null!;

    public string Enable { get; set; } = null!;

    public DateTime InsertTime { get; set; }

    public string InsertEmployeeId { get; set; } = null!;

    public DateTime UpdateTime { get; set; }

    public string UpdateEmployeeId { get; set; } = null!;

    public virtual ICollection<UploadFile> UploadFiles { get; } = new List<UploadFile>();
}
