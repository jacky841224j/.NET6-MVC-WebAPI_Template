using System;
using System.Collections.Generic;

namespace WebAPI_Template.Models;

public partial class UploadFile
{
    public int Id { get; set; }

    public int ListId { get; set; }

    public string FileName { get; set; }

    public string Src { get; set; }

    public string EmpId { get; set; }

    public virtual Employee? Emp { get; set; }

    public virtual TodoList? List { get; set; }
}
