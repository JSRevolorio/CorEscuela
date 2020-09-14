using System;
using System.Collections.Generic;
using CorEscuela.Util;

namespace CorEscuela.Entidades
{
    public class Curso: ObjetoEscuelaBase, ILugar
    {
        public TiposJornada Jornada { get; set; }
        public List<Alumno> Alumnos {get; set;}
        public List<Asignatura> Asignaturas {get; set;}
        public string Direccion { get; set; }

        public void LimpiarLugar()
        {
            Printer.PrintLine(36);
            Console.WriteLine("Limpiando establecimiento...........");
            Console.WriteLine($"curso {Nombre} esta limpio.");
        }
    }
}