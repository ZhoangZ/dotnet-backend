using Microsoft.AspNetCore.Http;
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
        [Column("blocked")]
        public int Blocked { get; set; }
        [Column("active")]
        public int Active { get; set; }
        [Column("avatar")]
        public string Avatar { set; get; }
        [Column("opt")]
        public string optCode { set; get; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
        public virtual ICollection<CommentEntity> comments { set; get; } = new List<CommentEntity>();
        //public virtual ICollection<CartItemEntity> CartItemEntities { set; get; } = new HashSet<CartItemEntity>();

        //public virtual CartEntity Cart { set; get; }
        [NotMapped]
        public bool IsAdmin
        {
            get { 
            if (UserRoles != null)
            {
                foreach(UserRole u in UserRoles)
                {
                    if (u.Role != null)
                    {
                        if (u.Role.Name.Equals("ADMIN")) return true;
                    }
                }
            }
            return false;}
        }
        [NotMapped]
        public static string EmailAdminFinal = "trandiem1006@gmail.com";

        public bool updateRole(RoleEntity role)
        {
            UserRoles = new List<UserRole>();
            UserRole ur = new UserRole();
            ur.Role = role;
            UserRoles.Add(ur);

            return this.UserRoles.Count > 0 ? true : false;
        }

       
    }
}
