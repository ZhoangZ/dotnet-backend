using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.DTO
{
    public class OrderDTO:OrderEntity

    {
      
        public IEnumerable<OrderItem2DTO> Items { set; get; }

       
        public OrderDTO(OrderEntity orderEntity)
        {
            Payment = orderEntity.Payment;
            TotalPrice = orderEntity.TotalPrice;
            TotalItem = orderEntity.TotalItem;
            User = orderEntity.User;
            Items = orderEntity.Items.Select(X=> new OrderItem2DTO(X));
            AddressDelivery = orderEntity.AddressDelivery;
            Email = orderEntity.Email;
            Phone = orderEntity.Phone;
            Fullname = orderEntity.Fullname;
            Note = orderEntity.Note;

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

        public int Idp
        {
            get
            {

                return Product.Id;
            }
        }
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
