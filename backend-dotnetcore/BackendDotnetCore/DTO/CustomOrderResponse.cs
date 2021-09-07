using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.DTO
{
    public class CustomOrderResponse : IComparable<CustomOrderResponse>
    {
        public int id { set; get; }
        public string name { set; get; }
        public string address { set; get; }
        public string phone { set; get; }
        public string date { set; get; }
        public int statusID { set; get; }
        public MyStatusOrder status { set; get; }
        public int totalItems { set; get; }
        public string paymentType { set; get; }
        public string note { set; get; }
        public double lastPrice { set; get; }
        public ArrayList cartItems { set; get; }
        public ArrayList comments { set; get; }//moi them 609

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string transactionStatus { set; get; }
        public CustomOrderResponse()
        {

        }

        public string toStatusString(int statusID)
        {
            string statusString = "";
            switch (statusID)
            {
                case 1:
                    statusString = "Đang tiếp nhận";
                    break;
                case 2:
                    statusString = "Đang vận chuyển";
                    break;
                case 3:
                    statusString = "Đã giao hàng";
                    break;
                case 4:
                    statusString = "Hủy đơn hàng";
                    break;
            }
            return statusString;
        }

        public CustomOrderResponse toOrderResponse(OrderEntity orderEntity)
        {
            CustomOrderResponse cs = new CustomOrderResponse();
            cs.id = (int)orderEntity.Id;
            cs.name = orderEntity.Fullname;
            cs.phone = orderEntity.Phone;
            cs.address = orderEntity.AddressDelivery;
            cs.date = orderEntity.CreatedDate.ToString();
            cs.status = new MyStatusOrder(orderEntity.Status, toStatusString(orderEntity.Status));
            cs.statusID = orderEntity.Status;
            cs.cartItems = toListItemsResponse(orderEntity.Items);
            cs.totalItems = cs.cartItems.Count;
            cs.paymentType = orderEntity.Cod == true ? "COD" : "VNPay";
            cs.note = orderEntity.Note;
            cs.lastPrice = (double)orderEntity.TotalPrice;
            cs.comments = orderEntity.Comments==null?new ArrayList(): new ArrayList(orderEntity.Comments);//moi them 06092021
            if (orderEntity.Payment != null)
            {
                cs.transactionStatus = (orderEntity.Payment.TransactionStatus.ToString() != null) ? orderEntity.Payment.TransactionStatus.ToString() : null;
            }
            if (cs.transactionStatus != null && cs.transactionStatus.Equals("PENDING"))
            {
                cs.status = new MyStatusOrder(4, toStatusString(4));//huy
                cs.statusID = 4;
            }

            return cs;
        }

        public List<CustomOrderResponse> toListCustomOrderResponse(List<OrderEntity> listOE)
        {
            List<CustomOrderResponse> list = new List<CustomOrderResponse>();
            foreach (OrderEntity oe in listOE)
            {
                list.Add(toOrderResponse(oe));
            }
            return list;
        }


        public ArrayList toListItemsResponse(ICollection<OrderItemEntity> items)
        {
            ArrayList listResponse = new ArrayList();
            foreach (OrderItemEntity oe in items)
            {
                CustomOrderItem csi = new CustomOrderItem();
                listResponse.Add(csi.toCustomOrderItem(oe));
            }
            return listResponse;
        }

        public int CompareTo(CustomOrderResponse other)
        {
            if (other.id > id) return -1;
            if (other.id == id) return 0;
            return 1;
        }
    }
    public class CustomOrderItem
    {
        public int idp { set; get; }
        public int quantity { set; get; }
        public Product2 product { set; get; }
       
        public CustomOrderItem()
        {

        }
       
        public CustomOrderItem toCustomOrderItem(OrderItemEntity orderItemEntity)
        {
            CustomOrderItem csi = new CustomOrderItem();
            Product2DAO product2DAO = new Product2DAO();

            csi.idp = orderItemEntity.ProductId;
            Product2 productItem = product2DAO.getProduct(csi.idp);
            List<Image> ls = new List<Image>();
            ls.Add(new Image(productItem.Images.ToArray()[0].Image));
            csi.quantity = orderItemEntity.Quantity;
            ProductObjItem productObj = new ProductObjItem();
            productObj.quantity = orderItemEntity.Quantity;
            productObj.images = ls;
            productObj.name = productItem.Name;
            productObj.salePrice = (double)productItem.SalePrice;
            productObj.priceAll = productObj.computePriceAllByProduct();
            csi.product = productItem;
            

            return csi;
        }

    }
    public class ProductObjItem{
        public List<Image> images { set; get; }
        public string name { set; get; }
        public int quantity { set; get; }
        public int promotionPercents { set; get; }
        public double originalPrice { set; get; }
        public double salePrice { set; get; }
        public double priceAll { set; get; }

        public double computePriceAllByProduct()
        {
            return salePrice * quantity;
        }

    }

    public class Image
    {
        public string image { get; set; }

        public Image(string image)
        {
            this.image = image;
        }
    }
}
