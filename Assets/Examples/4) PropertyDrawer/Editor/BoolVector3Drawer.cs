using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomPropertyDrawer(typeof(BoolVector3))]
public class BoolVector3Drawer : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        base.OnGUI(position, property, label);

        SerializedProperty x = property.FindPropertyRelative("x");
        SerializedProperty y = property.FindPropertyRelative("y");
        SerializedProperty z = property.FindPropertyRelative("z");

        float propWidth = position.width / 6.0f;

        EditorGUI.LabelField(new Rect(position.x, position.y, propWidth, position.height), "X");
        x.boolValue = EditorGUI.Toggle(new Rect(position.x + propWidth * 1, position.y, propWidth, position.height), x.boolValue);

        EditorGUI.LabelField(new Rect(position.x + propWidth * 2, position.y, propWidth, position.height), "Y");
        y.boolValue = EditorGUI.Toggle(new Rect(position.x + propWidth * 3, position.y, propWidth, position.height), y.boolValue);

        EditorGUI.LabelField(new Rect(position.x + propWidth * 4, position.y, propWidth, position.height), "Z");
        z.boolValue = EditorGUI.Toggle(new Rect(position.x + propWidth * 5, position.y, propWidth, position.height), z.boolValue);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }
}
