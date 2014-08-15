using UnityEngine;
using UnityEditor;
using System.Collections;

public class TexturePostProcessor : AssetPostprocessor
{

    void OnPostprocessTexture(Texture2D texture)
    {
        if (assetPath.Contains("TintRed"))
        {
            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    Color pixel = texture.GetPixel(x, y);
                    texture.SetPixel(x, y, pixel * Color.red);
                }
            }
        }
    }
}
