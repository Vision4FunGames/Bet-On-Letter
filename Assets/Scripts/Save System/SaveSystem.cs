using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save(GameManager gameManager)
    {
        PlayerPrefs.SetInt("loadLevel", gameManager.level);
        PlayerPrefs.SetInt("isAllLevelFinished", gameManager.allLevelsFinished == false ? 0 : 1);
        PlayerPrefs.SetInt("alllevelsControl", gameManager.alllevelsControl);

    }

    public static PlayerData Load()
    {
        return null;
        /*
        string path = Application.persistentDataPath + "/player.eomt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data= formatter.Deserialize(stream) as PlayerData;

            stream.Close();
            return data;
        } 
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
        */
    }

}
