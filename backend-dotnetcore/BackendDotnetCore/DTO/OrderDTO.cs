using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.DTO
{
    public class OrderDTO

    {
        public Decimal TotalPrice { set; get; }
        public int TotalItem { set; get; }
        [JsonIgnore]
        public UserEntity User { set; get; }
       
        public IEnumerable<OrderItem2DTO> Items { set; get; }
        public OrderDTO(OrderEntity cartEntity)
        {
            
            TotalPrice = cartEntity.TotalPrice;
            TotalItem = cartEntity.TotalItem;
            User = cartEntity.User;
            Items = cartEntity.Items.Select(X=> new OrderItem2DTO(X));
        }

    }
    public class OrderItem2DTO
    {
        [JsonIgnore]
        public long Id { set; get; }
       
        public int Quantity { set; get; }
        [JsonIgnore]
        public bool Actived { set; get; }
        [JsonIgnore]
        public bool Deleted { set; get; }        
        public Product2 Product { set; get; }
        public OrderItem2DTO(OrderItemEntity cartItemEntity)
        {
            Id = cartItemEntity.Id;
            Quantity = cartItemEntity.Quantity;
            Actived = cartItemEntity.Actived;
            Deleted = cartItemEntity.Deleted;
            Product = cartItemEntity.Product;
          
            
        } 

    }

    
}
