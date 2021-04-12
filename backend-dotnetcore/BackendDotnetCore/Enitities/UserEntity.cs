using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace BackendDotnetCore.Enitities
{
    //[Table("users")]
    public class UserEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("username")]
        [Require]
        public string Username { get; set; }
        [Column("password")]
        [Require]
        public string Password { get; set; }
        [Column("email")]
        [Require]
        public string Email { get; set; }

        [Column("provider")]
        public string Provider { get; set; }
        [Column("confirmed")]
        public int Confirmed { get; set; }
        [Column("blocked")]
        public int Blocked { get; set; }
        [Column("active")]
        public int Active { get; set; }
        
        public virtual ICollection<UserRole> UserRoles { get; set; }



      

        public bool checkUserInfo()
        {
            if(this.Username.Equals(" ") || this.Username.Equals(null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        

    }
}
