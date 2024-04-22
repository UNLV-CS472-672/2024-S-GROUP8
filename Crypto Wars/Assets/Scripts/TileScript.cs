using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Tile : MonoBehaviour
{
    // Backing fields
    public int playerIndex;

    // Public properties
    public struct TileReference
    {
        public Vector2 tilePosition;
        public string tileName;
    }

    // References to the renderer and materials for the tile
    public MeshRenderer rendererReference;
    private Building currBuilding;
    private TileReference reference;

    // Initialization in Start method
    // Assumes that the tile materials are located within a Resources folder
    void Start()
    {
        rendererReference = GetComponent<MeshRenderer>();
        SetMaterial(PlayerController.players[playerIndex].GetColor());
        currBuilding = new Building("Nothing",0,0);
        if (gameObject != null) {
            reference.tilePosition.x = (int)gameObject.transform.position.x;
            reference.tilePosition.y = (int)gameObject.transform.position.z;
            // Temp name system
            reference.tileName = " " + reference.tilePosition.x + " " + reference.tilePosition.y;
        }
        SetPlayer(GetPlayer());
    }

    public int GetPlayer() 
    {
        return playerIndex;
    }

    public void SetPlayer(int index)
    {
        // -1 represents non-ownership
        if (index > -1) {
            PlayerController.players[index].AddTiles(reference);
        }
    }
    
    public void SetMaterial(Material newMaterial)
    {
        rendererReference.material = newMaterial;
    }

    public Material GetMaterial()
    {
        return rendererReference.material;
    }

    public MeshRenderer GetRender()
    {
        return rendererReference;
    }

    public void SetRender(MeshRenderer renderer)
    {
        rendererReference = renderer;
    }

    public void SetTilePosition(int x, int y)
    {
        if (reference.Equals(null)) {
            reference = new TileReference();
        }
        reference.tilePosition.x = x;
        reference.tilePosition.y = y;
    }

    public Vector2 GetTilePosition()
    {
        return reference.tilePosition;
    }

    public Building GetBuilding()
    {
        return currBuilding;
    }

    public void SetBuilding(Building newBuilding)
    {
        currBuilding = newBuilding;
    }

    public static bool IsAdjacent(Player player, Tile friendlyTile) {
        List<TileReference> tiles = player.GetTiles();
        foreach (TileReference enemyTile in tiles) {
            int X1 = (int)enemyTile.tilePosition.x;
            int Y1 = (int)enemyTile.tilePosition.y;

            int X2 = (int)friendlyTile.reference.tilePosition.x;
            int Y2 = (int)friendlyTile.reference.tilePosition.y;

            if (X2 == X1 && Y2 == Y1) // (0,0) Self
                return false; 

            if (X2 + 1 == X1 || X2 - 1 == X1 || X2 == X1) {
                if (Y2 == Y1) // +(1, 0) || +(-1, 0)
                    return true;
                if (Y2 + 1 == Y1) // +(1, 1) || +(-1, 1) || +(0, 1) 
                    return true;
                if (Y2 - 1 == Y1) // +(1, -1) || +(-1, -1) || +(0, -1) 
                    return true;
            }

        }
        Debug.Log("AAA");
        return false;
    }



}
