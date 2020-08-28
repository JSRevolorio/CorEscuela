using System;
using static System.Console;
namespace CorEscuela.Util
{
    public static class Printer
    {
        public static void PrintLine(int tamaño = 10)
        {
            WriteLine("".PadLeft(tamaño,'='));
        }

        public static void WriteTitle(String title = "titulo")
        {
            PrintLine(title.Length);
            WriteLine(title);
            PrintLine(title.Length);
        }
        
    }
}