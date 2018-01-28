using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour {

    public Vector2 gridPosition = Vector2.zero;

    public List<Tile> Neighbours = new List<Tile>();
    public int MovementCost = 1;
	// Use this for initialization
	void Start () {
        GenerateNeighbours();
	}

    void GenerateNeighbours() {
        Neighbours = new List<Tile>();
        //Up
        if (gridPosition.y > 0) {
            Vector2 n = new Vector2(gridPosition.x, gridPosition.y - 1);
            Neighbours.Add(GameManager.instance.map[(int)n.x][(int)n.y]);
        }
        //Down
        if (gridPosition.y < GameManager.instance.mapSizeY - 1){
            Vector2 n = new Vector2(gridPosition.x, gridPosition.y + 1);
            Neighbours.Add(GameManager.instance.map[(int)n.x][(int)n.y]);
        }
        //Left
        if (gridPosition.x > 0){
            Vector2 n = new Vector2(gridPosition.x - 1, gridPosition.y);
            Neighbours.Add(GameManager.instance.map[(int)n.x][(int)n.y]);
        }
        //Right
        if (gridPosition.x < GameManager.instance.mapSizeX - 1){
            Vector2 n = new Vector2(gridPosition.x + 1, gridPosition.y);
            Neighbours.Add(GameManager.instance.map[(int)n.x][(int)n.y]);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseEnter() {
        /*
        if (GameManager.instance.players[GameManager.instance.currentPlayerIndex].CanMove) {
            transform.GetComponent<Renderer>().material.color = Color.blue;
        }else if (GameManager.instance.players[GameManager.instance.currentPlayerIndex].Attacking){
            transform.GetComponent<Renderer>().material.color = Color.red;
        }
        */


    }

    void OnMouseExit(){
        //transform.GetComponent<Renderer>().material.color = Color.white;
    }

    private void OnMouseDown(){
        if (GameManager.instance.IsPlayersTurn)
        {
            if (GameManager.instance.players[GameManager.instance.currentPlayerIndex].CanMove && GameManager.instance.players[GameManager.instance.currentPlayerIndex].MovementCounter > 0
                && GameManager.instance.players[GameManager.instance.currentPlayerIndex].IsMoving == false)
            {
                GameManager.instance.moveCurrentPlayer(this);
                GameManager.instance.players[GameManager.instance.currentPlayerIndex].GridPosition = this.gridPosition;
            }
            else if (GameManager.instance.players[GameManager.instance.currentPlayerIndex].Attacking)
            {
                GameManager.instance.AttackWithCurrentPlayer(this);
            }
        }

       
    }

}
