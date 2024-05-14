#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Models;

public partial class ranking_system
{
    [Key]
    public int id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string system_name { get; set; }

    [InverseProperty("ranking_system")]
    public virtual ICollection<ranking_criteria> ranking_criteria { get; set; } = new List<ranking_criteria>();
}