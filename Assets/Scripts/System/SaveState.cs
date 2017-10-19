using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveState {

    public static SaveState Instance;

    public List<BuildingData> AvailableBuildings;

    public static void Initiate()
    {
        if (!Load())
        {
            Instance = new SaveState();
        }
    }

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save.gd");
        bf.Serialize(file, SaveState.Instance);
        file.Close();

        bf = new BinaryFormatter();
        file = File.Open(Application.persistentDataPath + "/save.gd", FileMode.Open);
        SaveState.Instance = (SaveState)bf.Deserialize(file);
        file.Close();
    }

    public static bool Load()
    {
        if (File.Exists(Application.persistentDataPath + "/save.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save.gd", FileMode.Open);
            SaveState.Instance = (SaveState)bf.Deserialize(file);
            file.Close();
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void Delete()
    {
        File.Delete(Application.persistentDataPath + "/save.gd");
    }
}
