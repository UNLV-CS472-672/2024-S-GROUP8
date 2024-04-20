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
    private MeshRenderer rendererReference;
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
        if (playerIndex != -1) {
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

    public Building getBuilding()
    {
        return currBuilding;
    }

    public void setBuilding(Building newBuilding)
    {
        currBuilding = newBuilding;
    }
}
