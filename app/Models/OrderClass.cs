using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace app.Models
{
    public class OrderClass
    {
        [Key]
        public int id { get; set; }
        public int Operation { get; set; }
        public int State { get; set; }

        public string Code1 { get; set; }
        public decimal Amount { get; set; }

        public string Code2 { get; set; }
        public decimal Price { get; set; }

        public string Description { get; set; }
        public int UserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime RegDate { get; set; }

        public string Operation2Str()
        {
            switch (Operation)
            {
                case 1: return "Покупка";
                case 2: return "Продажа";
                default: return " ";
            }
        }

        public string RegDate2Str()
        {
            return RegDate.AddHours(3).ToString("yy.MM.dd HH:mm:ss");
        }

        public OrderClass()
        {
            State = 0;
            Description = "Заявка";
            RegDate = DateTime.Now;
        }

    }
}
