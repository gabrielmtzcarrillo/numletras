using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumLetras
{
    class Program
    {
        static void Main(string[] args)
        {
            MonedaLetra moneda;
            StringBuilder sb = new StringBuilder();
            Random r = new Random();
            double cantidad;

            for (int i = 0; i < 1000; i++)
            {
                cantidad = Math.Round(r.Next(0, 20000) + r.NextDouble(), 2);

                moneda = new MonedaLetra(cantidad, "PESO", "CENTAVO");

                sb.AppendFormat("{0,12:C}\t{1}", moneda.Moneda, moneda);
                sb.AppendLine();
            }

            System.IO.File.WriteAllText("prueba.txt",sb.ToString());
        }
    }
}
