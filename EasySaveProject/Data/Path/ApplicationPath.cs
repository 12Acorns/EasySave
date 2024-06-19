using NEG.Plugins.EasySave.SaveSystem;
using System;
using System.IO;
using UnityEngine;

namespace NEG.Plugins.EasySave.Data.Path;

/// <summary>
/// A wrapper class over <see cref="PathData"/>
/// <para></para>
/// Used by a <see cref="SaveManager"/> instance
/// </summary>
public sealed class ApplicationPath
{
	/// <param name="_path">The path to the save location directory, 
	/// IE
	/// <para></para>
	/// C:\\PathToUser\\USER\\Saves
	/// </param>
	/// <param name="_saveDirectoryName">Name for files to be created in under root</param>
	/// <exception cref="InvalidDataException"></exception>
	public ApplicationPath(string _path, string _fileName)
		: this(new PathData(_path, _fileName)) { }
	public ApplicationPath(PathData _path)
	{
		path = _path;
	}

	// DSaves => Default Saves-Denotes to the default directory to create saves in
	public static ApplicationPath Instance { get; } =
		new(Application.persistentDataPath, "DSaves");

	private readonly PathData path;

	public DirectoryInfo GetFullPath()
	{
		return path.FullPath;
	}
	public ReadOnlySpan<char> GetSaveDirectoryNameSpan()
	{
		return path.SaveDirectory;
	}
	public string GetSaveDirectoryName()
	{
		return path.SaveDirectory;
	}
}
