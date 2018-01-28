using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public GameObject tilePrefab;
    public GameObject userPlayerPrefab;
    public GameObject EnemyPlayerPrefab;


    public List<List<Tile>> map = new List<List<Tile>>();
    public List<Player> players = new List<Player>();
    public int currentPlayerIndex = 0;
    public List<Player> EnemyPlayers = new List<Player>();
    public bool IsPlayersTurn = false;

    public int mapSizeX = 10;
    public int mapSizeY = 10;


    private void Awake(){
        instance = this;    
    }

    // Use this for initialization
    void Start() {
        generateMap();
        generatePlayers();
        IsPlayersTurn = true;
    }

    // Update is called once per frame
    void Update() {
        if (IsPlayersTurn)
        {
            if (players[currentPlayerIndex].HP > 0)
            {
                players[currentPlayerIndex].turnUpdate();
            }
            else
            {
                nextTurn();
            }
        }
        else {
                
        }
    }

    private void OnGUI(){
        if (IsPlayersTurn)
        {
            players[currentPlayerIndex].TurnOnGUI();
        }
        else {
            EnemyPlayers[currentPlayerIndex].TurnOnGUI();
        }
    }    

    public void nextTurn() {
        if (IsPlayersTurn)
        {
            IsPlayersTurn = false;
            for (int i = 0; i < players.Count; i++) {
                players[i].transform.GetComponent<Renderer>().material.color = Color.white;
            }
            for (int i = 0; i < EnemyPlayers.Count; i++)
            {
                EnemyPlayers[i].MovementCounter = 1;
                EnemyPlayers[i].AttackCounter = 1;
            }
        }
        else {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].MovementCounter = 1;
                players[i].AttackCounter = 1;
            }
            IsPlayersTurn = true;
        }

    }
    
    public void HighlightTileAt(Vector2 OriginLocation, Color HighlightColor, int Range) {
        List <Tile> HighlightedTiles = TileHighlight.FindHighlight(map[(int)OriginLocation.x][(int)OriginLocation.y], Range);

        foreach (Tile t in HighlightedTiles) {
            t.transform.GetComponent<Renderer>().material.color = HighlightColor;
        }

    }

    public void RemoveTileHighlight() {
        for (int i = 0; i < mapSizeX; i++) {
            for (int j = 0; j < mapSizeY; j++) {
                map[i][j].transform.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }



    public void moveCurrentPlayer(Tile destination) {
        if (destination.transform.GetComponent<Renderer>().material.color != Color.white && IsPlayersTurn)
        {
            players[currentPlayerIndex].MoveDestination = destination.transform.position + 1.5f * Vector3.up;
        }
        else {
            Debug.Log("Destination invalid");
        }

    }


    public void AttackWithCurrentPlayer(Tile destination){
        if (destination.transform.GetComponent<Renderer>().material.color != Color.white)
        {
            Player target = null;
            foreach (Player p in players)
            {
                if (p.GridPosition == destination.gridPosition)
                {
                    target = p;
                }
            }
            if (target != null)
            {
                //attack

                if (players[currentPlayerIndex].GridPosition.x >= target.GridPosition.x - players[currentPlayerIndex].AttackRange && players[currentPlayerIndex].GridPosition.x <= target.GridPosition.x + players[currentPlayerIndex].AttackRange &&
                    players[currentPlayerIndex].GridPosition.y >= target.GridPosition.y - players[currentPlayerIndex].AttackRange && players[currentPlayerIndex].GridPosition.x <= target.GridPosition.x + players[currentPlayerIndex].AttackRange
                    && players[currentPlayerIndex].AttackCounter > 0)
                {
                    float hitChance = Random.Range(0.0f, 1.0f);
                    Debug.Log(hitChance);
                    bool hit = (hitChance <= players[currentPlayerIndex].AttackChance);
                    if (hit)
                    {
                        int damage = (int)Mathf.Floor((players[currentPlayerIndex].DamageBase + (Random.Range(0, players[currentPlayerIndex].DamageRollSides))));
                        target.HP -= damage;
                        Debug.Log(players[currentPlayerIndex].PlayerName + " successfully hit " + target.PlayerName + " for " + damage + " damage!");
                    }
                    else
                    {
                        Debug.Log(players[currentPlayerIndex].PlayerName + " missed " + target.PlayerName);
                    }
                    players[currentPlayerIndex].AttackCounter = 0;
                }
                else
                {
                    Debug.Log("You can't hit them baka!");
                }
            }
        }
        else {
            Debug.Log("Invalid Destination");

        }
    }

    void generateMap() {
        map = new List<List<Tile>>();
        for (int i = 0; i < mapSizeX; i++) {
            List<Tile> row = new List<Tile>();
            for (int j = 0; j < mapSizeY; j++) {
                Tile tile = ((GameObject)Instantiate(tilePrefab, new Vector3(i - Mathf.Floor(mapSizeX / 2), 0, -j + Mathf.Floor(mapSizeY / 2)), Quaternion.Euler(new Vector3()))).GetComponent<Tile>();
                tile.gridPosition = new Vector2(i, j);
                row.Add(tile);
            }
            map.Add(row);
        }
    }


    void generatePlayers() {

        UserPlayer player;
        float xPos = 0;
        float yPos = 0;

        xPos = 0 - Mathf.Floor(mapSizeX / 2);
        yPos = -0 + Mathf.Floor(mapSizeY / 2);

        player = ((GameObject)Instantiate(userPlayerPrefab, new Vector3(xPos, 1.5f, yPos), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
        player.GridPosition.x = player.MoveDestination.x + Mathf.Floor(mapSizeX / 2);
        player.GridPosition.y = -(player.MoveDestination.z - Mathf.Floor(mapSizeY / 2));
        players.Add(player);
        player.PlayerName = "Bob";


        xPos += mapSizeX - 1;
        yPos -= mapSizeY - 1;
        player = ((GameObject)Instantiate(userPlayerPrefab, new Vector3((mapSizeX-1) - Mathf.Floor(mapSizeX / 2), 1.5f, -(mapSizeY - 1) + Mathf.Floor(mapSizeY / 2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
        player.GridPosition.x = player.MoveDestination.x + Mathf.Floor(mapSizeX / 2);
        player.GridPosition.y = -(player.MoveDestination.z - Mathf.Floor(mapSizeY / 2));
        players.Add(player);
        player.PlayerName = "Person";

        for (int i = 0; i < players.Count; i++) {
            players[i].PlayerIndex = i;
        }

        AIPlayer enemy;
        xPos = (mapSizeX / 2);
        yPos = (mapSizeY / 2);
        enemy = ((GameObject)Instantiate(EnemyPlayerPrefab, new Vector3(0, 1.5f, 0), Quaternion.Euler(new Vector3()))).GetComponent<AIPlayer>();
        EnemyPlayers.Add(enemy);

    }

}
