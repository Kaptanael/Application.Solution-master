using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.Model
{
    [Table("Value")]
    public class Value
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
