using System.IO;
//accesses the binary formatter in unity
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoad
{
    //save path, persistentDataPath gives a path on any computer to a data file
    private static string path = Application.persistentDataPath + "/player.u";
    
    public static void SavePlayer(TestPlayer player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
    
        FileStream stream = new FileStream(path, FileMode.Create);

        //pass the player into the playerdata constructor
        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static PlayerData LoadPLayer()
    {
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
    
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }else
        {
            Debug.LogError("Save file nto found in " + path);
            return null;
        }
    }
}
