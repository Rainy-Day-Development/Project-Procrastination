using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayer : Player {

    public float moveSpeed = 10.0f;
    public bool GUIEnabled = false;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.players[GameManager.instance.currentPlayerIndex] == this && GameManager.instance.IsPlayersTurn){
            transform.GetComponent<Renderer>().material.color = Color.green;
            if (GameManager.instance.players[GameManager.instance.currentPlayerIndex].AttackCounter == 0 &&
                GameManager.instance.players[GameManager.instance.currentPlayerIndex].MovementCounter == 0) {
                GameManager.instance.nextTurn();
            }
        }
        else
        {
            transform.GetComponent<Renderer>().material.color = Color.white;
        }

        if (HP <= 0) {
            transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            transform.GetComponent<Renderer>().material.color = Color.red;
        }

    }

    public override void turnUpdate(){

        //Highlight functionality


        if (Vector3.Distance(MoveDestination, transform.position) > 0.1f && MovementCounter > 0 && GameManager.instance.IsPlayersTurn) {
            transform.position += (MoveDestination - transform.position).normalized * moveSpeed * Time.deltaTime;
            IsMoving = true;
            if (Vector3.Distance(MoveDestination, transform.position) <= 0.1f) {
                transform.position = MoveDestination;
                GameManager.instance.RemoveTileHighlight();
                IsMoving = false;
                MovementCounter = 0;
            }
        }
        base.turnUpdate();
    }


    public override void TurnOnGUI(){
        float ButtonHeight = 50f;
        float ButtonWidth = 150f;
        Rect ButtonRect = new Rect(0, Screen.height - ButtonHeight*3, ButtonWidth, ButtonHeight);


        //move button
        if (GUI.Button(ButtonRect, "Move")){
            if (!CanMove){
                GameManager.instance.RemoveTileHighlight();
                CanMove = true;
                Attacking = false;
                GameManager.instance.HighlightTileAt(GridPosition, Color.blue, MovementDistance);

            }else{
                CanMove= false;
                Attacking = false;
                GameManager.instance.RemoveTileHighlight();
            }

        }

        //attack button
        ButtonRect = new Rect(0, Screen.height - ButtonHeight * 2, ButtonWidth, ButtonHeight);
        if (GUI.Button(ButtonRect, "Attack")){
            if (!Attacking){
                GameManager.instance.RemoveTileHighlight();
                CanMove = false;
                Attacking = true;
                GameManager.instance.HighlightTileAt(GridPosition, Color.red, AttackDistance);
            }
            else{
                CanMove= false;
                Attacking = false;
                GameManager.instance.RemoveTileHighlight();
            }
        }

        //end turn button
        ButtonRect = new Rect(0, Screen.height - ButtonHeight * 1, ButtonWidth, ButtonHeight);
        if (GUI.Button(ButtonRect, "End All Players Turn")) {
            CanMove= false;
            Attacking = false;
            GameManager.instance.RemoveTileHighlight();
            GameManager.instance.nextTurn();
        }


        base.TurnOnGUI();

    }

    private void OnMouseDown()
    {
        if (GameManager.instance.IsPlayersTurn)
        {
            GameManager.instance.currentPlayerIndex = this.PlayerIndex;
            GameManager.instance.RemoveTileHighlight();
        }
    }

}
