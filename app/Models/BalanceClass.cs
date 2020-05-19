using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace app.Models
{
    public class BalanceClass
    {
        [Key]
        public string Code { get; set; }
        public int UserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [System.ComponentModel.DefaultValue(0.0)]
        public decimal Amount { get; set; }
    }
}
