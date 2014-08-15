using UnityEditor;
using UnityEngine;
using System.Collections;

public class SFXPostProcessor : AssetPostprocessor {

    void OnPreprocessAudio()
    {
        AudioImporter audioImporter = (AudioImporter)assetImporter;
        if (audioImporter != null)
        {
            if (assetPath.Contains("Music"))
            {
                audioImporter.threeD = false;
            }
            if (assetPath.Contains("SFX"))
            {
                audioImporter.loadType = AudioImporterLoadType.DecompressOnLoad;
            }
        }
    }

}
