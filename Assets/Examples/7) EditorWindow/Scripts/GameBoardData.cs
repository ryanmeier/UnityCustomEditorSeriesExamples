using System;
using System.Collections.Specialized;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class GameBoardData
{
    [SerializeField]
    private int sizeX;

    [SerializeField]
    private int sizeY;

    [XmlIgnore]
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

    [XmlIgnore]
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

    #region XML Serialization Specific Properties

    public int SerializeSizeX
    {
        get { return sizeX; }
        set { sizeX = value; }
    }

    public int SerializeSizeY
    {
        get { return sizeY; }
        set { sizeY = value; }
    }

    #endregion

    public List<GameBoardTile> tiles;

    public GameBoardData()
    {
        if (tiles == null)
        {
            tiles = new List<GameBoardTile>();
        }
    }

    public void Clear()
    {
        Fill(GameBoardTile.EMPTY);
    }

    public void Load(GameBoard board)
    {
        if (board != null && board.tiles != null)
        {
            SizeX = board.SizeX;
            SizeY = board.SizeY;

            for (int i = 0; i < board.tiles.Count; i++)
            {
                tiles[i] = board.tiles[i];
            }
        }
    }

    #region Advanced Functions

     public void Fill(GameBoardTile tileType)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i] = tileType;
        }
    }

    public void FillColumn(int x, GameBoardTile tileType)
    {
        for (int y = 0; y < SizeY; y++)
        {
            SetTileValue(x, y, tileType);
        }
    }

    public void FillRow(int y, GameBoardTile tileType)
    {
        for (int x = 0; x < SizeX; x++)
        {
            SetTileValue(x, y, tileType);
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
        int index = x * lengthY + y;
        if (index >= list.Count || index < 0)
        {
            return GameBoardTile.EMPTY;
        }
        return list[index];
    }

    public void SetTileValue(int x, int y, GameBoardTile value)
    {
        SetTileValue(tiles, x, y, value, SizeX, SizeY);
    }

    void SetTileValue(List<GameBoardTile> list, int x, int y, GameBoardTile value, int lengthX, int lengthY)
    {
        int index = x * lengthY + y;
        list[index] = value;
    }

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
    }

    #endregion
}
