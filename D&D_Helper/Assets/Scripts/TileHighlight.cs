using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePath {
    public List<Tile> ListOfTiles = new List<Tile>();
    public int CostOfPath = 0;

    public Tile LastTile;

    public TilePath() {}

    public TilePath(TilePath Path) {
        ListOfTiles = Path.ListOfTiles;
        CostOfPath = Path.CostOfPath;
        LastTile = Path.LastTile;
    }

    public Tile GetLastTile() {
        if (ListOfTiles.Count > 0) {
            return ListOfTiles[ListOfTiles.Count - 1];
        }
        return null;
    }

    public void AddTile(Tile t) {
        CostOfPath += t.MovementCost;
        ListOfTiles.Add(t);
        LastTile = t;
    }


}


public class TileHighlight{

    public TileHighlight() {

    }

    public static List<Tile> FindHighlight(Tile OriginTile, int MovementPoints) {
        List<Tile> Closed = new List<Tile>();
        List<TilePath> Open = new List<TilePath>();

        TilePath OriginPath = new TilePath();
        OriginPath.AddTile(OriginTile);

        Open.Add(OriginPath);


        while (Open.Count > 0) {
            TilePath Current = Open[0];
            Open.Remove(Open[0]);

            if (Closed.Contains(Current.LastTile)) {
                continue;
            }
            if (Current.CostOfPath > MovementPoints + 1) {
                continue;
            }
            Closed.Add(Current.LastTile);

            foreach (Tile t in Current.LastTile.Neighbours) {
                TilePath NewTilePath = new TilePath(Current);
                NewTilePath.AddTile(t);
                Open.Add(NewTilePath);
            }

        }
        Closed.Remove(OriginTile);
        return Closed;
    }
}
