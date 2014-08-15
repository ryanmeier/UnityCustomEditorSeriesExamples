using UnityEngine;
using System.Collections;

public class AttributeExampleObject : MonoBehaviour
{

    [Angle(true)] public float AngleRadians = 0.0f;
    [Angle(false)] public float AngleDegrees = 0.0f;

    [Incrementable] public int IncrementableInt = 0;
    [Incrementable] public float IncrementableFloat = 0.0f;
    [Incrementable(3.0f)] public int IncrementableIntByThree = 0;
    [Incrementable(4.5f)] public float IncrementableFloatByFourPointFive = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
