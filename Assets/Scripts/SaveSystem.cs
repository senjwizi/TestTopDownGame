using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static readonly string savePath = Application.dataPath + "/Saves/";

    public static void Init()
    {
        if(!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);
    }

    public static void Save(string saveData)
    {
        File.WriteAllText(savePath + "Save.data", saveData);
    }

    public static string Load()
    {
        if(File.Exists(savePath + "Save.data"))
            return File.ReadAllText(savePath + "Save.data");
        else
            return null;
    }
}
