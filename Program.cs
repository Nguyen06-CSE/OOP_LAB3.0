using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LAB3.DanhSachSinhVien;

namespace LAB3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //SinhVien sinhVien1 = new SinhVien();
            //SinhVien sinhVien2 = new SinhVien("001", "Tran Van An", 3, false, "DLK48B");
            //SinhVien sinhVien3 = new SinhVien("003, Nguyen Van Thinh, 8.8, Nam, CTK48B");
            //SinhVien sinhVien4 = new SinhVien("002, Nguyen Van A, 8.8, Nam, CTKU3");
            //SinhVien sinhVien5 = new SinhVien("001", "Tran Thi Anh", 3, true, "DLK48A");

            //Console.WriteLine(sinhVien1.ToString());
            //Console.WriteLine(sinhVien2.ToString());
            //Console.WriteLine(sinhVien3.ToString());
            //Console.WriteLine(sinhVien4.ToString());
            //Console.WriteLine(sinhVien5.ToString());

            DanhSachSinhVien ds = new DanhSachSinhVien();
            ds.NhapTuFile(); // Đọc danh sách sinh viên từ file

            ChayChuongTrinh(ds);


        }
        public static void XuatMenu()
        {
            Console.WriteLine("1.\tĐếm số lượng sinh viên Nam trong lớp\r\n2.\tĐếm số lượng sinh viên Nu trong lớp\r\n3.\tHiển thị danh sách sinh viên theo chiếu tăng, giảm của điểm trung bình\r\n4.\tTim danh sách sinh viên có điểm trung bình cao nhất\r\n5.\tTìm lớp có sinh viên có điểm trung bình cao nhất\r\n6.\tTìm lớp có sinh viên không có điểm trung bình cao nhất\r\n7.\tHiển thị danh sách sinh viên theo lớp và theo chiều giảm của điểm trung bình\r\n8.\tXếp hạng sinh viên của lớp\r\n9.\tTìm lớp có tổng điểm trung bình cao nhất, thấp nhất\r\n10.\tTìm lớp có nhiều sinh viên giỏi nhất\r\n11.\tTìm lớp có nhiều (hoặc ít) sinh viên theo loại yếu, trung bình, khá\r\n12.\tGhi xuống file danh sách lớp \r\n13.\tTìm lớp không có sinh viên nữ\r\n14.\tTìm lớp không có sinh viên nam\r\n15.\tĐếm số lượng sinh viên theo lớp\r\n16.\tXóa tất cả sinh viên của lớp nào đó\r\n17.\tXếp loại sinh viên dựa trên điểm trung bình\r\n");
        }
        public static int ChonMenu(int soMenu)
        {
            int choice;
            do
            {
                XuatMenu();
                Console.Write("Chọn chức năng: ");
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= 0 && choice <= soMenu)
                    return choice;
                Console.WriteLine("Lựa chọn không hợp lệ! Vui lòng nhập số từ 0 đến " + soMenu);
            } while (true);
        }


        public static void XuLyChucNang(int chon, DanhSachSinhVien ds)
        {
            
            string lop;
            switch (chon)
            {
                case 1:
                    Console.WriteLine("1. Đếm số lượng sinh viên Nam trong lớp");
                    Console.WriteLine("Nhap lop ban muon dem sinh vien: ");
                    lop = Console.ReadLine();
                    Console.WriteLine($"Số lượng sinh viên Nam trong lớp {lop}: {ds.DemSoLuongSVNam(ds,lop)}");
                    break;
                case 2:
                    Console.WriteLine("2. Đếm số lượng sinh viên Nữ trong lớp");
                    Console.WriteLine("Nhap lop ban muon dem sinh vien: ");
                    lop = Console.ReadLine();
                    Console.WriteLine($"Số lượng sinh viên Nu trong lớp {lop}: {ds.DemSoLuongSVNu(ds,lop)}");
                    break;
                case 3:
                    Console.WriteLine("3. Hiển thị danh sách sinh viên theo chiều tăng, giảm của điểm trung bình");
                    Console.WriteLine("danh sach sap xep theo chieu tang:");
                    ds.SapXepTangTheoDTB();
                    ds.XuatDanhSachSV(ds);
                    Console.WriteLine("danh sach sap xep theo chieu giam:");
                    ds.SapXepGiamTheoDTB();
                    ds.XuatDanhSachSV(ds);
                    break;
                case 4:
                    Console.WriteLine("4. Tìm danh sách sinh viên có điểm trung bình cao nhất");
                    DanhSachSinhVien kq = ds.TimDSSVCoDTBCaoNhat();
                    kq.XuatDanhSachSV(kq);
                    break;
                case 5:
                    Console.WriteLine("5. Tìm lớp có sinh viên có điểm trung bình cao nhất");
                    //se co truong hop nhieu sinh vien co so diem giong nhau thuoc nhieu lop khac nhau nen se luu vao mot list<string>
                    Console.WriteLine($"lop co sinh vien co diem trung binh cao nhat la ");
                    List<string> dsLopCoDTBCaoNhat = ds.TimLopCoDTBCaoNhat();
                    ds.XuatDanhSachChuoi(dsLopCoDTBCaoNhat);
                    break;
                case 6:
                    Console.WriteLine("6. Tìm lớp có sinh viên không có điểm trung bình cao nhất");
                    List<string> dsLopKoCoDTBCaoNhat = ds.TimLopKhongCoDTBCaoNhat();
                    ds.XuatDanhSachChuoi(dsLopKoCoDTBCaoNhat);
                    break;
                case 7:
                    Console.WriteLine("7. Hiển thị danh sách sinh viên theo lớp và theo chiều giảm của điểm trung bình");
                    ds.SapTheoLop_GiamTheoDTB();
                    ds.XuatDanhSachSV(ds);
                    break;
                case 8:
                    Console.WriteLine("8. Xếp hạng sinh viên của lớp");
                    ds.InViThuCuaCacSinhVienTrongLop();
                    break;
                case 9:
                    Console.WriteLine("9. Tìm lớp có tổng điểm trung bình cao nhất, thấp nhất");
                    Console.WriteLine($"Lop co tong diem trung binh cao nhat la: {ds.LopCoTongDiemTrungBinhCaoNhat()}");
                    break;
                case 10:
                    Console.WriteLine("10. Tìm lớp có nhiều sinh viên giỏi nhất");
                    Console.WriteLine($"lop co nhieu sinh vien gioi nhat la {ds.LopCoNhieuSinhVienGioiNhat()}");
                    break;
                case 11:
                    Console.WriteLine("11. Tìm lớp có nhiều (hoặc ít) sinh viên theo loại yếu, trung bình, khá");
                    Console.WriteLine($"lop cho nhieu sinh vien theo loai yeu, TB, kha la: {ds.LopCoNhieuSinhVienThuocLoaiYeuTBKhaNhat()}");
                    break;
                case 12:
                    Console.WriteLine("12. Ghi xuống file danh sách lớp");
                    ds.GhiDanhSachLopXuongFile();
                    Console.WriteLine("file da duoc ghi!!");
                    break;
                case 13:
                    Console.WriteLine("13. Tìm lớp không có sinh viên nữ");
                    if(ds.LopKhongCoSVNu().Count == 0 || ds.LopKhongCoSVNu() == null)
                    {
                        Console.WriteLine("danh sach rong !!!");
                    }
                    else
                    {
                        Console.WriteLine($"lop khong co sinh vien nu la ");
                        ds.XuatDanhSachChuoi(ds.LopKhongCoSVNu());
                        
                    }
                    break;
                case 14:
                    Console.WriteLine("14. Tìm lớp không có sinh viên nam");
                    if (ds.LopKhongCoSVNam().Count == 0 || ds.LopKhongCoSVNam() == null)
                    {
                        Console.WriteLine("danh sach rong !!!");
                    }
                    else
                    {

                        Console.WriteLine($"lop khong co sinh vien nam la ");
                        ds.XuatDanhSachChuoi(ds.LopKhongCoSVNam());

                    }
                    break;
                case 15:
                    Console.WriteLine("15. Đếm số lượng sinh viên theo lớp");
                    List<(string, int)> danhSachSoLuongSinhVienCuaTungLop = ds.TimSoLuongSinhVienCua1Lop();
                    foreach(var item in danhSachSoLuongSinhVienCuaTungLop)
                    {
                        Console.WriteLine($"Lop {item.Item1} co so luong sinh vien la {item.Item2}");
                    }
                    break;
                case 16:
                    Console.WriteLine("16. Xóa tất cả sinh viên của lớp nào đó");
                    Console.WriteLine("nhap lop ban muon xoa sinh vien");
                    string lopMuonXoa = Console.ReadLine();
                    lopMuonXoa = ds.ChuanHoaKyTu(lopMuonXoa);
                    ds.XoaTatCaSinhVienCuaLop(lopMuonXoa);
                    Console.WriteLine("danh sach sau khi da xoa la");
                    ds.XuatDanhSachSV(ds);
                    break;
                case 17:
                    Console.WriteLine("17. Xếp loại sinh viên dựa trên điểm trung bình");
                    Console.WriteLine("danh sach sinh vien sau khi duoc xep loai la");
                    List<(string, XepLoaiHocLuc)> xepLoai = ds.XepLoaiSinhVien();
                    foreach(var item in xepLoai)
                    {
                        Console.WriteLine($"{item.Item1} Xep Loai: {item.Item2}");
                    }
                    break;
                default:
                    Console.WriteLine("Chức năng không hợp lệ!");
                    break;
            }
        }
        public static void ChayChuongTrinh(DanhSachSinhVien ds)
        {
            int soMenu = 17;
            int chon;

            do
            {
                chon = ChonMenu(soMenu);
                XuLyChucNang(chon, ds);
            } while (chon != 0); // Nhập 0 để thoát chương trình
        }

    }
}
