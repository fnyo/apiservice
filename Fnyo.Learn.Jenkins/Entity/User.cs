
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fnyo.Learn.Jenkins.Entity
{
    [Table("tms_user")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("user_name")]
        public string UserName { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("real_name")]
        public string RealName { get; set; }
        [Column("telephone")]
        public string Telephone { get; set; }

    }
}
