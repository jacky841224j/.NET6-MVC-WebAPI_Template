using System;
using System.Collections.Generic;

namespace WebAPI_Template.Models;

public partial class Employee
{
    public string EmpId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Position { get; set; } = null!;

    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<UploadFile> UploadFiles { get; } = new List<UploadFile>();
}
