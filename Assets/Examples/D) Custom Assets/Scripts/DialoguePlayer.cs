using UnityEngine;
using System.Collections;

public class DialoguePlayer : MonoBehaviour
{
    public DialogueConversation conversation = null;
    public TextMesh displayMesh = null;

    public int lineIndex = -1;

    void Start()
    {
        nextLine();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextLine();
        }
    }

    private void nextLine()
    {
        lineIndex++;
        if (conversation != null && conversation.Lines != null && conversation.Lines.Count > lineIndex)
        {
            if (displayMesh != null)
            {
                displayMesh.text = conversation.Lines[lineIndex].Speaker + ": " + conversation.Lines[lineIndex].Text;
            }
        }
    }

}
