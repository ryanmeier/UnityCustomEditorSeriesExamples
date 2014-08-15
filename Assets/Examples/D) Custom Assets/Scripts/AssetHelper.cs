using System;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

public static class AssetHelper
{
    public static bool LoadAssetByType(Type type, string folderPath, string fileName, out Object obj)
    {
        obj = null;
#if UNITY_EDITOR
        FileHelper.CreateDirectoryIfDoesntExist(folderPath + "/" + fileName);

        string[] result = AssetDatabase.FindAssets(fileName + " t:" + type, new[] {folderPath});
        if (result.Length > 0)
        {
            obj = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(result[0]), type);
            return true;
        }
#else
        obj = Resources.LoadAssetAtPath(folderPath + fileName, type);
        if (obj != null)
        {
            return true;
        }
#endif
        return false;
    }

    public static T NewAsset<T>(string folderPath, string fileName, string extension, Action<T> initializer = null)
        where T : ScriptableObject
    {
        T data = ScriptableObject.CreateInstance<T>();

        if (initializer != null && data != null)
        {
            initializer(data);
        }

        CreateAsset(data, folderPath, fileName, extension);

        return data;
    }

    public static void CreateAsset(Object obj, string folderPath, string fileName, string extension)
    {
#if UNITY_EDITOR
        FileHelper.CreateDirectoryIfDoesntExist(folderPath + "/" + fileName + extension);
        string fullAssetPath = AssetDatabase.GenerateUniqueAssetPath(folderPath + "/" + fileName + extension);
        AssetDatabase.CreateAsset(obj, fullAssetPath);
#endif
    }

}