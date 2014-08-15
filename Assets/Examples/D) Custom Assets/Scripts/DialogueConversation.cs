using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class DialogueElement
{
    public string Speaker = "";
    public string Text = "";
}

public class DialogueConversation : ScriptableObject
{
    public List<DialogueElement> Lines;

    [MenuItem("Dialogue/New Dialogue")]
    public static void NewDialogue()
    {
        AssetHelper.NewAsset<DialogueConversation>("Assets/Dialogue", "NewDialogue", ".asset", (o => o.Lines = new List<DialogueElement>()));
    }
}
