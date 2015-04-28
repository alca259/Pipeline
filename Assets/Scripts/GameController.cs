using System.Linq;
using Assets.Scripts.Utils;
using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	#region Public fields
	public GameObject boardObject;
	public GameObject pipeRect;
	public GameObject pipeCurve;
	public GameObject pipeThreeSides;
	public GameObject pipeFourSides;
	
    public int gbsHorizontally = 10;
    public int gbsVertically = 12;
    public int numberOfColors = 1;
    public int gameSeed = 1;
	#endregion

    #region Private fields
	private int maxColors = 8;
    private List<Casilla> casillas;
	private List<Casilla> inputPoints = new List<Casilla>();
	private List<Casilla> outputPoints = new List<Casilla>();
	private Color[] colors = new Color[] {
		Color.blue, Color.red, Color.yellow, Color.green,
		Color.magenta, new Color(0.6F, 0.0F, 0.0F), Color.white, Color.black;
	};
	#endregion

	#region Override Methods
	/// <summary>
	/// Start this instance.
	/// </summary>
	private void Start() {
		// Set current seed
		Random.seed = gameSeed;

		// Creating the positions for the grid
		FillPoints();

		// Validation
		if (numberOfColors < 0) {
			numberOfColors = 1;
		} else if (numberOfColors >= maxColors) {
			numberOfColors = maxColors;
		}

		// Calculate input/output and routes for each color
		for(int colorIdx = 1; colorIdx <= numberOfColors; colorIdx++)
		{
	    	inputPoints.Add(GetRandomPoint(true));
	    	outputPoints.Add(GetRandomPoint(false));

			GenerateRoute(colorIdx);
		}
        //DrawPoints();
		/*
		// Draws a blue line from this transform to the target
        Gizmos.color = Color.blue;
        Gizmos.DrawLine (transform.position, target.position);
		 */
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	private void Update() {
	}
	#endregion

	#region Private Methods
	private void FillPoints() 
	{

        casillas = new List<Casilla>();

        for (int x = 0; x < gbsHorizontally; x++)
        {
            for (int y = 0; y < gbsVertically; y++)
            {
                if ((x == 0 && y == 0) || (x == 0 && y == (gbsVertically - 1)) ||
                    (x == (gbsHorizontally - 1) && y == 0) || (x == (gbsHorizontally - 1) && y == (gbsVertically - 1)))
                {
                    // Four corners
					casillas.Add(new Casilla(EnumCasillaTipo.Vacio, EnumCasillaEstado.Cerrado, x, y));
                }
                else if (x == 0 || x == (gbsHorizontally - 1) || y == 0 || y == (gbsVertically - 1))
                {
                    // Board margins
                    casillas.Add(new Casilla(EnumCasillaTipo.Punto, EnumCasillaEstado.Cerrado, x, y));
				}
                else 
                {
                    // Board
                    casillas.Add(new Casilla(EnumCasillaTipo.Tablero, EnumCasillaEstado.Cerrado, x, y));
				}
			}
		}
	}

	private Casilla GetRandomPoint(bool isInput) {
	    List<Casilla> points = BoardHelper.GetPoints(casillas);
		if (!points.Any()) return null;

        Casilla testPoint;

		while(true) 
        {
            testPoint = points.ElementAt(Random.Range(0, points.Count()));
		    if (isInput)
		    {
		        if (!inputPoints.Contains(testPoint))
		        {
					testPoint.Estado = EnumCasillaEstado.Entrada;
		            break;
		        }
		    }
		    else
		    {
                if (!outputPoints.Contains(testPoint))
                {
					testPoint.Estado = EnumCasillaEstado.Salida;
                    break;
                }
		    }
		}

		return testPoint;
	}

	private void GenerateRoute(int colorIdx) {

	}

	private void DrawPoints() 
	{
		// Tal vez deberiamos obtener el tamaño de la casilla en base al prefab, pero asi se calcula bien
		float xCasilla = 1;
		float yCasilla = 1;

		// Calculamos el numero de posiciones posibles del tablero
		float xBoard = gbsHorizontally * xCasilla;
		float yBoard = gbsVertically * yCasilla;

		// Calculamos el punto inferior izquierda del rectangulo (Ya que se pintan a partir del punto 0,0)
		// Para calcularlo, obtenemos la distancia entre el medio y el lateral y la convertimos a negativo
		float xBoardCornerLeftBottom = xBoard/2 * -1;
		float yBoardCornerLeftBottom = yBoard/2 * -1;

		for (int x = 0; x < gbsHorizontally; x++)
		{
			for (int y = 0; y < gbsVertically; y++)
			{
				Casilla actual = BoardHelper.Find(casillas, f => f.PosicionX.Equals(x) && f.PosicionY.Equals(y));

				if (actual == null) continue;
				
				if (actual.Tipo.Equals(EnumCasillaTipo.Tablero)) 
				{
					// Teniendo el punto inferior izquierda, obtenemos la distancia del cuadrado pequeño y vamos pintando segun la posicion
					// Que como son tableros, la X y la Y empiezan con valor 1 y no 0
					float x1 = xBoardCornerLeftBottom + xCasilla/2 + (xCasilla * actual.PosicionX);
					float y1 = yBoardCornerLeftBottom + yCasilla/2 + (yCasilla * actual.PosicionY);

					// Calculamos la rotacion en el eje Z
					Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, (float)actual.Rotacion);

					// Instanciamos el objeto
					Instantiate(this.pipeCurve, new Vector3(x1, y1, 0.0f), rotation);
				}
			}
		}
	}

	#endregion

	#region Public Methods
	#endregion
}
