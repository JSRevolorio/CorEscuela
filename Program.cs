
using System;
using System.Collections.Generic;
using CorEscuela.App;
using CorEscuela.Util;
using CorEscuela.Entidades;

namespace CorEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += AccionEvento;
            EscuelaEngine engine = new EscuelaEngine();
            engine.Inicializar();
            ImpimirCursosEscuela(engine.Escuela);
            
            var listaObjetoEscuala = engine.GetObjetoEscuelaBases();
            engine.Escuela.LimpiarLugar();

            var dictmp = engine.GetDiccionarioObjetos();
            engine.ImprimirDiccionario(dictmp, true);

            var reporteador = new Reporteador(engine.GetDiccionarioObjetos());
            var evalList = reporteador.GetListEvaluaciones();
            var listaAsg = reporteador.GetListAsignaturas();
            var listaEvalXAsig = reporteador.GetDicEvaluaXAsig();
            var listaPromXAsig = reporteador.GetPromeAlumnPorAsignatura();
            
        }

        private static void AccionEvento(object sender, EventArgs e)
        {
            Printer.WriteTitle("Evento Ejecutado");
        }

        private static void ImpimirCursosEscuela(Escuela escuela)
        {
            Printer.WriteTitle("Lista de curso");
            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    Console.WriteLine($"Nombre {curso.Nombre  }, Id  {curso.UniqueId}");
                }
            }
        }
    }
}