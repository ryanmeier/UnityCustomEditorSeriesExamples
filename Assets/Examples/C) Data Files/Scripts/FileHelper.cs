using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections;

public static class FileHelper
{

    #region Validation

    public static bool fileExists(string filepath)
    {
        return File.Exists(filepath);
    }

    public static void CreateDirectoryIfDoesntExist(string filePath)
    {
        FileInfo file = new FileInfo(filePath);
        file.Directory.Create();
    }

    #endregion

    public static void writeObjectFile<T>(string filePath, T obj, Action<FileStream, T> serializationMethod)
    {
        CreateDirectoryIfDoesntExist(filePath);

        FileStream stream = new FileStream(filePath, FileMode.Create);

        serializationMethod(stream, obj);

        stream.Close();
    }

    public static T readObjectFile<T>(string filename, Func<FileStream, T> deserializationMethod)
    {
        if (!fileExists(filename))
        {
            Debug.Log("ERROR: Can't load " + filename + " - no file exists");
        }

        FileStream stream = new FileStream(filename, FileMode.Open);

        T data = deserializationMethod(stream);

        stream.Close();

        return data;
    }


    #region Serialization/Deserialization Methods

    public static void SerializeXML<T>(FileStream stream, T obj)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        serializer.Serialize(stream, obj);
    }

    public static T DeserializeXML<T>(FileStream stream)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        return (T)serializer.Deserialize(stream);
    }

    #endregion


}
