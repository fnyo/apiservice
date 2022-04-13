using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fnyo.Learn.Jenkins.Entity
{
    [Table("tms_student")]
    public class Student
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("Name")]
        [Required]
        public string Name { get; set; }
        [Column("age")]
        [Range(0,150)]
        public int Age { get; set; }
        [Column("address")]
        public string Address { get; set; } 

        [Column("number")]
        public string Number { get; set; }
        [Column("sex")]
        public string Sex { get; set; }

        [Column("class")]
        public string Class { get; set; }

        [Column("tel")]
        public string Tel { get; set; }

    }
}
