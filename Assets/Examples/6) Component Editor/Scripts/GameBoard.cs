using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public enum GameBoardTile
{
    EMPTY,
    FULL,
    GOAL,
    START,
}

[Serializable]
public class TilePrefab
{
    public GameBoardTile Key;
    public GameObject Value;

    public TilePrefab()
    {}

    public TilePrefab(GameBoardTile key, GameObject value)
    {
        Key = key;
        Value = value;
    }
}

public class GameBoard : MonoBehaviour
{

    public GameObject playerObject = null;


    [SerializeField]
    [HideInInspector]
    private int sizeX;

    [SerializeField]
    [HideInInspector]
    private int sizeY;

    public int SizeX
    {
        get { return sizeX; }
        set
        {
            int oldSizeX = sizeX;
            sizeX = value;
            if (oldSizeX != sizeX) ResizeTilesArray(oldSizeX, SizeY);
        }
    }

    public int SizeY
    {
        get { return sizeY; }
        set
        {
            int oldSizeY = sizeY;
            sizeY = value;
            if (oldSizeY != sizeY) ResizeTilesArray(SizeX, oldSizeY);
        }
    }

    public Vector2 tileSize = Vector2.zero;

    private Vector2 boardSize
    {
        get
        {
            return new Vector2(SizeX * tileSize.x, sizeY * tileSize.y);
        }
    }

    //public GameBoardTile[,] tiles;
    public List<GameBoardTile> tiles; // index (x, y) = x*SizeY + y
    public List<TilePrefab> tilePrefabs;

    private List<GameObject> boardTileObjects;

    private int startX = -1;
    private int startY = -1;

    private int playerX = -1;
    private int playerY = -1;

    void Start()
    {
        GenerateBoard();
    }

    void Update()
    {
        HandleInput();
    }

    #region Board Generation/Utility

    [ExecuteInEditMode]
    public void ResizeTilesArray(int oldX, int oldY)
    {
        if (tiles != null && oldX == SizeX && oldY == SizeY)
        {
            return;
        }

        List<GameBoardTile> oldTiles = tiles;

        tiles = new List<GameBoardTile>();

        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                GameBoardTile tileValue = GameBoardTile.EMPTY;
                if (oldTiles != null && x < oldX && y < oldY)
                {
                    tileValue = GetTileValue(oldTiles, x, y, oldX, oldY);
                }
                tiles.Add(tileValue);
            }
        }

        GenerateBoard();
    }

    [ExecuteInEditMode]
    public void GenerateBoard()
    {
        #region Clean Up Old Board

        if (boardTileObjects == null)
        {
            boardTileObjects = new List<GameObject>();
        }

        while (boardTileObjects != null && boardTileObjects.Count > 0)
        {
            GameObject firstObj = boardTileObjects[0];
            boardTileObjects.RemoveAt(0);
            DestroyImmediate(firstObj);
        }

        List<GameObject> deleteObjects = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child != transform)
            {
                deleteObjects.Add(child.gameObject);
            }
        }

        foreach (GameObject obj in deleteObjects)
        {
            DestroyImmediate(obj);
        }

        #endregion

        Dictionary<GameBoardTile, GameObject> prefabs = new Dictionary<GameBoardTile, GameObject>();
        if (tilePrefabs == null)
        {
            tilePrefabs = new List<TilePrefab>();
        }
        foreach (TilePrefab pair in tilePrefabs)
        {
            prefabs[pair.Key] = pair.Value;
        }

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                Vector3 newPosition = getLocalPosition(x, y);

                GameBoardTile currentTile = GetTileValue(x, y);
                if (currentTile == GameBoardTile.START)
                {
                    startX = x;
                    startY = y;
                }

                if (prefabs[currentTile] != null)
                {
                    GameObject newTile = Instantiate(prefabs[currentTile]) as GameObject;
                    newTile.transform.parent = transform;
                    newTile.transform.localPosition = newPosition;
                    boardTileObjects.Add(newTile);
                }
            }
        }

        ResetPlayer();
    }

    public Vector3 getLocalPosition(int x, int y)
    {
        Vector3 pos = new Vector3();
        pos.x = -boardSize.x / 2 + tileSize.x * x;
        pos.y = boardSize.y / 2 - tileSize.y * y;

        return pos;
    }

    #endregion

    #region Gameplay

    void ResetPlayer()
    {
        if (playerObject != null && startX != -1 && startY != -1)
        {
            MovePlayerTo(startX, startY);
        }
    }

    void MovePlayerTo(int x, int y)
    {
        playerX = x;
        playerY = y;
        playerObject.transform.position = getLocalPosition(x, y) + transform.position + new Vector3(0.0f, 0.0f, -1.0f);
    }

    void HandleInput()
    {
        int nextTileX = playerX;
        int nextTileY = playerY;

        if (Input.GetKeyDown(KeyCode.W))
        {
            nextTileY--;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            nextTileY++;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            nextTileX--;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            nextTileX++;
        }

        ProcessMove(nextTileX, nextTileY);
    }

    void ProcessMove(int x, int y)
    {
        if (x < 0 || x >= SizeX ||
            y < 0 || y >= SizeY)
        {
            return;
        }

        GameBoardTile nextTile = GetTileValue(x,y);
        if (nextTile != GameBoardTile.FULL)
        {
            MovePlayerTo(x, y);
        }

        if (nextTile == GameBoardTile.GOAL)
        {
            Debug.Log("You Win!");
        }
    }

    #endregion

    #region Utility

    public GameBoardTile GetTileValue(int x, int y)
    {
        return GetTileValue(tiles, x, y, SizeX, SizeY);
    }

    GameBoardTile GetTileValue(List<GameBoardTile> list, int x, int y, int lengthX, int lengthY)
    {
        int index = x*lengthY + y;
        if (index >= list.Count || index < 0)
        {
            Debug.Log((index < list.Count ? "" : "index " + index + " is out of bounds"));
            return GameBoardTile.EMPTY;
        }
        return list[index];
    }

    public void SetTileValue(int x, int y, GameBoardTile value)
    {
        SetTileValue(tiles, x, y, value, SizeX, SizeY);
        GenerateBoard();
    }

    void SetTileValue(List<GameBoardTile> list, int x, int y,  GameBoardTile value, int lengthX, int lengthY)
    {
        int index = x*lengthY + y;
        list[index] = value;
    }

    public void Load(GameBoardData data)
    {
        if (data != null && data.tiles != null)
        {
            SizeX = data.SizeX;
            SizeY = data.SizeY;

            for (int i = 0; i < data.tiles.Count; i++)
            {
                tiles[i] = data.tiles[i];
            }
        }
    }

    #endregion

}
