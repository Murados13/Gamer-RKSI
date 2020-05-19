using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace app.Models
{
    public class LedgerClass
    {
        [Key]
        public int id { get; set; }
        public int Operation { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int CorOppId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime RegDate { get; set; }
        public int RegUserId { get; set; }

        public string Operation2Str()
        {
            switch (Operation)
            {
                case 1: return "Приход";
                case 2: return "Расход";
                default: return " ";
            }
        }

        public string RegDate2Str()
        {
            return RegDate.AddHours(3).ToString("yy.MM.dd HH:mm:ss");
        }

    }
}
