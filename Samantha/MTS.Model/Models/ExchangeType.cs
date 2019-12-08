using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTS.Model.Models
{
    public class ExchangeType
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Type Name is a required field!")]
        [MaxLength(150, ErrorMessage = "Name can't be more than 150 charecters")]
        public string TypeName { get; set; }
        public string Remarks { get; set; }
    }
}
