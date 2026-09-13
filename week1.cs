using System;
using System.Collections.Generic;

interface IHinh
{
    double GetDienTich();
    double GetChuVi();
    void Nhap();
    void HienThi();
}

class HinhTron : IHinh
{
    private double _r;
    public double R
    {
        get => _r;
        set
        {
            if (value <= 0) throw new ArgumentException("Ban kinh phai > 0");
            _r = value;
        }
    }

    public HinhTron() { }
    public HinhTron(double r) { R = r; }

    public double GetChuVi() => 2 * Math.PI * R;
    public double GetDienTich() => Math.PI * R * R;

    public void Nhap()
    {
        while (true)
        {
            Console.Write("Nhap ban kinh hinh tron: ");
            if (double.TryParse(Console.ReadLine(), out double r) && r > 0)
            {
                R = r; break;
            }
            Console.WriteLine("Gia tri khong hop le. Thu lai.");
        }
    }

    public void HienThi()
    {
        Console.WriteLine($"Hinh Tron: R={R}, Chu vi={GetChuVi():F2}, Dien tich={GetDienTich():F2}");
    }
}

class HinhChuNhat : IHinh
{
    private double _a, _b;
    public double A
    {
        get => _a;
        set
        {
            if (value <= 0) throw new ArgumentException("Chieu dai phai > 0");
            _a = value;
        }
    }
    public double B
    {
        get => _b;
        set
        {
            if (value <= 0) throw new ArgumentException("Chieu rong phai > 0");
            _b = value;
        }
    }

    public HinhChuNhat() { }
    public HinhChuNhat(double a, double b) { A = a; B = b; }

    public double GetChuVi() => 2 * (A + B);
    public double GetDienTich() => A * B;

    public void Nhap()
    {
        while (true)
        {
            Console.Write("Nhap chieu dai: ");
            bool ok1 = double.TryParse(Console.ReadLine(), out double a);
            Console.Write("Nhap chieu rong: ");
            bool ok2 = double.TryParse(Console.ReadLine(), out double b);
            if (ok1 && ok2 && a > 0 && b > 0)
            {
                A = a; B = b; break;
            }
            Console.WriteLine("Gia tri khong hop le. Thu lai.");
        }
    }

    public void HienThi()
    {
        Console.WriteLine($"Hinh Chu Nhat: A={A}, B={B}, Chu vi={GetChuVi():F2}, Dien tich={GetDienTich():F2}");
    }
}

class HinhTamGiac : IHinh
{
    private double _a, _b, _c;
    public double A
    {
        get => _a;
        set
        {
            if (value <= 0) throw new ArgumentException("Canh phai > 0");
            _a = value;
        }
    }
    public double B
    {
        get => _b;
        set
        {
            if (value <= 0) throw new ArgumentException("Canh phai > 0");
            _b = value;
        }
    }
    public double C
    {
        get => _c;
        set
        {
            if (value <= 0) throw new ArgumentException("Canh phai > 0");
            _c = value;
        }
    }

    public HinhTamGiac() { }
    public HinhTamGiac(double a, double b, double c)
    {
        A = a; B = b; C = c;
        if (!IsTamGiac()) throw new ArgumentException("Khong phai 3 canh tam giac");
    }

    public bool IsTamGiac()
    {
        return A + B > C && A + C > B && B + C > A;
    }

    public double GetChuVi() => A + B + C;
    public double GetDienTich()
    {
        double p = GetChuVi() / 2;
        return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
    }

    public void Nhap()
    {
        while (true)
        {
            Console.Write("Nhap canh a: ");
            bool ok1 = double.TryParse(Console.ReadLine(), out double a);
            Console.Write("Nhap canh b: ");
            bool ok2 = double.TryParse(Console.ReadLine(), out double b);
            Console.Write("Nhap canh c: ");
            bool ok3 = double.TryParse(Console.ReadLine(), out double c);
            if (ok1 && ok2 && ok3 && a > 0 && b > 0 && c > 0)
            {
                A = a; B = b; C = c;
                if (IsTamGiac()) break;
            }
            Console.WriteLine("Ba canh khong tao thanh tam giac. Thu lai.");
        }
    }

    public void HienThi()
    {
        Console.WriteLine($"Hinh Tam Giac: a={A}, b={B}, c={C}, Chu vi={GetChuVi():F2}, Dien tich={GetDienTich():F2}");
    }
}

class Program
{
    static void Main()
    {
        var shapes = new List<IHinh>();
        while (true)
        {
            Console.WriteLine("1. Them Hinh Tron\n2. Them Hinh Chu Nhat\n3. Them Hinh Tam Giac\n4. Hien thi tat ca\n5. Thoat");
            Console.Write("Lua chon: ");
            string choice = Console.ReadLine();
            if (choice == "5") break;
            switch (choice)
            {
                case "1":
                    var t = new HinhTron(); t.Nhap(); shapes.Add(t); break;
                case "2":
                    var r = new HinhChuNhat(); r.Nhap(); shapes.Add(r); break;
                case "3":
                    var tg = new HinhTamGiac(); tg.Nhap(); shapes.Add(tg); break;
                case "4":
                    if (shapes.Count == 0) Console.WriteLine("Chua co hinh nao.");
                    else
                    {
                        Console.WriteLine("Danh sach hinh:");
                        foreach (var h in shapes) h.HienThi();
                    }
                    break;
                default:
                    Console.WriteLine("Lua chon khong hop le."); break;
            }
        }
    }
}
