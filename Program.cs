
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
            EscuelaEngine engine = new EscuelaEngine();
            engine.Inicializar();
            ImpimirCursosEscuela(engine.Escuela);
            
            var listaObjetoEscuala = engine.GetObjetoEscuelaBases();
            engine.Escuela.LimpiarLugar();

            var dictmp = engine.GetDiccionarioObjetos();
            engine.ImprimirDiccionario(dictmp, true);
            
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