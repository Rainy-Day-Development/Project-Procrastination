using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : Player {
    public int fuck = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void turnUpdate()
    {
        base.turnUpdate();
    }

    public override void TurnOnGUI()
    {
        float ButtonHeight = 50f;
        float ButtonWidth = 150f;


        Rect ButtonRect = new Rect(0, Screen.height - ButtonHeight * 3, ButtonWidth, ButtonHeight);
        /*
        //move button
        if (GUI.Button(ButtonRect, "Move"))
        {
            if (!CanMove)
            {
                GameManager.instance.RemoveTileHighlight();
                CanMove = true;
                Attacking = false;
                GameManager.instance.HighlightTileAt(GridPosition, Color.blue, MovementDistance);

            }
            else
            {
                CanMove = false;
                Attacking = false;
                GameManager.instance.RemoveTileHighlight();
            }

        }

        //attack button
        ButtonRect = new Rect(0, Screen.height - ButtonHeight * 2, ButtonWidth, ButtonHeight);
        if (GUI.Button(ButtonRect, "Attack"))
        {
            if (!Attacking)
            {
                GameManager.instance.RemoveTileHighlight();
                CanMove = false;
                Attacking = true;
                GameManager.instance.HighlightTileAt(GridPosition, Color.red, MovementDistance);
            }
            else
            {
                CanMove = false;
                Attacking = false;
                GameManager.instance.RemoveTileHighlight();
            }
        }
        */

        //end turn button
        ButtonRect = new Rect(0, Screen.height - ButtonHeight * 1, ButtonWidth, ButtonHeight);
        if (GUI.Button(ButtonRect, "End Turn"))
        {
            CanMove = false;
            Attacking = false;
            AttackCounter = 1;
            MovementCounter = 1;
            GameManager.instance.RemoveTileHighlight();
            GameManager.instance.nextTurn();
            
        }


        base.TurnOnGUI();

    }


    private void OnMouseDown()
    {
        if (!GameManager.instance.IsPlayersTurn)
        {
            GameManager.instance.currentPlayerIndex = this.PlayerIndex;
            GameManager.instance.RemoveTileHighlight();
        }
    }

}
