using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace app.Models
{
    public class CurrencyClass
    {
        [Key]
        public string Code { get; set; }
        public string Description { get; set; }
        public string License { get; set; }

        [JsonIgnore]
        public string TokenId { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime RegDate { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [System.ComponentModel.DefaultValue(0.0)]
        public decimal Amount { get; set; }

        public string RegDate2Str()
        {
            return RegDate.AddHours(3).ToString("yy.MM.dd HH:mm:ss");
        }
    }
}
