using BibliotecaDominio.IRepositorio;
using BibliotecaDominio.ReglaDeNegocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaDominio
{
    public class Bibliotecario
    {
        public const string EL_LIBRO_NO_SE_ENCUENTRA_DISPONIBLE = "El libro no se encuentra disponible";
        public const string LIBRO_SIN_STOCK = "La libreria no cuenta con el libro";
        public const string LIBRO_PALINDROME = "los libros palíndromos solo se pueden utilizar en la biblioteca";
        private IRepositorioLibro libroRepositorio;
        private IRepositorioPrestamo prestamoRepositorio;
        private IReglaPrestamo prestamoRegla;

        public Bibliotecario(IRepositorioLibro libroRepositorio, IRepositorioPrestamo prestamoRepositorio,
            IReglaPrestamo prestamoRegla)
        {
            this.libroRepositorio = libroRepositorio;
            this.prestamoRepositorio = prestamoRepositorio;
            this.prestamoRegla = prestamoRegla;
        }

        public void Prestar(string isbn, string nombreUsuario)
        {
            Libro libro = ValidarLibro(isbn);

            Prestamo prestamo = new Prestamo(DateTime.Now, libro, prestamoRegla.FechaPrestamo(libro), nombreUsuario);

            this.prestamoRepositorio.Agregar(prestamo);
        }

        public bool EsPrestado(string isbn)
        {
            return prestamoRepositorio.ObtenerLibroPrestadoPorIsbn(isbn) != null;
        }

        public Libro ValidarLibro(string isbn)
        {
            Libro libro = libroRepositorio.ObtenerPorIsbn(isbn);
            if (libro == null)
            {
                throw new Exception(LIBRO_SIN_STOCK);
            }

            if (EsPrestado(libro.Isbn))
            {
                throw new Exception(EL_LIBRO_NO_SE_ENCUENTRA_DISPONIBLE);
            }

            if (libro.EsPalindromo())
            {
                throw new Exception(LIBRO_PALINDROME);
            }

            return libro;
        }
    }
}
