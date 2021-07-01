using BackendDotnetCore.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.Entities
{
    [Table("order")]   
    public class OrderEntity
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { set; get; }

        [Column("order_status")]
        public int Status { set; get; }

        [Column("created_date")]
        public DateTime CreatedDate { set; get; }

        [Column("address_delivery")]
        public string AddressDelivery { set; get; }

        [Column("name_consumer")]
        public string Fullname { set; get; } 
        [Column("phone_number")]
        public string Phone { set; get; }

        [Column("email")]
        public string Email { set; get; }

        [Column("note")]
        public string Note { set; get; }

        [Column("total_price")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalPrice { set; get; }

        [Column("total_item")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int TotalItem { set; get; }

        [Column("user_id")]
        [JsonIgnore]
        public int UserId { set; get; }      
        public virtual UserEntity User { set; get; }

        [Column("payment_id")]
       
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? PaymentId { set; get; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual PaymentEntity Payment { set; get; }

        [Column("cod")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool Cod { set; get; }


        public virtual List<OrderItemEntity> Items { set; get; }

        public enum StatusString
        {
             PENDING,
             DELIVERING,
             FINISH,
             DENY
        }

        public OrderEntity()
        {
            TotalPrice = 0;
            TotalItem = 0;
        }


        //get list status
        public static List<MyStatusOrder> GetListStatusOrders()
        {
            List<MyStatusOrder> ls = new List<MyStatusOrder>();
            int i = 0;
            foreach (StatusString s in (StatusString[])Enum.GetValues(typeof(StatusString)))
            {
                MyStatusOrder ms = new MyStatusOrder();
                ms.id = ++i;
                switch (s)
                {
                    case StatusString.PENDING:
                        ms.statusString = "Đang tiếp nhận";
                        break;
                    case StatusString.DELIVERING:
                        ms.statusString = "Đang vận chuyển";
                        break;
                    case StatusString.FINISH:
                        ms.statusString = "Đã giao hàng";
                        break;
                    case StatusString.DENY:
                        ms.statusString = "Hủy đơn hàng";
                        break;
                }
                ls.Add(ms);
            }
            return ls;
        }
    }
}
