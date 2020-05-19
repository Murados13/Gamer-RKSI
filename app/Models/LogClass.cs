using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models
{
    public class LogClass
    {
        public int id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime RegDate { get; set; }

        public LogClass(String code, String description)
        {
            Code = code;
            Description = description;
        }

        public string RegDate2Str()
        {
            return RegDate.AddHours(3).ToString("yy.MM.dd HH:mm:ss");
        }

    }
}
