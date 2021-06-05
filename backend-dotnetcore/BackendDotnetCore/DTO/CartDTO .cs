using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.DTO
{
    public class CartDTO

    {
        

        public Decimal TotalPrice { set; get; }
        public int TotalItem { set; get; }
        [JsonIgnore]
        public UserEntity User { set; get; }

        public IEnumerable<CartItem2DTO> Items { set; get; }
        public CartDTO(CartEntity cartEntity)
        {
            
            TotalPrice = cartEntity.TotalPrice;
            TotalItem = cartEntity.TotalItem;
            User = cartEntity.User;
            Items = cartEntity.Items.Select(X=> new CartItem2DTO(X));
        }

    }
    public class CartItem2DTO
    {
        public long Id { set; get; }
        public int Quantity { set; get; }
        public bool Actived { set; get; }        
        public bool Deleted { set; get; }        
        public Product2 Product { set; get; }
        public CartItem2DTO(CartItemEntity cartItemEntity)
        {
            Id = cartItemEntity.Id;
            Quantity = cartItemEntity.Amount;
            Actived = cartItemEntity.Actived;
            Deleted = cartItemEntity.Deleted;
            
            Product = cartItemEntity.ProductSpecific.Product;
            Product.Specific = cartItemEntity.ProductSpecific;
            Product.Specifics = null;
            
        } 

    }

   
    
}
