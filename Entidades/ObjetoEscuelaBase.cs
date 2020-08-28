using System;

namespace CorEscuela.Entidades
{
    public abstract class ObjetoEscuelaBase
    {
        public string UniqueId { get; private set; } = Guid.NewGuid().ToString();
        public string Nombre { get; set; }
        
    }
}


