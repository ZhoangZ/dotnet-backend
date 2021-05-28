using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace BackendDotnetCore.Entities
{
    //[Table("users")]
    public class UserEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("username")]
        [Require]
        public string Username { get; set; }
        [Column("password")]
        [Require]   
        [JsonIgnore]
        public string Password { set; get; }
        [Column("email")]
        [Require]
        public string Email { get; set; }
        [Column("fullname")]
        public string Fullname { set; get; }
        [Column("phone")]
        public string phone { set; get; }
        [Column("address")]
        public string address { set; get; }
        [Column("provider")]
        public string Provider { get; set; }
        [Column("confirmed")]
        public int Confirmed { get; set; }
        [Column("blocked")]
        public int Blocked { get; set; }
        [Column("active")]
        public int Active { get; set; }
        [Column("avatar")]
        public string Avatar { set; get; }
        
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual CommentEntity comment { set; get; }

        /* public virtual RoleEntity roleUpdate { get; set; }
         public virtual RoleEntity roleCreate { get; set; }*/

       
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
