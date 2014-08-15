using UnityEngine;
using System.Collections;

[System.Serializable]
public class BoolVector3
{
    public bool x;
    public bool y;
    public bool z;
       
    public BoolVector3()
    {
        x = false;
        y = false;
        z = false;
    }

    public BoolVector3(bool gX, bool gY, bool gZ)
    {
        x = gX;
        y = gY;
        z = gZ;
    }
}

public class BoolVectorTestComponent : MonoBehaviour
{
    public BoolVector3 vec;
}
