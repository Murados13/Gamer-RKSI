using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace app.Models
{
    [Table("players")]
    public class PlayerClass
    {
        public int id { get; set; }
        public string Email { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped,
         System.ComponentModel.DefaultValue(0.0)]
        public decimal Amount { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        public byte Blocked { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime RegDate { get; set; }

        public string Blocked2Str()
        {
            return Blocked > 0 ? "Да" : "Нет";
        }

        public string RegDate2Str()
        {
            return RegDate.AddHours(3).ToString("yy.MM.dd HH:mm:ss");
        }
    }
}
