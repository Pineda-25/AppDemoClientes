using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ApiException : Exception
   {
        //Constructor (metodo inicializador)
        //Anulado el constructor predeterminado vacio  => public clase
        //base(massage
        public ApiException(string message) : base (message) { }
    }
}
