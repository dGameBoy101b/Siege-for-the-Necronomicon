using System.IO;
//accesses the binary formatter in unity
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/**
* @author Connor Burnside 33394927
* @date 13/04/2021
* this class opens a binary formatter and filestream to save or load the players data 
*/

public static class SaveLoad
{
    //save path, persistentDataPath gives a path on any computer to a data file
    private static string path = Application.persistentDataPath + "/player.u";
    
    /**
    *@param player this is the class that is going to be saved
    * this fucntion saves the playerdata to a consistent filepath
    */
    //change param to class that has the players data to save
    public static void SaveData(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
    
        FileStream stream = new FileStream(path, FileMode.Create);

        //pass the player into the playerdata constructor
        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("file saved");
    }

    /**
    *@return PlayerData the data that has been loaded
    * this function loads the saved data from the consistent filepath
    */
    public static PlayerData LoadData()
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
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
