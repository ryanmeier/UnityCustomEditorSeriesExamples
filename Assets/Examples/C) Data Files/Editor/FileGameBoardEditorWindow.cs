using System;
using UnityEditor;
using UnityEngine;
using System.Collections;
using FileUtil = UnityEditor.FileUtil;

public class FileGameBoardEditorWindow : EditorWindow {

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

    private GameBoardData currentData = new GameBoardData();
    private GameBoardTile selectedTile;
    private string fileName = "";

    private static string filePrefix = "boards/";

    [MenuItem("Window/FileGameBoard Editor %g")]
    static void CreateWindow()
    {
        // Get existing open window or if none, make a new one:
        FileGameBoardEditorWindow window =
            (FileGameBoardEditorWindow)EditorWindow.GetWindow(typeof(FileGameBoardEditorWindow));
    }

    void OnGUI()
    {
        GameBoard selectedObjectBoard = null;
        if (Selection.activeGameObject != null)
        {
            selectedObjectBoard = Selection.activeGameObject.GetComponent<GameBoard>();
        }

        EditorGUILayout.BeginHorizontal(); //BEGIN Whole Window

        //Sidebar
        EditorGUILayout.BeginVertical(GUILayout.Width(200)); // BEGIN Sidebar

        EditorGUILayout.BeginHorizontal(); // BEGIN Board Size

        currentData.SizeX = EditorGUILayout.IntField("Size X", currentData.SizeX);
        currentData.SizeY = EditorGUILayout.IntField("Size Y", currentData.SizeY);

        EditorGUILayout.EndHorizontal(); // END Board Size

        EditorGUILayout.Space();

        if (GUILayout.Button("Fill"))
        {
            currentData.Fill(selectedTile);
        }

        if (GUILayout.Button("Clear"))
        {
            currentData.Clear();
        }

        bool guiEnabled = GUI.enabled;
        GUI.enabled = selectedObjectBoard != null;

        if (GUILayout.Button("Save to Object"))
        {
            selectedObjectBoard.Load(currentData);
        }
        if (GUILayout.Button("Load From Object"))
        {
            currentData.Load(selectedObjectBoard);
        }

        GUI.enabled = guiEnabled;

        GUILayout.Space(30f);
        fileName = EditorGUILayout.TextField("File:", fileName);

        if (GUILayout.Button("Save to File"))
        {
            FileHelper.writeObjectFile(filePrefix + fileName, currentData, FileHelper.SerializeXML);
        }

        GUI.enabled = FileHelper.fileExists(filePrefix + fileName);

        if (GUILayout.Button("Load from File"))
        {
            currentData = FileHelper.readObjectFile<GameBoardData>(filePrefix + fileName, FileHelper.DeserializeXML<GameBoardData>);
        }

        GUI.enabled = guiEnabled;

        EditorGUILayout.EndVertical(); // END Sidebar

        //Board Editor
        EditorGUILayout.BeginVertical(); // BEGIN Board Editor

        EditorGUILayout.BeginHorizontal(); // BEGIN selectedTile Menu
        foreach (GameBoardTile tile in Enum.GetValues(typeof(GameBoardTile)))
        {
            if (GUILayout.Button(tile.ToString(), (tile == selectedTile ? selectedButton : GUI.skin.button)))
            {
                selectedTile = tile;
            }
        }
        EditorGUILayout.EndHorizontal(); // END selectedTile Menu

        EditorGUILayout.BeginHorizontal(); // BEGIN Board Layout

        #region V2 Code
        /*
        EditorGUILayout.BeginVertical(); // BEGIN Row Fill Column
        GUILayout.Space(20);
        for (int y = 0; y < currentData.SizeY; y++)
        {
            if (GUILayout.Button(">", GUILayout.Width(20), GUILayout.Height(50)))
            {
                currentData.FillRow(y, selectedTile);
            }
        }
        EditorGUILayout.EndVertical(); // END Row Fill Column
         */
        #endregion

        for (int x = 0; x < currentData.SizeX; x++)
        {

            EditorGUILayout.BeginVertical(); // BEGIN Sub-Board Layout

            #region V2 Code
            /*
            if (GUILayout.Button("v", GUILayout.Width(50), GUILayout.Height(20)))
            {
                currentData.FillColumn(x, selectedTile);
            }
            */
            #endregion

            for (int y = 0; y < currentData.SizeY; y++)
            {
                if (GUILayout.Button(currentData.GetTileValue(x, y).ToString(), GUILayout.Width(50), GUILayout.Height(50)))
                {
                    currentData.SetTileValue(x, y, selectedTile);
                }
            }
            EditorGUILayout.EndVertical(); // END Sub-Board Layout
        }
        EditorGUILayout.EndHorizontal(); // END Board Layout

        EditorGUILayout.EndVertical(); //END Board Editor

        EditorGUILayout.EndHorizontal(); //END Whole Window
    }



}
