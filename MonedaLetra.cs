using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumLetras
{
    public class MonedaLetra
    {
        double _numero;
        int _entero;
        int _centavos;

        string _nom_moneda;
        string _nom_centimos;

        public double Moneda { get { return _numero; } }

        public MonedaLetra(double numero, string moneda, string centimos)
        {
            _numero = Math.Round(numero, 2);
            _entero = (int)Math.Floor(_numero);
            _centavos = (int)(Math.Round(_numero - _entero, 2) * 100.0D);

            _nom_moneda = moneda;
            _nom_centimos = centimos;
        }

        public override string ToString()
        {
            string num = string.Empty;
            string cents = string.Empty;

            cents = _centavos > 0 ? " CON " + Letras0_99(_centavos) + _nom_centimos + (_centavos > 1 ? "S" : "") : string.Empty;

            num = string.Format("{0}{1}{2}{3}", Letras(_entero), _nom_moneda, _numero > 1 ? "S" : string.Empty, cents);

            return num;
        }

        public string Letras0_99(int numero)
        {
            string letras = string.Empty;

            Dictionary<int, string> unidades = new Dictionary<int, string>()
            {
                { 0 , ""},
                { 1 , "UN "},
                { 2 , "DOS "},
                { 3 , "TRES "},
                { 4 , "CUATRO "},
                { 5 , "CINCO "},
                { 6 , "SEIS "},
                { 7 , "SIETE "},
                { 8 , "OCHO "},
                { 9 , "NUEVE "},
                { 10 , "DIEZ "},
                { 11 , "ONCE "},
                { 12 , "DOCE "},
                { 13 , "TRECE "},
                { 14 , "CATORCE "},
                { 15 , "QUINCE "},
                { 20 , "VEINTE "},
                //{ 21 , "VEINTIÚN "},
                { 30 , "TREINTA "},
                //{ 31 , "TREINTAIÚN "},
                { 40 , "CUARENTA "},
                //{ 41 , "CUARENTAIÚN "},
                { 50 , "CINCUENTA "},
                //{ 51 , "CINCUENTAIÚN "},
                { 60 , "SESENTA "},
                //{ 61 , "SESENTAIÚN "},
                { 70 , "SETENTA "},
                //{ 71 , "SETENTAIÚN "},
                { 80 , "OCHENTA "},
                //{ 81 , "OCHENTAIÚN "},
                { 90 , "NOVENTA "},
                //{ 91 , "NOVENTAIÚN "},
            };

            Dictionary<int, string> decenas = new Dictionary<int, string>()
            {
                {0,""},
                { 10 , "DIECI"},
                { 20 , "VEINTI"},
                { 30 , "TREINTA Y "},
                { 40 , "CUARENTA Y "},
                { 50 , "CINCUENTA Y "},
                { 60 , "SESENTA Y "},
                { 70 , "SETENTA Y "},
                { 80 , "OCHENTA Y "},
                { 90 , "NOVENTA Y "},
            };

            if (unidades.ContainsKey(numero))
            {
                letras = unidades[numero];
            }
            else
            {
                int parteUnidad = numero % 10;
                int parteDecena = (numero % 100) - parteUnidad;
                
                string letrasUnidad = unidades[parteUnidad];
                string letrasDecena = decenas[parteDecena];
                
                letras = letrasDecena + letrasUnidad;
            }

            return letras;
        }

        public string Letras0_999(int numero)
        {
            string letras = string.Empty;

            Dictionary<int, string> especial = new Dictionary<int, string>()
            {
                {0,"CERO "},
                {100,"CIEN "},
                {101,"CIENTO UNO "},
            };

            Dictionary<int, string> centenas = new Dictionary<int, string>()
            {
                {0,""},
                { 100 , "CIENTO "},
                { 200 , "DOS CIENTOS "},
                { 300 , "TRES CIENTOS "},
                { 400 , "CUATRO CIENTOS "},
                { 500 , "QUINIENTOS "},
                { 600 , "SEIS CIENTOS "},
                { 700 , "SETECIENTOS "},
                { 800 , "OCHO CIENTOS "},
                { 900 , "NOVECIENTOS "},
            };

            if (especial.ContainsKey(numero))
            {
                letras = especial[numero];
            }
            else
            {
                int parteUnidad = numero % 10;
                int parteDecena = (numero % 100) - parteUnidad;
                int parteCentena = (numero % 1000) - parteDecena - parteUnidad;

                string letras0_99 = Letras0_99(parteUnidad + parteDecena);
                string letrasCentenas = centenas[parteCentena];

                letras = letrasCentenas + letras0_99;
            }

            return letras;
        }

        public string Letras(int numero)
        {
            string parte = string.Empty;
            string letras = string.Empty;
            
            string cantidad = new string(numero.ToString().Reverse().ToArray());

            int n = (int)Math.Ceiling((double)cantidad.Length/3.0D);
            int digitos = 0;
            int tam = cantidad.Length;

            string mil;

            for (int i = 0; i < n; i++)
            {
                tam = cantidad.Length - i * 3;
                digitos = tam > 3 ? 3 : tam;

                parte = cantidad.Substring(i * 3, digitos);
                parte = new string(parte.Reverse().ToArray());

                digitos = int.Parse(parte);
                
                mil = i == 1 ? "MIL " : string.Empty;
                letras = digitos == 1 && i == 1 ? "MIL " : Letras0_999(digitos) + mil + letras;
            }

            return letras;
        }

    }
}
