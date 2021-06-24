# dotnet-backend 2021

Hamachi:

- Network name

```
> DOTNET-NLU-2021
```

- Password

```
> DOTNET
```
[Trang chủ VNPAY Sandbox]( https://sandbox.vnpayment.vn/merchantv2/Home/Dashboard.htm)

VNPAY:

- Username

```
> hearterzouest99.999@gmail.com
```

- Password

```
> aA@12345
```

Tài khoản ngân hàng dùng để test thanh toán

<table>
     <thead>
        <tr>
            <th colspan=2 align="left">Ngân hàng NCB</th>           
        </tr>
    </thead>
  <tbody>
        <tr>
            <td >Số thẻ</td>
            <td >9704198526191432198</td>        
        </tr>   
        <tr>
            <td >Tên chủ thẻ</td>
            <td >NGUYEN VAN A</td>        
        </tr>   
         <tr>
            <td >Ngày phát hành</td>
            <td >07/15</td>        
        </tr>   
         <tr>
            <td >Mật khẩu OTP</td>
            <td >123456</td>        
        </tr>       
    </tbody>
</table>


[Danh sách tài khoản test]( https://sandbox.vnpayment.vn/apis/vnpay-demo/)


## Kiểm tra api trên front-end

| API           | Status |
| ------------- | ------ |
| /products | OK  |
| /products/{number} | Bug  |
| / | Bug  |

- [x] Đã fix
- [ ] /products/103 Hình không thấy
- [ ] / Không click vào tên sản phẩm để vào trang xem sản phẩm được

## Kiểm tra console
- [ ] Faulture writeline console
