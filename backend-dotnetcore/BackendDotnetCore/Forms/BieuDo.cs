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
            listTheoTheLoai = TheLoai.createdDataFake();
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
            list.Add(new Thang("1", 170000000L));
            list.Add(new Thang("2", 210000000L));
            list.Add(new Thang("3", 227000000L));
            list.Add(new Thang("4", 227568000L));
            list.Add(new Thang("5", 231750000L));
            list.Add(new Thang("6", 197589000L));
            list.Add(new Thang("7", 210195000L));
            list.Add(new Thang("8", 198890000L));
            list.Add(new Thang("9", 221989000L));
            list.Add(new Thang("10", 250000000L));
            list.Add(new Thang("11", 191159000L));
            list.Add(new Thang("12", 285598500L));

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

        //biet duoc brand, lay duoc cac don hang, tinh
        public void tinhTien(List<OrderEntity> lsOrdersByName)
        {
            this.soTien = 0;
        }

        public static List<TheLoai> createdDataFake()
        {
            List<TheLoai> ls = new List<TheLoai>();
            //samsung, xiaomi, huawei, realmi, oppo,vsmat, apple, nokia
            ls.Add(new TheLoai("SamSung", 75559000L, 57));
            ls.Add(new TheLoai("Xiaomi", 57709000L, 46));
            ls.Add(new TheLoai("Huawei", 25590000L, 24));
            ls.Add(new TheLoai("Realmi", 75559000L, 57));
            ls.Add(new TheLoai("Oppo", 35597000L, 26));
            ls.Add(new TheLoai("VSmart", 34590000L, 30));
            ls.Add(new TheLoai("Apple", 105597000L, 25));
            ls.Add(new TheLoai("Nokia", 17457000L, 17));
            return ls;
        }
    }
}
