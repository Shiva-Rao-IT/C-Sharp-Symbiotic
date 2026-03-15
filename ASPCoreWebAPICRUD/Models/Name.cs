using System;
using System.Collections.Generic;

namespace ASPCoreWebAPICRUD.Models;

public partial class Name
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? SecondName { get; set; }
}
