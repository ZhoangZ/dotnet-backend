using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.DTO
{
    public class CartDTO:CartEntity

    {

       

        public IEnumerable<CartItem2DTO> Items { set; get; }
        public CartDTO(CartEntity cartEntity)
        {
            
            TotalPrice = cartEntity.TotalPrice;
            TotalItem = cartEntity.TotalItem;
            User = cartEntity.User;
            Items = cartEntity.Items.Select(X=> new CartItem2DTO(X));
        }

    }
    public class CartItem2DTO:CartItemEntity
    {
               
        public int Idp
        {
            get
            {
                
                return Product.Id;
            }
        }
        public CartItem2DTO(CartItemEntity cartItemEntity)
        {
            Id = cartItemEntity.Id;
            Quantity = cartItemEntity.Quantity;
            Actived = cartItemEntity.Actived;
            Deleted = cartItemEntity.Deleted;            
            Product = cartItemEntity.Product;
          
            
        } 

    }

   
    
}
