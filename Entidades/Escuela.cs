using System;
using System.Collections.Generic;
using CorEscuela.Util;

namespace CorEscuela.Entidades
{
    public class Escuela: ObjetoEscuelaBase, ILugar
    {
        //public Escuela(string nombre, int año) => (Nombre, AñoDeCreación) = (nombre, año);
        public Escuela(string nombre, int año, TiposEscuela tipo, string pais = "", string ciudad = "")
        {
            (Nombre, AñoDeCreación) = (nombre, año);
            Pais = pais;
            Ciudad = ciudad;
        }
        //public string nombre;
        //public string Nombre{ get { return "Copia:" + nombre; } set { nombre = value.ToUpper(); }}
        public int AñoDeCreación { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public TiposEscuela TipoEscuela { get; set; }
        public List<Curso> Cursos { get; set; }
        public string Direccion { get; set;}

        public void LimpiarLugar()
        {
            Printer.PrintLine(36);
            Console.WriteLine("Limpiando establecimiento...........");

            foreach (var curso in Cursos)
            {
                curso.LimpiarLugar();
            }

            Console.WriteLine($"Escuela {Nombre} esta limpio.");
        }

        public override string ToString()
        {
            return $"Nombre: \"{Nombre}\", Tipo: {TipoEscuela} {System.Environment.NewLine} Pais: {Pais}, Ciudad:{Ciudad}";
        }
    }
}
