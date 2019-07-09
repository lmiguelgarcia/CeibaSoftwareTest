using BibliotecaDominio.ReglaDeNegocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaDominio
{
    public class ReglaPrestamo : IReglaPrestamo
    {
        const int SUMA_DIGITOS_ISBN = 30;
        const int DIAS_DE_ENTREGA = 15;

        public DateTime FechaPrestamo(Libro libro)
        {
            DateTime fechaEntrega = new DateTime();
            if (libro.ObtenerSumaIsbn() > SUMA_DIGITOS_ISBN)
            {
                fechaEntrega = CalificadorUtil.SumarDiasSinContarDomingo(DateTime.Now, DIAS_DE_ENTREGA);
            }
            return fechaEntrega;
        }
    }
}
