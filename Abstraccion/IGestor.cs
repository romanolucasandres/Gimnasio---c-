using System.Collections.Generic;

namespace Abstraccion
{
    public interface IGestor<T> where T : IEntidad
    {
        void Guardar(T BEobjeto);
        void Baja(T BEObjeto);
        List<T> Listar();
    }
}
