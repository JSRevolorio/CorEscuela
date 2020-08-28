using System;
using CorEscuela.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace CorEscuela.App
{
    public class EscuelaEngine
    {
        public Escuela Escuela { get; set; }
        public EscuelaEngine() { }


        public void Inicializar()
        {
            Escuela = new Escuela("Platzi Academy", 2012, TiposEscuela.Primaria, ciudad: "Bogotá", pais: "Colombia");

            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones();
        }

    private void CargarEvaluaciones()
    {
        foreach (Curso curso in Escuela.Cursos)
        {
            foreach (Asignatura asignatura in curso.Asignaturas)
            {
                foreach (Alumno alumno in curso.Alumnos)
                {
                    var rnd = new Random(System.Environment.TickCount);
                    alumno.Evaluaciones = new List<Evaluacion>();

                    for (int i = 0; i < 5; i++)
                    {
                        var evaluacion = new Evaluacion()
                        {
                            Asignatura = asignatura,
                            Nombre     = $"{asignatura.Nombre} Ev#{i+1}",
                            Nota       = (float)(5*rnd.NextDouble()),
                            Alumno = alumno
                        };
                        alumno.Evaluaciones.Add(evaluacion);  
                    } 
                }
            }
        }
    }

    private void CargarAsignaturas()
    {
        foreach (Curso curso in Escuela.Cursos)
        {
            curso.Asignaturas = new List<Asignatura>(){
                            new Asignatura{Nombre="Matemáticas"} ,
                            new Asignatura{Nombre="Educación Física"},
                            new Asignatura{Nombre="Castellano"},
                            new Asignatura{Nombre="Ciencias Naturales"}
                };
        }
    }

    private List<Alumno> GenerarAlumnosAlAzar(int cantidad)
    {
        string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
        string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
        string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

        var listaAlumnos = from n1 in nombre1
                           from n2 in nombre2
                           from a1 in apellido1
                           select new Alumno { Nombre = $"{n1} {n2} {a1}" };

        return listaAlumnos.OrderBy((al) => al.UniqueId).Take(cantidad).ToList();
    }

    private void CargarCursos()
    {
        Escuela.Cursos = new List<Curso>
            {
                new Curso() {Nombre = "101", Jornada = TiposJornada.Mañana},
                new Curso() {Nombre = "201", Jornada = TiposJornada.Noche},
                new Curso() {Nombre = "301", Jornada = TiposJornada.Mañana},
                new Curso() {Nombre = "401", Jornada = TiposJornada.Tarde},
                new Curso() {Nombre = "501", Jornada = TiposJornada.Tarde}
            };

        Random rnd = new Random();
        foreach (var c in Escuela.Cursos)
        {
            int cantRandom = rnd.Next(5, 20);
            c.Alumnos = GenerarAlumnosAlAzar(cantRandom);
        }
    }
}
}


