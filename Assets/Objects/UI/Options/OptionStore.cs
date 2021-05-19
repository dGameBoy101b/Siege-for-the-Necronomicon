using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/**
 * @author Rhys Mader 33705134
 * @date 11/05/2021
 * A class for storing and serialising options.
 */
public sealed class OptionStore : MonoBehaviour
{
	/**
	 * An exception class to indicate a name clash in the options dictionary.
	 */
	public class OptionAlreadyExistsException : Exception
	{
		public OptionAlreadyExistsException() : base() {}
		public OptionAlreadyExistsException(string msg) : base(msg) {}
		public OptionAlreadyExistsException(string msg, Exception inner) : base(msg, inner) {}
	}
	
	/**
	 * An exception class to indicate a name mismatch with the options dictionary.
	 */
	public class OptionDoesNotExistException : Exception
	{
		public OptionDoesNotExistException() : base() {}
		public OptionDoesNotExistException(string msg) : base(msg) {}
		public OptionDoesNotExistException(string msg, Exception inner) : base(msg, inner) {}
	}
	
	[SerializeField()]
	[Tooltip("The string name of the file to save these options to or load from.")]
	public string FILENAME;
	
	/**
	 * A singleton instance of this option store.
	 */
	public static OptionStore Instance {get; private set;} = null;
	
	/**
	 * A dictionary of all named options.
	 */
	private Dictionary<string, object> options = new Dictionary<string, object>();
	
	/**
	 * Add a named option to this option store.
	 * @param name The string name of the new option.
	 * @param val The initial value of the new option.
	 * @throws OptionStore.OptionAlreadyExistsException An option with the given name already exists.
	 */
	public void addOption(string name, object val)
	{
		if (this.hasOption(name))
		{
			throw new OptionStore.OptionAlreadyExistsException("Option \"" + name + "\" already exists!");
		}
		this.options.Add(name, val);
	}
	
	/**
	 * Set the value of the given named option to the given value.
	 * @param name The string name of the existing option to target.
	 * @param val The new value to set the target option to.
	 * @throws OptionStore.OptionDoesNotExistException An option with the given name could not be found.
	 */
	public void setOption(string name, object val)
	{
		if (!this.hasOption(name))
		{
			throw new OptionStore.OptionDoesNotExistException("Option \"" + name + "\" does not exist!");
		}
		this.options[name] = val;
	}
	
	/**
	 * Test if this option store has an option with the given name.
	 * @param name The string name of the option to search for.
	 * @return True if this option store has an option with the given name, false otherwise.
	 */
	public bool hasOption(string name)
	{
		return this.options.ContainsKey(name);
	}
	
	/**
	 * Return the current value of an option with the given name.
	 * @param name The string name of the option to retreive.
	 * @return The value of the option with the given name.
	 * @throws OptionStore.OptionDoesNotExistException An option with the given name could not be found.
	 */
	public object getOption(string name)
	{
		if (!this.hasOption(name))
		{
			throw new OptionStore.OptionDoesNotExistException("Option \"" + name + "\" does not exist!");
		}
		return this.options[name];
	}
	
	/**
	 * Check the singleton instance to ensure there is only one.
	 */
	private void checkInstance()
	{
		if (OptionStore.Instance == null)
		{
			OptionStore.Instance = this;
			GameObject.DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			GameObject.Destroy(this.gameObject);
		}
	}
	
	/**
	 * Calculate the path to the linked option file.
	 */
	private string calcPath()
	{
		return Path.Combine(Application.dataPath, this.FILENAME);
	}
	
	/**
	 * Save the current state of these options to the linked file.
	 */
	public void save()
	{
		FileStream file = new FileStream(this.calcPath(), FileMode.Create, FileAccess.Write, FileShare.None);
		new BinaryFormatter().Serialize(file, this.options);
		file.Close();
	}
	
	/**
	 * Load the options from the linked file into these options.
	 */
	public void load()
	{
		FileStream file;
		try
		{
			file = new FileStream(this.calcPath(), FileMode.Open, FileAccess.Read, FileShare.Read);
		}
		catch (Exception e)
		{
			Debug.LogWarning(e);
			return;
		}
		this.options = new BinaryFormatter().Deserialize(file) as Dictionary<string, object>;
		file.Close();
	}
	
	private void Awake()
	{
		this.checkInstance();
		this.load();
	}
}
