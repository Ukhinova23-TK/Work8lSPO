using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8lb
{
    class App
    {
        private string _name;
        private string _prouzvod;
        private double _razmer;
        private double _zena;
        public string Name { get { return _name; } set { _name = value; } }
        public string Prouzvod { get { return _prouzvod; } set { _prouzvod = value; } }
        public double Razmer { get { return _razmer; } set { _razmer = value; } }
        public double Zena { get { return _zena; } set { _zena = value; } }
        public App()
        {
            this.Name = "";
            this.Prouzvod = "";
            this.Razmer = 0;
            this.Zena = 0;
        }
        public App(string name, string prouzvod, double razmer, double zena)
        {
            this.Name = name;
            this.Prouzvod = prouzvod;
            this.Razmer = razmer;
            this.Zena = zena;
        }
        public bool Sort(App app)
        {
            if (this.Name.CompareTo(app.Name) > 0)
            {
                return true;
            }
            else if (this.Name.CompareTo(app.Name) < 0)
            {
                return false;
            }
            else
            {
                if (this.Prouzvod.CompareTo(app.Prouzvod) > 0)
                {
                    return true;
                }
                else if (this.Prouzvod.CompareTo(app.Prouzvod) < 0)
                {
                    return false;
                }
                else
                {
                    if ((this.Razmer == app.Razmer) && (this.Zena == app.Zena))
                    {
                        throw new Exception("Такой эелемент уже есть в списке.");
                    }
                    else return true;
                }
            }
        }
    }
}
