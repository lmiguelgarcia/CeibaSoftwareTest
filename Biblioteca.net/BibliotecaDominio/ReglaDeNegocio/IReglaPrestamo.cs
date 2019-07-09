using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaDominio.ReglaDeNegocio
{
    public interface IReglaPrestamo
    {
        DateTime FechaPrestamo(Libro libro);
    }
}
