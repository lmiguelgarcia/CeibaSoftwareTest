using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BibliotecaDominio
{
    public class Libro
    {
        public string Isbn { get; }
        public string Titulo { get; }
        public int Anio { get; }


        public Libro(string isbn, string titulo, int anio)
        {
            this.Isbn = isbn;
            this.Titulo = titulo;
            this.Anio = anio;
        }

        public decimal ObtenerSumaIsbn()
        {
            return Isbn.Sum(x => Char.IsDigit(x) ? int.Parse(x.ToString()) : 0);
        }

        public bool EsPalindromo()
        {
            return Isbn.SequenceEqual(Isbn.Reverse());
        }


    }
}
