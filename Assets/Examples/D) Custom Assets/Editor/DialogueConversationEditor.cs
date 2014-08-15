using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(DialogueConversation))]
public class DialogueConversationEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        DialogueConversation conversation = (DialogueConversation)target;
        if (conversation.Lines == null)
        {
            conversation.Lines = new List<DialogueElement>();
        }

        int deleteIndex = -1;
        for (int i = 0; i < conversation.Lines.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("X", GUILayout.Width(30f)))
            {
                deleteIndex = i;
            }

            //EditorGUILayout.LabelField();
            conversation.Lines[i].Speaker = EditorGUILayout.TextField(i + ") Speaker:", conversation.Lines[i].Speaker);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Text:");
            conversation.Lines[i].Text = EditorGUILayout.TextField("", conversation.Lines[i].Text);

            EditorGUILayout.Space();
        }

        if (deleteIndex != -1)
        {
            conversation.Lines.RemoveAt(deleteIndex);
        }

        if (GUILayout.Button("Add"))
        {
            conversation.Lines.Add(new DialogueElement());
        }

    }
}
