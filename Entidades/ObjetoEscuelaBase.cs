using System;

namespace CorEscuela.Entidades
{
    /*
      Clase Abstracta
    1. Permite la herencia para crear jerarquias 
    2. No permite una instacia o creacion de objetos
    */
    
    public abstract class ObjetoEscuelaBase
    {
        public string UniqueId { get; private set; } = Guid.NewGuid().ToString();
        public string Nombre { get; set; }
        
        public override string ToString()
        {
            return $"{Nombre},{UniqueId}";
        }
    }
}
