using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Vector2 GridPosition = Vector2.zero;

    public Vector3 MoveDestination;



    public bool IsMoving = false;
    public bool CanMove= false;
    public bool Attacking = false;

    public int HP = 25;
    public float AttackChance = 0.75f;
    public float DefenseReduction = 0.15f;
    public float DamageBase = 5;
    public float DamageRollSides = 6; //d6 damage
    public int AttackRange = 1;

    public string PlayerName= "George";
    public int MovementDistance = 6;
    public int MovementCounter = 1;
    public int AttackDistance = 1;
    public int AttackCounter = 1;

    public int PlayerIndex = -1;


    private void Awake(){
        MoveDestination = transform.position;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public virtual void turnUpdate() {

    }

    public virtual void TurnOnGUI() {

    }

    public virtual void TurnOffGUI() {

    }
}
