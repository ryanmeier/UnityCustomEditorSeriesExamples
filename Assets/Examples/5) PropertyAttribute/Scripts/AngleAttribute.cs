using UnityEngine;
using System.Collections;

public class AngleAttribute : PropertyAttribute
{
    public readonly bool radians;

    public AngleAttribute(bool radians)
    {
        this.radians = radians;
    }
}
