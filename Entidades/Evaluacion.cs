using System;

namespace CorEscuela.Entidades
{
    public class Evaluacion: ObjetoEscuelaBase
    {
        ///public Evaluacion() => UniqueId = Guid.NewGuid().ToString();
        public Alumno Alumno { get; set; }
        public Asignatura Asignatura {get; set;}
        public float Nota { get; set; }

        public override string ToString() 
        {
            return $"{Nota},{Alumno.Nombre}, {Asignatura.Nombre}";
        }
    }
}