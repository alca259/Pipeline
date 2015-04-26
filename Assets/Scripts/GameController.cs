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
    private List<Casilla> casillas;
	private List<Casilla> inputPoints = new List<Casilla>();
	private List<Casilla> outputPoints = new List<Casilla>();
	#endregion

	#region Override Methods
	/// <summary>
	/// Start this instance.
	/// </summary>
	private void Start() {
		Random.seed = gameSeed;
		FillPoints();
	    inputPoints.Add(GetRandomPoint(true));
	    outputPoints.Add(GetRandomPoint(false));
        DrawPoints();
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	private void Update() {
	}
	#endregion

	#region Private Methods
	private void FillPoints() {

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
		            break;
		        }
		    }
		    else
		    {
                if (!outputPoints.Contains(testPoint))
                {
                    break;
                }
		    }
		}

		return testPoint;
	}

	private void DrawPoints() 
	{
		Vector2 boardSizes = boardObject.GetComponent<BoxCollider2D>().size;

		float xBoard = boardSizes.x / 2;
		float yBoard = boardSizes.y / 2;

		float xCasilla = 1;
		float yCasilla = 1;

		float xBoardFix = 0.22f;
		float yBoardFix = 0.22f;

		for (int x = 0; x < gbsHorizontally; x++)
		{
			for (int y = 0; y < gbsVertically; y++)
			{
				Casilla actual = BoardHelper.Find(casillas, f => f.PosicionX.Equals(x) && f.PosicionY.Equals(y));

			    if (actual == null) continue;

				if (actual.Tipo.Equals(EnumCasillaTipo.Tablero)) 
                {
					float x1 = 0.0f;
					float y1 = 0.0f;

					x1 = (xCasilla * actual.PosicionX) - xBoard + xCasilla - xBoardFix;
					y1 = (yCasilla * actual.PosicionY) - yBoard + yCasilla - yBoardFix;

					Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, (float)actual.Rotacion);

					Instantiate(this.pipeCurve, new Vector3(x1, y1, 0.0f), rotation);
				}
			}
		}
	}

	#endregion

	#region Public Methods
	#endregion
}
