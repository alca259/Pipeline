using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Utils
{
    public static class BoardHelper
    {
        public static Casilla Find(IEnumerable<Casilla> matrix, Func<Casilla, bool> filter)
        {
            return matrix
                .Where(w => w.Tipo != EnumCasillaTipo.Vacio)
                .FirstOrDefault(filter);
        }

        public static List<Casilla> GetPoints(IEnumerable<Casilla> matrix)
        {
            return matrix.Where(casilla => casilla.Tipo == EnumCasillaTipo.Punto).ToList();
        }
    }
}
