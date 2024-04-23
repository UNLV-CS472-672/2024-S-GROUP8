using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Tile : MonoBehaviour
{
    // Backing fields
    public int playerIndex;

    // Public properties
    public class TileReference
    {
        public Vector2 tilePosition = new Vector2();
        public string tileName = "";
        public Building currBuilding = new Building("Nothing", 0, 0);
    }

    // References to the renderer and materials for the tile
    private MeshRenderer rendererReference;
    private TileReference reference = new TileReference();

    // Initialization in Start method
    // Assumes that the tile materials are located within a Resources folder
    void Start()
    {
        rendererReference = GetComponent<MeshRenderer>();
        if (playerIndex > -1)
            SetMaterial(PlayerController.players[playerIndex].GetColor());
        if (gameObject != null) { 
            reference.tilePosition.x = (int)gameObject.transform.position.x;
            reference.tilePosition.y = (int)gameObject.transform.position.z;
            // Temp name system
            reference.tileName = " " + reference.tilePosition.x + " " + reference.tilePosition.y;
        }
        SetPlayer(playerIndex);
    }

    public int GetPlayer() 
    {
        return playerIndex;
    }

    public void SetPlayer(int index)
    {
        playerIndex = index;
        // -1 represents non-ownership
        if (index > -1) {
            PlayerController.players[index].AddTiles(ref reference);
        }
    }

    /* Fucntion to check if a tile located at a certain postion is in the players tilesOwned list */
    public static TileReference GetTileAtPostion(Vector2 position, List<Tile.TileReference> tilesOwned){
        foreach (TileReference tileRef in tilesOwned){
            if (tileRef.tilePosition == position){
                return tileRef;
            }
        }
        return default(TileReference);
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

     /* Fucntion to check if a tile located at a certain postion is in the players tilesOwned list */
    public static TileReference GetTileAtPostion(Vector2 position, List<Tile.TileReference> tilesOwned){
        foreach (TileReference tileRef in tilesOwned){
            if (tileRef.tilePosition == position){
                return tileRef;
            }
        }
        return default(TileReference);
    }

    public Building GetBuilding()
    {
        return reference.currBuilding;
    }

    public void SetBuilding(Building newBuilding)
    {
        if (newBuilding == null){
            reference.currBuilding = new Building("Nothing", 0, 0);
        }
        else {
            reference.currBuilding = newBuilding;
        }
        
    }
}
