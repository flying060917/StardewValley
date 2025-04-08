using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantManager : MonoBehaviour
{
    public static PlantManager instance;
    public Tilemap tilemap;
    public Tile invesibleTile;
    public Tile hoedGroundTile;
    private void Awake()
    {
        instance=this;
    }
    void Start()
    {
        InitInteractableMap();
    }
    private void InitInteractableMap()
    {
        foreach(Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile=tilemap.GetTile(pos);
            if(tile!=null)
            {
                tilemap.SetTile(pos,invesibleTile);
            }
        }
    }
    public void HoeGround(Vector3 pos)
    {
        Vector3Int tilePos=tilemap.WorldToCell(pos);
        TileBase tile=tilemap.GetTile(tilePos);
        if(tile!=null&&tile.name==invesibleTile.name)
        {
            tilemap.SetTile(tilePos,hoedGroundTile);
        }
    }

}
