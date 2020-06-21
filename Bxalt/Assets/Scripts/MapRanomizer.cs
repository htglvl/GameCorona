using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class MapRanomizer : MonoBehaviour
{
    public int ngang, doc;
    public bool EmptyTrueElseFalse;
    private int[,] map;
    public int[,][] weird;
    public Tilemap tilemapPavement, tilemapWall;
    public TileBase tileBasePavement, tileBaseWall;
    public building[] buildings;
    // Start is called before the first frame update
    void Awake()
    {

        StartCoroutine(LateStart(.1f));
    }
    // Update is called once per frame
    public Vector2 PickRandomSpotThatFit(int[,] map, int[,] building, int HowmanyTimeShouldTry = 100, Tilemap tilemap = null)
    {
        Vector2 pos;
        for (int i = 0; i < HowmanyTimeShouldTry; i++)
        {
            pos.x = Random.Range(map.GetLowerBound(0), map.GetUpperBound(0) - building.GetLength(0));
            pos.y = Random.Range(map.GetLowerBound(1), map.GetUpperBound(1) - building.GetLength(1));
            if (checkHaveTileOrNot(new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)), building, tilemap))
            {
                return pos;
            }
            if (i == HowmanyTimeShouldTry - 1)
            {
                if (checkHaveTileOrNot(new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)), building, tilemap))
                {
                    return pos;
                }
                else
                {
                    for (int x = 0; x < map.GetUpperBound(0) - building.GetLength(0); x++)
                    {
                        for (int y = 0; y < map.GetUpperBound(1) - building.GetLength(1); y++)
                        {
                            pos = new Vector2(x, y);
                            if (checkHaveTileOrNot(new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)), building, tilemap))
                            {
                                return pos;
                            }
                            if (x == map.GetUpperBound(0) - building.GetLength(0) - 1 && y == map.GetUpperBound(1))
                            {
                                if (checkHaveTileOrNot(new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)), building, tilemap))
                                {
                                    return pos;
                                }
                                else
                                {
                                    return new Vector2(-100, -100);
                                }
                            }
                        }
                    }
                }
            }
        }
        return new Vector2(-100, -100);
    }
    public static int[,] GenerateArray(int width, int height, bool empty)
    {
        int[,] map = new int[width, height];
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                if (empty)
                {
                    map[x, y] = 0;
                }
                else
                {
                    map[x, y] = 1;
                }
            }
        }
        return map;
    }
    public static void RenderMap(int[,] map, Tilemap tilemap, TileBase tile, Vector2 Pos = new Vector2(), bool ClearAllTile = true, bool SkipTileOrNot = false)
    {
        //Clear the map (ensures we dont overlap)
        if (ClearAllTile)
        {
            tilemap.ClearAllTiles();
        }
        //Loop through the width of the map
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            //Loop through the height of the map
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                // 1 = tile, 0 = no tile
                if (map[x, y] == 1 || SkipTileOrNot)
                {
                    tilemap.SetTile(new Vector3Int(x + Mathf.RoundToInt(Pos.x), y + Mathf.RoundToInt(Pos.y), 0), tile);
                }
            }
        }
    }
    public static void UpdateMap(int[,] map, Tilemap tilemap) //Takes in our map and tilemap, setting null tiles where needed
    {
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                //We are only going to update the map, rather than rendering again
                //This is because it uses less resources to update tiles to null
                //As opposed to re-drawing every single tile (and collision data)
                if (map[x, y] == 0)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), null);
                }
            }
        }
    }
    public static void CreatWallAround(int[,] map, Tilemap tilemap, TileBase tile, Vector2Int pos = new Vector2Int())
    {
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            tilemap.SetTile(new Vector3Int(x + pos.x, 0 + pos.y, 0), tile);
            tilemap.SetTile(new Vector3Int(x + pos.x, map.GetUpperBound(1) + pos.y, 0), tile);

        }
        for (int y = 0; y < map.GetUpperBound(1); y++)
        {
            tilemap.SetTile(new Vector3Int(0 + pos.x, y + pos.y, 0), tile);
            tilemap.SetTile(new Vector3Int(map.GetUpperBound(0) + pos.x, y + pos.y, 0), tile);
        }
        //con thieu o goc tren ben phai
        tilemap.SetTile(new Vector3Int(map.GetUpperBound(0) + pos.x, map.GetUpperBound(1) + pos.y, 0), tile);
    }

    public bool checkHaveTileOrNot(Vector2Int pos, int[,] building, Tilemap tilemap)
    {
        for (int x = 0; x < building.GetUpperBound(0); x++)
        {
            for (int y = 0; y < building.GetUpperBound(1); y++)
            {
                if (tilemap.HasTile(new Vector3Int(x + pos.x, y + pos.y, 0)))
                {
                    return true;
                }
            }
        }
        return false;
    }
    IEnumerator LateStart(float time)
    {
        yield return new WaitForSeconds(time);
        map = GenerateArray(ngang, doc, EmptyTrueElseFalse);
        RenderMap(map, tilemapPavement, tileBasePavement);
        CreatWallAround(map, tilemapWall, tileBaseWall);
        //tao tuong xung quanh
        for (int i = 0; i < buildings.Length; i++)
        {
            int[,] buildingSize = new int[buildings[i].ngang + 1, buildings[i].doc + 1];
            //do may chay tu 0, m dem tu 1 nen bi lech, phai cong them
            Vector2 pos = PickRandomSpotThatFit(map, buildingSize, 100, tilemapPavement);
            yield return new WaitForSeconds(.1f);
            RenderMap(buildingSize, tilemapPavement, buildings[i].tilebaseForBuilding, pos, false, true);
        }
        yield return new WaitForSeconds(time);
        AstarPath.active.Scan();
    }

}
