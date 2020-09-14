using System;
using System.Collections.Generic;
using System.Linq;
using CorEscuela.Entidades;

namespace CorEscuela.App
{
    public class Reporteador
    {
        public Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> diccionarioObjetoEscuela;
        public Reporteador(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> diccionarioObjetoEscuela)
        {
            if(diccionarioObjetoEscuela == null)
            {
                throw new ArgumentNullException(nameof(diccionarioObjetoEscuela));
            }

            this.diccionarioObjetoEscuela = diccionarioObjetoEscuela;
        }

        public IEnumerable<Evaluacion> GetListEvaluaciones()
        {
            if (this.diccionarioObjetoEscuela.TryGetValue(LlaveDiccionario.Evaluacion, out IEnumerable<ObjetoEscuelaBase> listEvaluaciones))
            {
                return listEvaluaciones.Cast<Evaluacion>();
            }
            else
            {
                return new List<Evaluacion>();
            }

        }

        public IEnumerable<string> GetListAsignaturas()
        {
            return GetListAsignaturas(
                    out var dummy);
        }

        public IEnumerable<string> GetListAsignaturas(out IEnumerable<Evaluacion> listaEvaluaciones)
        {
            listaEvaluaciones = GetListEvaluaciones();

            return (from Evaluacion ev in listaEvaluaciones
                    select ev.Asignatura.Nombre).Distinct();
        }

        public Dictionary<string, IEnumerable<Evaluacion>> GetDicEvaluaXAsig()
        {
            var dictaRta = new Dictionary<string, IEnumerable<Evaluacion>>();

            var listaAsig = GetListAsignaturas(out var listaEval);

            foreach (var asig in listaAsig)
            {
                var evalsAsig = from eval in listaEval
                                where eval.Asignatura.Nombre == asig
                                select eval;

                dictaRta.Add(asig, evalsAsig);
            }

            return dictaRta;
        }

        public Dictionary<string, IEnumerable<object>> GetPromeAlumnPorAsignatura()
        {
            var rta = new Dictionary<string, IEnumerable<object>>();
            var dicEvalXAsig = GetDicEvaluaXAsig();

            foreach (var asigConEval in dicEvalXAsig)
            {
                var promsAlumn = from eval in asigConEval.Value
                            group eval by new{
                                 eval.Alumno.UniqueId,
                                 eval.Alumno.Nombre}
                            into grupoEvalsAlumno
                            select new AlumnoPromedio
                            { 
                                alumnoid = grupoEvalsAlumno.Key.UniqueId,
                                alumno = grupoEvalsAlumno.Key.Nombre,
                                promedio = grupoEvalsAlumno.Average(evaluacion => evaluacion.Nota)
                            };
                 
                 rta.Add(asigConEval.Key, promsAlumn);
            }

            return rta;
        }

    }
}