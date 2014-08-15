using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(GameBoardHandles))]
public class GameBoardHandlesEditor : GameBoardEditor {

    public override void OnInspectorGUI()
    {
        //Swap selectedTile with hotkeys
        if (Event.current.type == EventType.KeyUp)
        {
            switch (Event.current.keyCode)
            {
                case KeyCode.O:
                    selectedTile = GameBoardTile.EMPTY;
                    break;
                case KeyCode.P:
                    selectedTile = GameBoardTile.FULL;
                    break;
                case KeyCode.LeftBracket:
                    selectedTile = GameBoardTile.GOAL;
                    break;
                case KeyCode.RightBracket:
                    selectedTile = GameBoardTile.START;
                    break;
            }
            this.Repaint();
        }

        base.OnInspectorGUI();
    }

    void OnSceneGUI()
    {

        GameBoard targetBoard = (GameBoard) target;
        float buttonSize = Mathf.Min(targetBoard.tileSize.x, targetBoard.tileSize.y)/2;

        Color handlesColor = Handles.color;

        Handles.color = new Color(0.3f, 0.3f, 0.3f, 0.5f);

        for (int x = 0; x < targetBoard.SizeX; x++)
        {
            for (int y = 0; y < targetBoard.SizeY; y++)
            {
                Vector3 position = targetBoard.transform.position + targetBoard.getLocalPosition(x, y);
                if(Handles.Button(position, Quaternion.identity, buttonSize, buttonSize,
                    Handles.SelectionFrame))
                {
                    targetBoard.SetTileValue(x, y, selectedTile);
                }
            }
        }

        Handles.color = handlesColor;
    }
}
