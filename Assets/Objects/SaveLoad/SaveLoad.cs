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
	/**
	 * Save the given data to the given file
	 * @param rel_path The string path to the file to save to.
	 * @param data The level data to save.
	 */
	public static void saveData(string rel_path, LevelData data)
	{
		LevelData old_data = SaveLoad.loadData(rel_path);
		if (old_data == null)
		{
			old_data = data;
		}
		if (old_data.isComplete() && old_data.HighScore < data.HighScore)
		{
			old_data.HighScore = data.HighScore;
		}
		if (!old_data.isComplete() && old_data.TimeLeft > data.TimeLeft)
		{
			old_data.TimeLeft = data.TimeLeft;
		}
		FileStream stream = new FileStream(SaveLoad.absPath(rel_path), FileMode.Create);
		new BinaryFormatter().Serialize(stream, data);
		stream.Close();
	}

	/**
	* Loads the saved data from the given path.
	* @param rel_path The string path to the file to load.
	* @return LevelData The data that has been loaded, null if file not found.
	*/
	public static LevelData loadData(string rel_path)
	{
		string abs_path = SaveLoad.absPath(rel_path);
		if (File.Exists(abs_path))
		{
			return null;
		}
		FileStream stream = new FileStream(abs_path, FileMode.Open);
		LevelData data = new BinaryFormatter().Deserialize(stream) as LevelData;
		stream.Close();
		return data;
	}
	
	/**
	 * Convert the given relative path into an absolute path.
	 */
	private static string absPath(string rel_path)
	{
		return Application.persistentDataPath + rel_path;
	}
}
