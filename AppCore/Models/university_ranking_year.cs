#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Models;

public partial class university_ranking_year
{
    [Key]
    public int Id { get; set; }
    public int? university_id { get; set; }

    public int? ranking_criteria_id { get; set; }

    public int? year { get; set; }

    public int? score { get; set; }

    [ForeignKey("ranking_criteria_id")]
    public virtual ranking_criteria ranking_criteria { get; set; }

    [ForeignKey("university_id")]
    public virtual university university { get; set; }
}