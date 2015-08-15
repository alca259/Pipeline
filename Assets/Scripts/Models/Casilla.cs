using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Casilla
{
    #region Private fields
    private EnumCasillaTipo tipo;
    private EnumCasillaEstado estado;
    private GameObject tuberia;
    private int posicionX;
    private int posicionY;
    private int? rotacion;
    private Color? colorLiquido;
    private float? porcentajeLleno;
    private int? orden;

	private readonly List<int> validRotations = new List<int> {0, 90, 180, 270};
    #endregion

    #region Public constructors
    public Casilla()
    {
        Tipo = EnumCasillaTipo.Vacio;
        Estado = EnumCasillaEstado.Cerrado;
        Tuberia = null;
        PosicionX = 0;
        PosicionY = 0;
        Rotacion = null;
        ColorLiquido = null;
        PorcentajeLleno = null;
    }

	public Casilla(EnumCasillaTipo tipo)
	{
		Tipo = tipo;
		Estado = EnumCasillaEstado.Cerrado;
		Tuberia = null;
        PosicionX = 0;
        PosicionY = 0;
		Rotacion = null;
		ColorLiquido = null;
		PorcentajeLleno = null;
	    Orden = null;
	}

	public Casilla(EnumCasillaTipo tipo, EnumCasillaEstado estado)
	{
		Tipo = tipo;
		Estado = estado;
		Tuberia = null;
        PosicionX = 0;
        PosicionY = 0;
		Rotacion = null;
		ColorLiquido = null;
		PorcentajeLleno = null;
        Orden = null;
	}

    public Casilla(EnumCasillaTipo tipo, EnumCasillaEstado estado, int posicionX, int posicionY)
    {
        Tipo = tipo;
        Estado = estado;
        Tuberia = null;
        PosicionX = posicionX;
        PosicionY = posicionY;
        Rotacion = null;
        ColorLiquido = null;
        PorcentajeLleno = null;
        Orden = null;
    }

    public Casilla(EnumCasillaTipo tipo, EnumCasillaEstado estado, int posicionX, int posicionY, int rotacion)
    {
        Tipo = tipo;
        Estado = estado;
        Tuberia = null;
        PosicionX = posicionX;
        PosicionY = posicionY;
        Rotacion = rotacion;
        ColorLiquido = null;
        PorcentajeLleno = null;
        Orden = null;
    }
    #endregion

    #region Public properties
    /// <summary>
    /// Tipo de casilla: [Vacío - 0, Punto - 1, Tablero - 2]
    /// </summary>
    public EnumCasillaTipo Tipo
    {
        get { return tipo; }
        private set { tipo = value; }
    }

    /// <summary>
    /// Estado [Entrada, Salida, Cerrado] - Aplica solamente a Punto, nulo para el resto
    /// </summary>
    public EnumCasillaEstado Estado
    {
        get { return estado; }
        set { estado = value; }
    }

    /// <summary>
    /// Tubería (GameObject) - Aplica solamente a Tablero, nulo para el resto
    /// </summary>
    public GameObject Tuberia
    {
        get { return tuberia; }
        set { tuberia = value; }
    }

    public int PosicionX
    {
        get { return posicionX; }
        set { posicionX = value; }
    }

    public int PosicionY
    {
        get { return posicionY; }
        set { posicionY = value; }
    }

    /// <summary>
    /// Rotación [0, 90, 180, 270] - Aplica solamente a Tablero, nulo para el resto
    /// </summary>
    public int? Rotacion
    {
        get { return rotacion; }
        set {
			bool assigned = false;

			if (value >= 0 && value <= 270) {
				rotacion = value;
				assigned = true;
			}

			if (!assigned) {
				rotacion = validRotations.ElementAt(UnityEngine.Random.Range(0, validRotations.Count()));
			}
		}
    }

    /// <summary>
    /// Color del liquido actual - Solamente para tablero y punto
    /// </summary>
    public Color? ColorLiquido
    {
        get { return colorLiquido; }
        set { colorLiquido = value; }
    }

    /// <summary>
    /// Porcentaje de la tuberia completado - Solamente para tablero
    /// </summary>
    public float? PorcentajeLleno
    {
        get { return porcentajeLleno; }
        set { porcentajeLleno = value; }
    }

    public int? Orden
    {
        get { return orden; }
        set { orden = value ?? 0; }
    }

    #endregion
}