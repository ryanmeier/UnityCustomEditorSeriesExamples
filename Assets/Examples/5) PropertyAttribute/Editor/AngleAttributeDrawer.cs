using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomPropertyDrawer(typeof(AngleAttribute))]
public class AngleAttributeDrawer : PropertyDrawer
{

    private AngleAttribute _attributeValue = null;
    private AngleAttribute attributeValue
    {
        get
        {
            if (_attributeValue == null)
            {
                _attributeValue = (AngleAttribute) attribute;
            }
            return _attributeValue;
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty angle = property;

        EditorGUI.PropertyField(position, angle);

        clampAngle(angle);
    }

    private void clampAngle(SerializedProperty angle)
    {
        if (angle == null)
        {
            return;
        }
        float fullRotation = (attributeValue.radians ? Mathf.PI * 2 : 360.0f);
        while (angle.floatValue < 0.0f)
        {
            angle.floatValue += fullRotation;
        }

        while (angle.floatValue > fullRotation)
        {
            angle.floatValue -= fullRotation;
        }
    }
}
