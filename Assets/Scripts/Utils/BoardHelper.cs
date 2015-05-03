using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        /// <summary>
        /// Devuelve si la posición de una celda es válida
        /// </summary>
        /// <param name="cellPosX">Posición X de la celda a probar</param>
        /// <param name="cellPosY">Posición Y de la celda a probar</param>
        /// <param name="currentColorAssigned">Lista de celdas ya cargadas en el sistema</param>
        /// <param name="minPosX">X Mínima</param>
        /// <param name="minPosY">Y Mínima</param>
        /// <param name="maxPosX">X Máxima</param>
        /// <param name="maxPosY">Y Máxima</param>
        /// <returns>true si es válida</returns>
		private static bool IsValidCell(int cellPosX, int cellPosY, IEnumerable<Casilla> currentColorAssigned, 
		                               int minPosX, int minPosY, int maxPosX, int maxPosY)
		{
			// Comprobamos primero el eje de las X
            if (minPosX > cellPosX || cellPosX > maxPosX) 
			{
                return false;
			}

			// Comprobamos el eje de las Y
            if (minPosY > cellPosY || cellPosY > maxPosY)
			{
                return false;
			}

			// Comprobamos si la linea de la casilla atraviesa una casilla que ya tiene este color
            if (currentColorAssigned.Any(a => a.PosicionX == cellPosX && a.PosicionY == cellPosY))
			{
				return false;
			}

			return true;
		}

        /// <summary>
        /// Valida que la celda sea un punto y de salida
        /// </summary>
        /// <param name="testCell"></param>
        /// <returns>true si es final</returns>
		private static bool IsFinalCell(Casilla testCell)
		{
			if (testCell.Tipo == EnumCasillaTipo.Punto && testCell.Estado == EnumCasillaEstado.Salida)
			{
                return true;
			}

			return false;
		}

        /// <summary>
        /// Devuelve la lista de puntos, entre el actual y la nueva posicion válida
        /// </summary>
        /// <param name="color"></param>
        /// <param name="inputCell"></param>
        /// <param name="outputCell"></param>
        /// <param name="matriz"></param>
        /// <param name="gbsHorizontally"></param>
        /// <param name="gbsVertically"></param>
        public static List<Casilla> GetRoute(Color color, Casilla inputCell, Casilla outputCell, IEnumerable<Casilla> matriz,
            int gbsHorizontally, int gbsVertically)
		{
			System.Random r = new System.Random();
			var validDirections = Enum.GetValues(typeof(EnumFacingDirection)).Cast<EnumFacingDirection>().ToList();

            int currentPosX = inputCell.PosicionX;
            int currentPosY = inputCell.PosicionY;
            int orden = 0;

            List<Casilla> currentRoute = new List<Casilla>();

            // Añadimos la entrada
            inputCell.Orden = orden;
            inputCell.ColorLiquido = color;
            currentRoute.Add(inputCell);

            // Subimos el orden
            orden++;

            // Repetir mientras no se alcanze el punto final
            while (true)
            {
                Casilla destCasilla = null;

                // Repetir mientras no sea válido
                while (true)
                {
                    // Obtenemos una dirección aleatoria
                    EnumFacingDirection direction = validDirections.ElementAt(r.Next(0, validDirections.Count()));
                    // Obtenemos un número de movimientos aleatorio, en un rango de 1 a 4
                    int moveCells = r.Next(1, 4);
                    // Calculamos la nueva posicion a probar
                    int newPosX = currentPosX;
                    int newPosY = currentPosY;
                    // Obtenemos el rango de posiciones por si acaso
                    List<Vector2> rangedPosList = new List<Vector2>();

                    switch (direction)
                    {
                        case EnumFacingDirection.North:
                            // Aumentamos la Y
                            newPosY = currentPosY + moveCells;
                            rangedPosList.AddRange(Enumerable.Range(currentPosY, moveCells).Select(v => new Vector2(newPosX, v)));
                            break;
                        case EnumFacingDirection.South:
                            // Disminuimos la Y
                            newPosY = currentPosY - moveCells;
							rangedPosList.AddRange(Enumerable.Range(currentPosY - moveCells, moveCells).Select(v => new Vector2(newPosX, v)));
                            break;
                        case EnumFacingDirection.West:
                            // Disminuimos la X
                            newPosX = currentPosX - moveCells;
							rangedPosList.AddRange(Enumerable.Range(currentPosX - moveCells, moveCells).Select(v => new Vector2(v, newPosY)));
                            break;
                        case EnumFacingDirection.East:
                            // Aumentamos la X
                            newPosX = currentPosX + moveCells;
                            rangedPosList.AddRange(Enumerable.Range(currentPosX, moveCells).Select(v => new Vector2(v, newPosY)));
                            break;
                    }

                    // Comprobamos si la celda es válida, si no lo es, continuamos con el while
                    if (!IsValidCell(newPosX, newPosY, currentRoute, 0, 0, gbsHorizontally, gbsVertically)) continue;

                    // Obtenemos y modificamos la casilla de destino, y verificamos que no sea una casilla vacia
                    destCasilla = matriz.SingleOrDefault(s => s.PosicionX == newPosX 
                        && s.PosicionY == newPosY && s.Tipo != EnumCasillaTipo.Vacio);

                    // Si es nula, continuamos con el while
                    if (destCasilla == null) continue;

                    // Guardamos las casillas entre la ultima posicion (sin incluir) y la actual válida
                    // TODO: Verificar qué casillas y posiciones se incluyen
                    foreach (Vector2 vector2 in rangedPosList)
                    {
                        // Buscamos la casilla intermedia
                        Casilla interCasilla = matriz.Single(s => s.PosicionX == (int)vector2.x && s.PosicionY == (int)vector2.y);
                        // Cambiamos los parametros y la añadimos
                        interCasilla.Orden = orden;
                        interCasilla.ColorLiquido = color;
                        currentRoute.Add(interCasilla);
                        // Subimos el orden
                        orden++;
                    }

                    // Cambiamos el valor de la posición actual
                    currentPosX = newPosX;
                    currentPosY = newPosY;

                    // Salimos del bucle de validez
                    break;
                }

                // Verificamos si el punto ultimo asignado es el final, y si no, continuamos
                if (!IsFinalCell(destCasilla)) continue;

                break;
            }

            // TODO: Añadimos la salida??
            outputCell.Orden = orden;
            outputCell.ColorLiquido = color;

            currentRoute.Add(outputCell);

            return currentRoute;
		}
    }
}
