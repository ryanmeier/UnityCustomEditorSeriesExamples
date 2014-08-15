using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(GameBoard))]
public class GameBoardEditor : Editor
{

    private static GUIStyle _selectedButton = null;

    private static GUIStyle selectedButton
    {
        get
        {
            if (_selectedButton == null)
            {
                _selectedButton = new GUIStyle(GUI.skin.button);
                _selectedButton.normal.background = _selectedButton.active.background;
                _selectedButton.normal.textColor = _selectedButton.active.textColor;
            }
            return _selectedButton;
        }
    }

    protected GameBoardTile selectedTile = GameBoardTile.EMPTY;

    public override void OnInspectorGUI()
    {

        GameBoard boardTarget = (GameBoard) target;

        #region Target Initialization

        if (boardTarget.tiles == null)
        {
            boardTarget.ResizeTilesArray(0, 0);
        }

        if (boardTarget.tilePrefabs == null)
        {
            boardTarget.tilePrefabs = new List<TilePrefab>();
        }

        List<GameBoardTile> addTiles = new List<GameBoardTile>();
        foreach (GameBoardTile tile in Enum.GetValues(typeof (GameBoardTile)))
        {
            bool addTile = !boardTarget.tilePrefabs.Exists(o => o.Key == tile);
            if (addTile)
            {
                boardTarget.tilePrefabs.Add(new TilePrefab(tile, null));
            }
        }

        #endregion

        boardTarget.playerObject = EditorGUILayout.ObjectField("Player", boardTarget.playerObject, typeof(GameObject), true) as GameObject;

        #region Board Parameters

        EditorGUILayout.BeginHorizontal();

        boardTarget.SizeX = EditorGUILayout.IntField("Size X", boardTarget.SizeX);
        boardTarget.SizeY = EditorGUILayout.IntField("Size Y", boardTarget.SizeY);

        EditorGUILayout.EndHorizontal();

        boardTarget.tileSize = EditorGUILayout.Vector2Field("Tile Size", boardTarget.tileSize);

        //Prefab List
        foreach (TilePrefab pair in boardTarget.tilePrefabs)
        {
            pair.Value = EditorGUILayout.ObjectField(pair.Key.ToString(), pair.Value, typeof (GameObject)) as GameObject;
        }

        #endregion


        #region Board

        EditorGUILayout.BeginHorizontal();
        foreach (GameBoardTile tile in Enum.GetValues(typeof (GameBoardTile)))
        {
            if (GUILayout.Button(tile.ToString(), (tile == selectedTile ? selectedButton : GUI.skin.button)))
            {
                selectedTile = tile;
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        for(int x = 0; x < boardTarget.SizeX; x++)
        {
            EditorGUILayout.BeginVertical();
            for (int y = 0; y < boardTarget.SizeY; y++)
            {
                if (GUILayout.Button(boardTarget.GetTileValue(x, y).ToString(), GUILayout.Width(50), GUILayout.Height(50)))
                {
                    boardTarget.SetTileValue(x, y, selectedTile);
                }
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();

        #endregion

        if (GUILayout.Button("Generate"))
        {
            boardTarget.GenerateBoard();
        }

    }
}
