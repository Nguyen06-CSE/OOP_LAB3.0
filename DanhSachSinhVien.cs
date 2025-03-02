using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB3
{
    internal class DanhSachSinhVien
    {
        //khoi tao 1 danh sach sinh vien
        List<SinhVien> ds = new List<SinhVien>();

        //ham them sinh vien voi thong tin cua 1 sinh vien cho truoc 
        public void Them(SinhVien sv)
        {
            ds.Add(sv);
        }


        //ham de doc thong tin tu file 
        public void NhapTuFile()
        {
            //khoi tao 1 bien cung ten voi file du lieu 
            var fileName = "data.txt";
            //ham de doc du lieu truc tiep tu fileName vua tao
            StreamReader sr = new StreamReader(fileName);

            //kiem tra khi nao het duong chua noi dung thi se ket thuc
            //moi khi khiem tra se luu noi dung vao DanhSachSinhVien thong qua ham them. SinhVien o day se duoc khoi tao theo phuong thuc thu 3 
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Them(new SinhVien(line));
            }
        }


        //ghi de phhuong thuc, dua ve 1 chuoi string
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("MSSV".PadRight(6) + "Ho ten".PadRight(14) + "DTB".PadRight(5) + "Gioi Tinh".PadRight(10) + "Lop\n"); // Corrected PadRight(2) to PadRight(5) for DTB and added padding to Gioi Tinh
            foreach (var sv in ds)
            {
                sb.Append(sv + "\n");
            }
            return sb.ToString();
        }



        public float TimDTBCaoNhat()
        {
            if (ds.Count == 0) return 0; // Trả về 0 nếu danh sách rỗng
            return ds.Max(x => x.dTB);
        }


        public DanhSachSinhVien TimDSSVCoDTBCaoNhat()
        {
            //khoi tao danh sach ket qua de luu cac sinh vien co diem trung binh cao nhat
            DanhSachSinhVien kq = new DanhSachSinhVien();
            float max = TimDTBCaoNhat();

            foreach (var item in ds)
            {
                if (item.dTB == max)
                {
                    kq.Them(item);
                }
            }
            //tra ve danh sach kq
            return kq;
        }


        public string ChuanHoaKyTu(string kyTu)
        {
            // Kiểm tra nếu chuỗi rỗng hoặc null
            if (string.IsNullOrWhiteSpace(kyTu))
                return string.Empty;

            // Chuyển thành mảng ký tự để xử lý thủ công (đổi tên biến để tránh trùng)
            char[] kyTuArr = kyTu.ToCharArray();

            // Xác định vị trí bắt đầu và kết thúc chuỗi (bỏ khoảng trắng đầu và cuối)
            int start = 0;
            int end = kyTuArr.Length - 1;

            while (start <= end && kyTuArr[start] == ' ')
                start++; // Loại bỏ khoảng trắng đầu

            while (end >= start && kyTuArr[end] == ' ')
                end--; // Loại bỏ khoảng trắng cuối

            // Tạo chuỗi mới từ phần không có khoảng trắng đầu và cuối
            string result = new string(kyTuArr, start, end - start + 1);

            // Chuyển đổi thành chữ hoa toàn bộ
            return result.ToUpper();
        }


        private int DemSoLuongSVTheoGioiTinhvaLop(DanhSachSinhVien ds, bool GT, string lop)
        {
            
            // Kiểm tra nếu lớp được nhập không hợp lệ
            if (string.IsNullOrWhiteSpace(lop))
            {
                Console.WriteLine("Lớp không hợp lệ!");
                return 0;
            }

            // Chuẩn hóa giá trị lớp: loại bỏ khoảng trắng dư thừa, chuyển về chữ thường
            lop = ChuanHoaKyTu(lop);

            int count = 0; // Biến đếm số lượng sinh viên

            // Duyệt qua danh sách sinh viên để đếm số lượng phù hợp
            foreach (var sinhVien in ds.ds)
            {
                // Chuẩn hóa giá trị lớp của sinh viên trước khi so sánh
                string lopSinhVien = ChuanHoaKyTu(sinhVien.Lop);
                if (sinhVien.gioiTinh == GT && lopSinhVien == lop)
                {
                    count++;
                }
            }

            return count;
        }



        public int DemSoLuongSVNam(DanhSachSinhVien ds, string lop)
        {
            return DemSoLuongSVTheoGioiTinhvaLop(ds, true, lop);
        }

        public int DemSoLuongSVNu(DanhSachSinhVien ds, string lop)
        {
            return DemSoLuongSVTheoGioiTinhvaLop(ds, false, lop);
        }



        public List<string> LayDanhSachLop()
        {
            // tao ra list ket qua
            List<string> kq = new List<string>();
            foreach (var sv in ds)
            {
                //neu kq chua co lop thi them lop
                if (!kq.Contains(sv.Lop))
                {
                    kq.Add(sv.Lop);
                }
            }
            //tra ve list kq
            return kq;
        }

        //tao ra kieu enum de code de doc, de hinh dung
        public enum KieuSapXep
        {
            //TangDTB == 0
            TangDTB,
            //GiamDTB == 1
            GiamDTB
        }


        void SapXep(KieuSapXep kieu)
        {
            //neu la TangDTB thi sap tang va nguoc lai
            if (kieu == KieuSapXep.TangDTB)
            {
                ds.Sort((sv1, sv2) => sv1.dTB.CompareTo(sv2.dTB));
            }
            else if (kieu == KieuSapXep.GiamDTB)
            {
                ds.Sort((sv1, sv2) => -sv1.dTB.CompareTo(sv2.dTB));
            }
        }

        public void SapXepTangTheoDTB()
        {
            SapXep(KieuSapXep.TangDTB);
        }

        public void SapXepGiamTheoDTB()
        {
            SapXep(KieuSapXep.GiamDTB);
        }

        public List<string> TimLopCoDTBCaoNhat()
        {
            //tao danh sach de luu cac lop co diem trung binh bang nhau
            List<string> lopDiemCao = new List<string>();
            //khoi tao gia tri max
            float max = TimDTBCaoNhat();

            foreach (var i in ds)
            {
                //neu tim thay lop co dTB bang max va danh sach de luu ket qua chua luu thong tin nay thi ta se them
                if (i.dTB == max && !lopDiemCao.Contains(i.Lop))
                {
                    lopDiemCao.Add(i.Lop);
                }
            }

            //tra ve list lopDiemCao
            return lopDiemCao;
        }

        public List<string> TimLopKhongCoDTBCaoNhat()
        {
            List<string> lopDiemCao = TimLopCoDTBCaoNhat();
            //lopKoCao de luu ket qua
            List<string> lopKoCao = new List<string>();
            foreach (var i in ds)
            {
                //neu lopDiemCao khong chua lop va lopKoCao cung khong chua lop thi ta se them lop vao ket qua
                if (!lopDiemCao.Contains(i.Lop) && !lopKoCao.Contains(i.Lop))
                {
                    lopKoCao.Add(i.Lop);
                }
            }
            return lopKoCao;
        }

        public void XuatDanhSachChuoi(List<string> chuoi)
        {
            foreach (var item in chuoi)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }


        public void SapTheoLop_GiamTheoDTB()
        {
            ds.Sort((sv1, sv2) =>
            {
                // So sánh lớp trước (sắp xếp tăng dần theo lớp)
                int compareLop = sv1.Lop.CompareTo(sv2.Lop);

                // Nếu lớp giống nhau, sắp xếp giảm dần theo điểm trung bình (DTB)
                if (compareLop == 0)
                    return sv2.dTB.CompareTo(sv1.dTB); // Sắp giảm theo DTB

                return compareLop;
            });
        }



        public int TimViThuCuaSV(SinhVien sv)
        {
            //khoi tao vi thu bang 1 
            int vt = 1;
            foreach (var i in ds)
            {
                //neu tim thay cac sinh vien cung lop va cac sinh vien do co diem cao hon thi se thang vi thu
                if (sv.Lop == i.Lop && sv.dTB < i.dTB)
                {
                    vt++;
                }
            }
            return vt;
        }

        public void InViThuCuaCacSinhVienTrongLop()
        {
            List<(string, int)> result = new List<(string, int)>();

            foreach (var sv in ds)
            {
                result.Add((sv.hoTen, TimViThuCuaSV(sv)));
            }


            // Sắp xếp danh sách theo vị thứ tăng dần
            result.Sort((a, b) => a.Item2.CompareTo(b.Item2));

            Console.WriteLine("Danh sách xếp hạng sinh viên:");
            foreach (var item in result)
            {
                Console.WriteLine($"Tên: {item.Item1}, Vị thứ: {item.Item2}");
            }
        }


        public float TinhTongDTB(string Lop)
        {
            float result = 0;
            foreach (var sv in ds)
            {
                if(Lop == sv.Lop)
                {
                    result += sv.dTB;
                }
            }
            return result;
        }


        public string LopCoTongDiemTrungBinhCaoNhat()
        {
            if (ds.Count == 0)
                return ""; // Trả về chuỗi rỗng nếu danh sách sinh viên trống

            Dictionary<string, float> tongDTBTheoLop = new Dictionary<string, float>();

            // Tính tổng điểm trung bình cho từng lớp
            foreach (var sv in ds)
            {
                string lop = sv.Lop;
                if (!tongDTBTheoLop.ContainsKey(lop))
                {
                    //nếu trong dictionary chưa chứa lop thì sẽ thêm lop vào đồng thời gán thêm giá trị TinhTongDTB vào phần thứ hai của Dictionary
                    tongDTBTheoLop[lop] = TinhTongDTB(lop);
                }
            }

            // Tìm lớp có tổng điểm trung bình cao nhất
            // .Key se tra ve kieu string
            return tongDTBTheoLop.OrderByDescending(x => x.Value).First().Key;
        }



        public int DemSLSinhVienGioiTrongMotLop(string Lop)
        {
            int result = 0;
            foreach (var sv in ds)
            {
                if(sv.Lop == Lop && sv.dTB >= 8)
                {
                    ++result;
                }
            }
            return result;
        }

        public string LopCoNhieuSinhVienGioiNhat()
        {
            string result = "";

            //dung hathset de loai bo trung lap 
            HashSet<string> xuLyTrung = new HashSet<string>(); 
            int max = 0;

            foreach (var sv in ds)
            {
                if (!xuLyTrung.Contains(sv.Lop))
                {
                    xuLyTrung.Add(sv.Lop);
                    int soLuongGioi = DemSLSinhVienGioiTrongMotLop(sv.Lop); 

                    if (soLuongGioi > max)
                    {
                        max = soLuongGioi;
                        result = sv.Lop;
                    }
                }
            }

            return result;
        }

        public int DemSLSinhVienKhongXepLoaiGioiTrongMotLop(string Lop)
        {
            int result = 0;
            foreach (var sv in ds)
            {
                if (sv.Lop == Lop && sv.dTB < 8)
                {
                    ++result;
                }
            }
            return result;
        }


        public string LopCoNhieuSinhVienThuocLoaiYeuTBKhaNhat()
        {
            string result = "";

            //dung hathset de loai bo trung lap 
            HashSet<string> xuLyTrung = new HashSet<string>();
            int max = 0;

            foreach (var sv in ds)
            {
                if (!xuLyTrung.Contains(sv.Lop))
                {
                    xuLyTrung.Add(sv.Lop);
                    int soLuongGioi = DemSLSinhVienKhongXepLoaiGioiTrongMotLop(sv.Lop);

                    if (soLuongGioi > max)
                    {
                        max = soLuongGioi;
                        result = sv.Lop;
                    }
                }
            }

            return result;
        }


        public void GhiDanhSachLopXuongFile()
        {
            using (StreamWriter writer = new StreamWriter("danhsachlop.txt"))
            {
                foreach (var sv in ds)
                {
                    writer.WriteLine($"{sv.maSV}, {sv.hoTen}, {sv.dTB}, {(sv.gioiTinh ? "Nam" : "Nữ")}, {sv.Lop}");
                }
            }
        }

        public List<string> LopKhongCoSVNu()
        {
            HashSet<string> tatCaLop = new HashSet<string>();
            HashSet<string> lopCoNu = new HashSet<string>();

            foreach (var sv in ds)
            {
                tatCaLop.Add(sv.Lop);
                if (!sv.gioiTinh) // Nếu có sinh viên nữ
                {
                    lopCoNu.Add(sv.Lop);
                }
            }

            // Lớp không có nữ = Tổng lớp - Lớp có nữ
            return tatCaLop.Except(lopCoNu).ToList();
        }

        public List<string> LopKhongCoSVNam()
        {
            HashSet<string> tatCaLop = new HashSet<string>();
            HashSet<string> lopCoNam = new HashSet<string>();

            foreach (var sv in ds)
            {
                tatCaLop.Add(sv.Lop);
                if (sv.gioiTinh) // Nếu có sinh viên nam
                {
                    lopCoNam.Add(sv.Lop);
                }
            }

            // Lớp không có nam = Tổng lớp - Lớp có nam
            return tatCaLop.Except(lopCoNam).ToList();
        }

        public int DemSoLuongSinhVienTrong1Lop(string lop)
        {
            int count = 0;
            foreach (var sv in ds)
            {
                if(sv.Lop == lop)
                {
                    ++count;
                }
            }
            return count;
        }

        public List<(string, int)> TimSoLuongSinhVienCua1Lop()
        {
            List<(string, int)> result = new List<(string, int)>();
            HashSet<string> tatCaLop = new HashSet<string>();

            foreach (var sv in ds)
            {
                if (tatCaLop.Add(sv.Lop))  
                {
                    result.Add((sv.Lop, DemSoLuongSinhVienTrong1Lop(sv.Lop)));
                }
            }
            return result;
        }

        public void XoaTatCaSinhVienCuaLop(string Lop)
        {
            
            ds.RemoveAll(sv => ChuanHoaKyTu(sv.Lop) == Lop);
        }


        // Định nghĩa Enum cho xếp loại
        public enum XepLoaiHocLuc
        {
            Yeu,
            TrungBinh,
            Kha,
            Gioi
        }

        // Hàm xếp loại sinh viên
        public XepLoaiHocLuc XepLoai(float DTB)
        {
            if (DTB < 5) return XepLoaiHocLuc.Yeu;
            else if (DTB < 7) return XepLoaiHocLuc.TrungBinh;
            else if (DTB < 8) return XepLoaiHocLuc.Kha;
            else return XepLoaiHocLuc.Gioi;
        }


        public List<(string, XepLoaiHocLuc)> XepLoaiSinhVien()
        {
            List<(string, XepLoaiHocLuc)> result = new List<(string, XepLoaiHocLuc)>();
            foreach (var sv in ds)
            {
                result.Add((sv.hoTen, XepLoai(sv.dTB)));  
            }
            return result;
        }

        public void XuatDanhSachSV(DanhSachSinhVien ds)
        {
            foreach (SinhVien sv in ds.ds)
            {
                Console.WriteLine(sv);
            }
        }

    }
}
