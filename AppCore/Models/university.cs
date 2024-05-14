#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Models;

public partial class university
{
    [Key]
    public int id { get; set; }

    public int? country_id { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string university_name { get; set; }

    [ForeignKey("country_id")]
    [InverseProperty("university")]
    public virtual country country { get; set; }
}