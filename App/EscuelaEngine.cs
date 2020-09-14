using System;
using CorEscuela.Entidades;
using System.Collections.Generic;
using System.Linq;
using CorEscuela.Util;

namespace CorEscuela.App
{
    /*
     Operador Sealed:
     Sella la clase para que no puede heredar 
     pero si permite crear instancias
    */
    public sealed class EscuelaEngine
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
       
        public void ImprimirDiccionario(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> diccionarioObjetos, bool imprimirEvaluacion = false)
        {
            foreach (var diccionario in diccionarioObjetos)
            {
                Printer.WriteTitle(diccionario.Key.ToString());

                foreach (var val in diccionario.Value)
                {
                    switch (diccionario.Key)
                    {
                        case LlaveDiccionario.Evaluacion:
                            if (imprimirEvaluacion)
                                Console.WriteLine(val);
                            break;
                        case LlaveDiccionario.Escuela:
                            Console.WriteLine("Escuela: " + val);
                            break;
                        case LlaveDiccionario.Alumno:
                            Console.WriteLine("Alumno: " + val.Nombre);
                            break;
                        case LlaveDiccionario.Curso:
                            var curtmp = val as Curso;
                            if(curtmp != null)
                            {
                                int count = curtmp.Alumnos.Count;
                                Console.WriteLine("Curso: " + val.Nombre + " Cantidad Alumnos: " + count);
                            }
                            break;
                        default:
                            Console.WriteLine(val);
                            break;
                    }
                }
            }
        }

        public Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> GetDiccionarioObjetos()
        {
            var diccionario = new Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>>();

            diccionario.Add(LlaveDiccionario.Escuela, new[] { Escuela });
            diccionario.Add(LlaveDiccionario.Curso, Escuela.Cursos.Cast<ObjetoEscuelaBase>());

            var listaEvaluaciones = new List<Evaluacion>();
            var listaAsignaturas  = new List<Asignatura>();
            var listaAlumno       = new List<Alumno>();

            foreach (var curso in Escuela.Cursos)
            {
                listaAsignaturas.AddRange(curso.Asignaturas);
                listaAlumno.AddRange(curso.Alumnos);

                foreach (var alumno in curso.Alumnos)
                {
                    listaEvaluaciones.AddRange(alumno.Evaluaciones);
                }

            }
            diccionario.Add(LlaveDiccionario.Evaluacion,listaEvaluaciones.Cast<ObjetoEscuelaBase>());
            diccionario.Add(LlaveDiccionario.Asignatura,listaAsignaturas.Cast<ObjetoEscuelaBase>());
            diccionario.Add(LlaveDiccionario.Alumno,listaAlumno.Cast<ObjetoEscuelaBase>());
            return diccionario;
        }

        //IReadOnlyList: lista de solo lectura
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool traeCursos = true
            )
        {

            return GetObjetosEscuela(out int dummy, out dummy, out dummy, out dummy);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
           bool traeEvaluaciones = true,
           bool traeAlumnos = true,
           bool traeAsignaturas = true,
           bool traeCursos = true
           )
        {

            return GetObjetosEscuela(out conteoEvaluaciones, out int dummy, out dummy, out dummy);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
                        out int conteoEvaluaciones, out int conteoCursos,
                        bool traeEvaluaciones = true,
                        bool traeAlumnos = true,
                        bool traeAsignaturas = true,
                        bool traeCursos = true
                        )
        {

            return GetObjetosEscuela(out conteoEvaluaciones, out conteoCursos, out int dummy, out dummy);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
                        out int conteoEvaluaciones,
                        out int conteoCursos,
                        out int conteoAsignaturas,
                        bool traeEvaluaciones = true,
                        bool traeAlumnos = true,
                        bool traeAsignaturas = true,
                        bool traeCursos = true
             )
        {

            return GetObjetosEscuela(out conteoEvaluaciones, out conteoCursos, out conteoAsignaturas, out int dummy);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            out int conteoCursos,
            out int conteoAsignaturas,
            out int conteoAlumnos,
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool traeCursos = true
            )
        {
            conteoAlumnos = conteoAsignaturas = conteoEvaluaciones = 0;

            var listaObj = new List<ObjetoEscuelaBase>();
            listaObj.Add(Escuela);

            if (traeCursos)
                listaObj.AddRange(Escuela.Cursos);

            conteoCursos = Escuela.Cursos.Count;
            foreach (var curso in Escuela.Cursos)
            {
                conteoAsignaturas += curso.Asignaturas.Count;
                conteoAlumnos += curso.Alumnos.Count;

                if (traeAsignaturas)
                    listaObj.AddRange(curso.Asignaturas);

                if (traeAlumnos)
                    listaObj.AddRange(curso.Alumnos);

                if (traeEvaluaciones)
                {
                    foreach (var alumno in curso.Alumnos)
                    {

                        listaObj.AddRange(alumno.Evaluaciones);
                        conteoEvaluaciones += alumno.Evaluaciones.Count;
                    }
                }
            }

            return listaObj.AsReadOnly();  //lista de solo lectura
        }

        
        //retorna una tupla
        public (List<ObjetoEscuelaBase>, int) GetObjetoEscuelaBases(
            bool traeEvaluaciones = true,
            bool traeAlumnos      = true,
            bool traeAsignaturas  = true,
            bool traeCursos       = true
        )
        {
            
            int conteoEvaluaciones = 0;
            List<ObjetoEscuelaBase> listEscuelaBase = new List<ObjetoEscuelaBase>();

            listEscuelaBase.Add(Escuela);

            if (traeCursos)
            {
                listEscuelaBase.AddRange(Escuela.Cursos);
            }
            
            foreach (Curso curso in Escuela.Cursos)
            {

                if (traeAlumnos)
                {
                    listEscuelaBase.AddRange(curso.Alumnos);

                }

                if (traeAsignaturas)
                {
                    listEscuelaBase.AddRange(curso.Asignaturas);
                }

                if (traeEvaluaciones)
                {
                    foreach (Alumno alumno in curso.Alumnos)
                    {
                        listEscuelaBase.AddRange(alumno.Evaluaciones);
                        conteoEvaluaciones += alumno.Evaluaciones.Count;
                    }
                }
            }

            return (listEscuelaBase, conteoEvaluaciones);
        }

        public List<ObjetoEscuelaBase> GetObjetoEscuelaBases()
        {
            List<ObjetoEscuelaBase> listEscuelaBase = new List<ObjetoEscuelaBase>();

            listEscuelaBase.Add(Escuela);
            listEscuelaBase.AddRange(Escuela.Cursos);
            
            foreach (Curso curso in Escuela.Cursos)
            {
                listEscuelaBase.AddRange(curso.Asignaturas);
                listEscuelaBase.AddRange(curso.Alumnos);

                foreach (Alumno alumno in curso.Alumnos)
                {
                    listEscuelaBase.AddRange(alumno.Evaluaciones);
                }
            }

            return listEscuelaBase;
        }

        #region Metodos de carga
        private void CargarEvaluaciones()
        {
            foreach (Curso curso in Escuela.Cursos)
            {
                foreach (Asignatura asignatura in curso.Asignaturas)
                {
                    foreach (Alumno alumno in curso.Alumnos)
                    {
                        var rnd = new Random(System.Environment.TickCount);

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
        #endregion
    }
}



