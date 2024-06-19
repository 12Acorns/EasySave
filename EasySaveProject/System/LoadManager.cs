using NEG.Plugins.EasySave.Data;
using NEG.Plugins.EasySave.Data.Path;
using NEG.Plugins.EasySave.ReturnType;
using NEG.Plugins.EasySave.Utility;
using Newtonsoft.Json;
using OneOf;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using FileIO = System.IO.File;

namespace NEG.Plugins.EasySave.SaveSystem;

/// <summary>
/// Loads objects (of type T => ISaveable) from disk and deserializes the file
/// </summary>
public sealed class LoadManager
{
	private const string JSON = ".json";

	public LoadManager(ApplicationPath _applicationPath)
	{
		ApplicationPath = _applicationPath;
	}

	public static LoadManager Instance { get; } = new(ApplicationPath.Instance);

	public ApplicationPath ApplicationPath { get; }

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="File"></typeparam>
	/// <param name="_subDirectories">The directory which the file will be retrieved from.</param>
	/// <param name="_fileName">The file which will be deserialized</param>
	/// <returns></returns>
	public OneOf<File, LoadOutputResponce> LoadFile<File>(string _subDirectories, string _fileName)
		where File : ISaveable
	{
		var _directoryInfo = ArgumentsValid(_subDirectories, _fileName);
		if(_directoryInfo == null)
		{
			return LoadOutputResponce.InvalidArguments;
		}
		return TryLoadFileImpl<File>(_directoryInfo, _fileName);
	}

	// TODO:
	// Return OneOf<DirectoryInfo, LoadOutputResponce> where a NoDirectoryFound and invalid file arg
	// return types are added. (Extra clarity)
	private DirectoryInfo? ArgumentsValid(string _subDirectories, string _fileName)
	{
		var _pathToFile = Path.Combine(ApplicationPath.GetFullPath().FullName, _subDirectories);
		if(!DirectoryUtility.DirectoryExists(_pathToFile, out DirectoryInfo _pathToFileDir))
		{
			return null;
		}
		if(!FileUtility.IsFileNameValid(_fileName))
		{
			return null;
		}
		return _pathToFileDir;
	}
	public OneOf<File, LoadOutputResponce> TryLoadFileImpl<File>
		(DirectoryInfo _pathToFile, string _fileName)
			where File : ISaveable
	{
		if(!CombinePaths(_pathToFile, _fileName, out var _fullPath))
		{
			return LoadOutputResponce.PathConcatenationFailure;
		}

		try
		{
			if(!FileIO.Exists(_fullPath))
			{
				return LoadOutputResponce.FileDoesNotExist;
			}

			using var _fileStream =
				new FileStream(
					_fullPath,
					FileMode.Open, FileAccess.Read,
					FileShare.Read, 4096, false);
			try
			{
				return LoadFile<File>(_fileStream, _fullPath);
			}
			catch(Exception _ex)
			{
				Debug.LogException(_ex);
				return LoadOutputResponce.FileIOFailure;
			}
		}
		catch(Exception _ex)
		{
			Debug.LogException(_ex);
			return LoadOutputResponce.BadStream;
		}
	}
	private OneOf<File, LoadOutputResponce> LoadFile<File>
		(FileStream _stream, string _fullPath)
			where File : ISaveable
	{
		var _stringBuilder = new StringBuilder();

		try
		{
			var _file = new FileInfo(_fullPath)!;
			var _fileSizeBytes = _file.Length;

			// No stackalloc (<= 1024 bytes) as cannot use Span<byte> with _steam.Read(...)
			var _buffer = new byte[_fileSizeBytes];

			int _numRead;
			try
			{
				while((_numRead = _stream.Read(_buffer, 0, _buffer.Length)) != 0)
				{
					var _contents = Encoding.ASCII.GetString(_buffer, 0, _numRead);
					_stringBuilder.Append(_contents);
				}
				return TryDeserializeFile<File>(_stringBuilder.ToString());
			}
			catch(Exception _ex)
			{
				Debug.LogException(_ex);
				return LoadOutputResponce.FileIOFailure;
			}
		}
		catch(Exception _ex)
		{
			Debug.LogException(_ex);
			return LoadOutputResponce.FileIOFailure;
		}
		finally
		{
			_stream.Dispose();
		}
	}
	private bool CombinePaths
		(DirectoryInfo _fullPathDir, string _fileName, out string _fullPath)
	{
		_fullPath = string.Empty;
		try
		{
			_fullPath = Path.Combine(_fullPathDir.FullName, _fileName + JSON);
		}
		catch(Exception _ex)
		{
			Debug.LogException(_ex);
			return false;
		}
		return true;
	}
	private static OneOf<File, LoadOutputResponce> TryDeserializeFile<File>(string _fileContents)
		where File : ISaveable
	{
		try
		{
			var _outputFile = JsonConvert.DeserializeObject<File>(_fileContents);
			if(_outputFile == null)
			{
				return LoadOutputResponce.BadFile;
			}
			return _outputFile;
		}
		catch(Exception _ex)
		{
			Debug.LogException(_ex);
			return LoadOutputResponce.BadFile;
		}
	}
}
