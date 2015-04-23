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

    public Vector2 gameBoardSquare = new Vector2(100.0f, 100.0f);
    public int gameBoardSquareHorizontally = 10;
    public int gameBoardSquareVertically = 12;
    public int numberOfColors = 1;
    public int gameSeed = 1;
	#endregion

    #region Private fields
    private Casilla[][] casillas;
	private List<Casilla> inputPoints;
    private List<Casilla> outputPoints;
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
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	private void Update() {
	}
	#endregion

	#region Private Methods
	private void FillPoints() {
        int w = gameBoardSquareHorizontally; // width
        int h = gameBoardSquareVertically; // height

        casillas = new Casilla[w][];

        for (int x = 0; x < w; x++)
        {
            casillas[x] = new Casilla[h];

            for (int y = 0; y < h; y++)
            {
                if ((x == 0 && y == 0) || (x == 0 && y == (h - 1)) || (x == (w - 1) && y == 0) || (x == (w - 1) && y == (h - 1)))
                {
                    // Four corners
					casillas[x][y] = new Casilla ();
                }
                else if (x == 0 || x == (w - 1) || y == 0 || y == (h - 1))
                {
                    // Board margins
					casillas[x][y] = new Casilla (EnumCasillaTipo.Punto);
				}
                else 
                {
                    // Board
					casillas[x][y] = new Casilla (EnumCasillaTipo.Tablero);
				}
			}
		}
	}

	private Casilla GetRandomPoint(bool isInput) {
		Casilla testPoint;
	    List<Casilla> points = BoardHelper.GetPoints(casillas);

		while(true) {
            testPoint = points.ElementAt(Random.Range(0, points.Count() - 1));
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

	#endregion

	#region Public Methods
	#endregion
}
