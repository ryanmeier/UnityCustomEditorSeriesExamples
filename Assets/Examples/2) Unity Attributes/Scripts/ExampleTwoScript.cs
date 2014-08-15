using UnityEngine;
using System.Collections;

public class ExampleTwoScript : MonoBehaviour {

	[HideInInspector] public float hiddenVariable = 1.0f;
	[SerializeField] private float serializedPrivateVariable = 2.0f;
	[Space(30.0f)] public float spacedFloat = 10.0f;
	[Range(3.0f, 4.0f)] public float rangeFloat = 3.5f;
	[Header("Text Attributes")]
	public string headeredString = "headeredString";
	[TextArea(3, 6)] public string textArea =
		"Here is some text\nin a\ntext area";
	[Multiline(4)] public string multilineText = "Mult\ni\nline\ntext\nscroll\nto\nthis\ntext";
	[Tooltip("This variable is an integer!")] public int tooltippedInteger = 9;

    [ContextMenuItem("Output Value", "FieldContextFunction")]
    public float ContextFloat = 0.0f;

    void FieldContextFunction()
    {
        Debug.Log(ContextFloat);
    }

    [ContextMenu("Context Function")]
    public void ContextFunction()
    {
        Debug.Log("Context Function Activated!");
    }
}
