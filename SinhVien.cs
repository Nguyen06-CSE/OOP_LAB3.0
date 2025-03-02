using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB3
{
    internal class SinhVien
    {
        //khởi tạo các thuộc tính của sinh viên 
        public string maSV;
        public string hoTen;
        public float dTB;
        public bool gioiTinh;
        private string lop;

        //hàm lớp này để lấy giá trị (get) hoạc gán giá trị (set)
        public string Lop
        {
            //lay gia tri lop
            get { return lop; }
            //gan lop voi 1 gia tri 
            //Trim() co tac dung loai bo khoang trang trong mot chuoi ky tu 
            set { lop = value.Trim(); }
        }

        public SinhVien()
        {
            //hàm khởi tạo
            //mỗi sinh viên khởi tạo khi không đưa tham số vô sẽ mặc định có ms là 1 và tên là a 
            maSV = "1";
            hoTen = "a";
        }

        //hàm khởi tạo khi có tham số
        public SinhVien(string ma, string ho, float dtb, bool gt, string lop)
        {
            maSV = ma;
            hoTen = ho;
            dTB = dtb;
            gioiTinh = gt;
            this.lop = lop;
        }

        //hàm dùng để đưa 1 chuỗi thông tin trực tiếp vào 
        public SinhVien(string Line)
        {
            // 001, Nguyen Van A, 8.8, Nam, CTKU3

            //nơi có dấu phảy sẽ tách các thông tin ra lưu vào 1 mảng
            string[] str = Line.Split(',');
            maSV = str[0];
            hoTen = str[1];
            dTB = float.Parse(str[2]);
            gioiTinh = str[3] == "Nam" ? true : false;
            lop = str[4];
        }

        //ghi đè phương thức. các thông tin sẽ được đưa về 1 chuỗi kí tự. sau khi ghi đè chỉ cần dùng Console.Write để xuất 
        public override string ToString()
        {
            return string.Format("{0, 2} {1, 10} {2, 5:F1} {3, 6} {4, 10}", maSV, hoTen, dTB, gioiTinh == true ? "Nam" : "Nu", lop);
        }
    }
}
