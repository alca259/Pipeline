using System;

namespace Assets.Scripts.Utils
{
    public static class BoardHelper
    {
        public static Casilla Find(Casilla[][] matrix, Casilla objectToFind)
        {
            int w = matrix.GetLength(0); // width
            int h = matrix.GetLength(1); // height

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (matrix[x][y].Equals(objectToFind))
                        return matrix[x][y];
                }
            }

            return null;
        }
    }
}
