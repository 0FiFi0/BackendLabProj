#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Models;

[Keyless]
public partial class university_year
{
    public int? university_id { get; set; }

    public int? year { get; set; }

    public int? num_students { get; set; }

    [Column(TypeName = "decimal(6, 2)")]
    public decimal? student_staff_ratio { get; set; }

    public int? pct_international_students { get; set; }

    public int? pct_female_students { get; set; }

    [ForeignKey("university_id")]
    public virtual university university { get; set; }
}