#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Models;

public partial class ranking_criteria
{
    [Key]
    public int id { get; set; }

    public int? ranking_system_id { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string criteria_name { get; set; }

    [ForeignKey("ranking_system_id")]
    [InverseProperty("ranking_criteria")]
    public virtual ranking_system ranking_system { get; set; }

    public static implicit operator ranking_criteria(string v)
    {
        throw new NotImplementedException();
    }
}