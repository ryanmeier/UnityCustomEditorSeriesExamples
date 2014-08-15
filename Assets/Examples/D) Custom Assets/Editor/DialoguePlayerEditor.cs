using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(DialoguePlayer))]
public class DialoguePlayerEditor : Editor
{

    private int selectedConversationIndex = 0;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DialoguePlayer player = (DialoguePlayer)target;

        EditorGUILayout.Space();

        
        string[] options = AssetDatabase.FindAssets("t:DialogueConversation").Select(o => AssetDatabase.GUIDToAssetPath(o)).ToArray();

        selectedConversationIndex = EditorGUILayout.Popup("Conversation: ", selectedConversationIndex, options);

        if (GUILayout.Button("Load"))
        {
            player.conversation = (DialogueConversation)AssetDatabase.LoadAssetAtPath(options[selectedConversationIndex],
                typeof(DialogueConversation));
        }
         

    }
}
