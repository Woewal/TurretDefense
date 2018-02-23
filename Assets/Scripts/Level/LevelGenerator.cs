using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.SpacePartitioning;

public class LevelGenerator : MonoBehaviour
{

    Level level;
    [SerializeField]
    private GameObject floorPrefab;

    float tileSize = 1;

    private void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        level = new Level();
        SetLevelSize(level, 27, 27);
        //AddOuterRing();
        //AddInnerRing();
        FillSpace();
        //DrawLevel();
    }

    public void SetLevelSize(Level level, int width, int height)
    {
        level.tiles = new Tile[width, height];

        for (int x = 0; x < level.tiles.GetLength(0); x++)
        {
            for (int y = 0; y < level.tiles.GetLength(1); y++)
            {
                level.tiles[x, y] = new Tile();
            }
        }
    }

    void AddOuterRing()
    {
        int outerRingWidth = 2;

        for (int x = 0; x < level.tiles.GetLength(0); x++)
        {
            for (int y = 0; y < level.tiles.GetLength(1); y++)
            {
                if (level.tiles[x, y].type != Tile.Type.Empty)
                {
                    return;
                }
                if (
                    x < outerRingWidth ||
                    y < outerRingWidth ||
                    x >= level.tiles.GetLength(0) - outerRingWidth ||
                    y >= level.tiles.GetLength(1) - outerRingWidth
                )
                {
                    level.tiles[x, y].type = Tile.Type.Floor;
                }
            }
        }
    }

    void AddInnerRing()
    {
        int middleX = (int)Mathf.Floor((float)level.tiles.GetLength(0) / 2f);
        int middleY = (int)Mathf.Floor((float)level.tiles.GetLength(1) / 2f);
        
        for(int x = middleX - 1; x < middleX + 2; x++)
        {
            for (int y = middleY - 1; y < middleY + 2; y++)
            {
                level.tiles[x, y].type = Tile.Type.Floor;
            }
        }
    }

    void FillSpace()
    {
        var leafs = BinarySpacePartitioner.GenerateLeafs(level.tiles.GetLength(0), level.tiles.GetLength(1));
        
        foreach(var leaf in leafs)
        {
            var floor = Instantiate(floorPrefab, transform);
            floor.transform.position = new Vector3(leaf.x, 0, leaf.y);
            floor.transform.localScale = new Vector3(leaf.width, floor.transform.localScale.y, leaf.height);
        }

    }

    void SetTile(Tile tile, Tile.Type tileType)
    {
        tile.type = tileType;
    }

    public void DrawLevel()
    {
        for (int x = 0; x < level.tiles.GetLength(0); x++)
        {
            for (int y = 0; y < level.tiles.GetLength(1); y++)
            {
                if(level.tiles[x,y].type == Tile.Type.Floor)
                {
                    var tile = Instantiate(floorPrefab, this.transform);
                    tile.transform.position = new Vector3(x * tileSize, 0, y * tileSize);
                }
            }
        }
    }
}
