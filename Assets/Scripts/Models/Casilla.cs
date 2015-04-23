using System;
using UnityEngine;

[Serializable]
public class Casilla
{
    #region Private fields
    private EnumCasillaTipo tipo;
    private EnumCasillaEstado estado;
    private GameObject tuberia;
    private int? rotacion;
    private Color? colorLiquido;
    private float? porcentajeLleno;
    #endregion

    #region Public constructors
    public Casilla()
    {
        Tipo = EnumCasillaTipo.Vacio;
        Estado = EnumCasillaEstado.Cerrado;
        Tuberia = null;
        Rotacion = null;
        ColorLiquido = null;
        PorcentajeLleno = null;
    }

	public Casilla(EnumCasillaTipo tipo)
	{
		Tipo = tipo;
		Estado = EnumCasillaEstado.Cerrado;
		Tuberia = null;
		Rotacion = null;
		ColorLiquido = null;
		PorcentajeLleno = null;
	}

	public Casilla(EnumCasillaTipo tipo, EnumCasillaEstado estado)
	{
		Tipo = tipo;
		Estado = estado;
		Tuberia = null;
		Rotacion = null;
		ColorLiquido = null;
		PorcentajeLleno = null;
	}

    public Casilla(EnumCasillaTipo tipo, EnumCasillaEstado estado, int rotacion)
    {
        Tipo = tipo;
        Estado = estado;
        Rotacion = rotacion;
        Tuberia = null;
        ColorLiquido = null;
        PorcentajeLleno = null;
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

    /// <summary>
    /// Rotación [0, 90, 180, 270] - Aplica solamente a Tablero, nulo para el resto
    /// </summary>
    public int? Rotacion
    {
        get { return rotacion; }
        set { rotacion = value >= 0 && value <= 270 ? value : 0; }
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
    #endregion
}