﻿using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Forms
{
    public class BieuDo
    {
        public List<Thang> listDTTheoThang { set; get; }
        public List<TheLoai> listTheoTheLoai { set; get; }


        public BieuDo()
        {
            listDTTheoThang = Thang.createdDataFake();
            listTheoTheLoai = TheLoai.getDataThongKe();
        }

    }
    public class Thang
    {
        public string thang { set; get; }
        public long doanhThu { set; get; }

        public Thang(string thang, long doanhThu)
        {
            this.thang = thang;
            this.doanhThu = doanhThu;
        }

        public static List<Thang> createdDataFake()
        {
            List<Thang> list = new List<Thang>();
            list.Add(new Thang("1", 17));
            list.Add(new Thang("2", 21));
            list.Add(new Thang("3", 22));
            list.Add(new Thang("4", 22));
            list.Add(new Thang("5", 23));
            list.Add(new Thang("6", 19));
            list.Add(new Thang("7", (long) new RevenueEntityDAO().getEntity(DateTime.Now.Year, 7).Money));
            list.Add(new Thang("8", (null == new RevenueEntityDAO().getEntity(DateTime.Now.Year, 8)) ? 0L : (long)new RevenueEntityDAO().getEntity(DateTime.Now.Year, 8).Money));
            list.Add(new Thang("9", (null == new RevenueEntityDAO().getEntity(DateTime.Now.Year, 9)) ? 0L : (long)new RevenueEntityDAO().getEntity(DateTime.Now.Year, 9).Money));
            list.Add(new Thang("10", (null == new RevenueEntityDAO().getEntity(DateTime.Now.Year, 10)) ? 0L : (long)new RevenueEntityDAO().getEntity(DateTime.Now.Year, 10).Money));
            list.Add(new Thang("11", (null == new RevenueEntityDAO().getEntity(DateTime.Now.Year, 11)) ? 0L : (long)new RevenueEntityDAO().getEntity(DateTime.Now.Year, 11).Money));
            list.Add(new Thang("12", (null == new RevenueEntityDAO().getEntity(DateTime.Now.Year, 12)) ? 0L : (long)new RevenueEntityDAO().getEntity(DateTime.Now.Year, 12).Money));
            return list;
        }
    }
    public class TheLoai
    {
        public string ten { set; get; }
        public long soTien { set; get; }
        public int soLuong { set; get; }

        public TheLoai(string ten, long soTien, int soLuong)
        {
            this.ten = ten;
            this.soTien = soTien;
            this.soLuong = soLuong;
        }
        public TheLoai()
        {

        }

        //biet duoc brand, lay duoc cac don hang, tinh
        public void tinhTien(List<OrderEntity> lsOrdersByName)
        {
            this.soTien = 0;
        }

        public static List<TheLoai> createdDataFake()
        {
            List<TheLoai> ls = new List<TheLoai>();
            //samsung, xiaomi, huawei, realmi, oppo,vsmat, apple, nokia
            ls.Add(new TheLoai("SamSung", 175, 57));
            ls.Add(new TheLoai("Xiaomi", 150, 46));
            ls.Add(new TheLoai("Huawei", 70, 24));
            ls.Add(new TheLoai("Realmi", 17, 57));
            ls.Add(new TheLoai("Oppo", 12, 26));
            ls.Add(new TheLoai("VSmart", 15, 30));
            ls.Add(new TheLoai("Apple", 350, 25));
            ls.Add(new TheLoai("Nokia", 17, 17));
            return ls;
        }

        public static List<TheLoai> getDataThongKe()
        {
            List<TheLoai> ls = new List<TheLoai>();
            //samsung, xiaomi, huawei, realmi, oppo,vsmat, apple, nokia
            OrderDAO orderDAO = new OrderDAO();
            ls.Add(orderDAO.getDataThongKe("SAMSUNG"));
            ls.Add(orderDAO.getDataThongKe("XIAOMI"));
            ls.Add(orderDAO.getDataThongKe("REALME"));
            ls.Add(orderDAO.getDataThongKe("OPPO"));
            ls.Add(orderDAO.getDataThongKe("VSMART"));
            ls.Add(orderDAO.getDataThongKe("APPLE"));
            ls.Add(orderDAO.getDataThongKe("NOKIA"));

            return ls;
        }
    }
}
