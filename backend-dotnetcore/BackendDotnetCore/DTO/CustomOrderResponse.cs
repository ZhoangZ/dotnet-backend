using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.DTO
{
    public class CustomOrderResponse: IComparable<CustomOrderResponse>
    {
        public int id { set; get; }
        public string name { set; get; }
        public string address { set; get; }
        public string phone { set; get; }
        public string date { set; get; }
        public string status { set; get; }
        public int totalItems { set; get; }
        public string paymentType { set; get; }
        public string note { set; get; }
        public double lastPrice { set; get; }
        public ArrayList listItems { set; get; }
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
            cs.id = (int) orderEntity.Id;
            cs.name = name;
            cs.phone = phone;
            cs.address = orderEntity.AddressDelivery;
            cs.date = orderEntity.CreatedDate.ToString();
            cs.status = toStatusString(orderEntity.Status);
            cs.listItems = toListItemsResponse(orderEntity.Items);
            cs.totalItems = cs.listItems.Count;
            cs.paymentType = (orderEntity.PaymentId != 0) ? "VNPay" : "COD";
            cs.note = orderEntity.Note;
            cs.lastPrice = (double) orderEntity.TotalPrice;
            return cs;
        }


        public ArrayList toListItemsResponse(List<OrderItemEntity> items)
        {
            ArrayList listResponse = new ArrayList();
            foreach(OrderItemEntity oe in items)
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
        public int productID { set; get; }
        public string productImg { set; get; }
        public string productName { set; get; }
        public int quatity { set; get; }
        public double pricePerOne { set; get; }
        public double priceAll { set; get; }

        public CustomOrderItem()
        {

        }

        public double computePriceAllByProduct()
        {
            return pricePerOne * quatity;
        }

        public CustomOrderItem toCustomOrderItem(OrderItemEntity orderItemEntity)
        {
            CustomOrderItem csi = new CustomOrderItem();
            Product2DAO product2DAO = new Product2DAO();

            csi.productID = orderItemEntity.ProductId;
            Product2 productItem = product2DAO.getProduct(csi.productID);
            csi.productImg = productItem.Images.ToArray()[0].Image;
            csi.productName = productItem.Name;
            csi.pricePerOne = (double)productItem.OriginalPrice;
            csi.quatity = orderItemEntity.Quantity;
            csi.priceAll = csi.computePriceAllByProduct();

            return csi;
        }

    }
}
