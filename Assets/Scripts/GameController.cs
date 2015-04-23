using System.Linq;
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
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	private void Update() {
	}
	#endregion

	#region Private Methods
	private void FillPoints() {
		casillas = new Casilla[gameBoardSquareHorizontally][];

		for (int x = 0; x < this.gameBoardSquareHorizontally; x++) {

			casillas[x] = new Casilla[this.gameBoardSquareVertically];

			for (int y = 0; y < this.gameBoardSquareVertically; y++) {
				if ((x == 0 && y == 0) || (x == 0 && y == (this.gameBoardSquareVertically - 1)) ||
				    (x == (this.gameBoardSquareHorizontally - 1) && y == 0) || (x == (this.gameBoardSquareHorizontally - 1) && y == (this.gameBoardSquareVertically - 1))) {
					casillas[x][y] = new Casilla ();
				} else if (x == 0 || x == (this.gameBoardSquareHorizontally - 1) ||
				    y == 0 || y == (this.gameBoardSquareVertically - 1)) {
					casillas[x][y] = new Casilla (EnumCasillaTipo.Punto);
				} else {
					casillas[x][y] = new Casilla (EnumCasillaTipo.Tablero);
				}
			}
		}
	}

	/* TODO: Falta buscar */
	/*
	private Casilla GetRandomInputPoint() {
		Casilla testPoint = null;

		while(true) {
			testPoint = casillas.ElementAt(Random.Range (0, casillas.Count() - 1));
			if (!inputPoints.Contains(testPoint)) {
				break;
			}
		}

		return testPoint;
	}
	*/

	#endregion

	#region Public Methods
	#endregion
}
