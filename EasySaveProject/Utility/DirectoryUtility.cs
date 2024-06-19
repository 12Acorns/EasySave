using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace NEG.Plugins.EasySave.Utility;

public static class DirectoryUtility
{
	private const int compareLength = 3;
	private const string pathStartA = ":\\";
	private const string pathStartB = ":/";

	private static readonly HashSet<string> invalidNameSet =
	[
		"CON",
		"NUL",
		"PRN",
		"AUX",
		"LPT",
	];
	private static readonly HashSet<char> invalidCharsSet =
	[
		'<',
		'>',
		'.',
		'\"',
		'\'',
		'|',
		'?',
		'*',
		'\0'
	];

	public static bool TryGetSubDirectories(string _parentDirectoryFullPath, out string[] _directoryPaths)
	{
		_directoryPaths = [];
		if(!DirectoryExists(_parentDirectoryFullPath, out var _directory))
		{
			return false;
		}

		try
		{
			_directoryPaths = Directory.GetDirectories(_directory.FullName);
			return true;
		}
		catch(Exception _ex)
		{
			Debug.LogException(_ex);
			return false;
		}
	}
	public static bool DirectoryExists(ReadOnlySpan<char> _path, out DirectoryInfo _directory)
	{
		_directory = null!;

		if(!TryValidateString(_path))
		{
			return false;
		}

		var _pathString = _path.ToString();
		try
		{
			if(Directory.Exists(_pathString))
			{
				_directory = new DirectoryInfo(_pathString);
				return true;
			}
			return false;
		}
		catch(Exception _ex)
		{
			Debug.LogException(_ex);
			return false;
		}
	}
	public static bool TryCreateDirectory(ReadOnlySpan<char> _path, out DirectoryInfo _directory)
	{
		_directory = null!;

		if(!TryValidateString(_path))
		{
			return false;
		}

		var _pathString = _path.ToString();
		try
		{
			_directory = Directory.CreateDirectory(_pathString);
			return _directory.Exists;
		}
		catch(Exception _ex)
		{
			Debug.LogException(_ex);
			return false;
		}
	}
	private static bool TryValidateString(ReadOnlySpan<char> _path)
	{
		return StringExistAndValidLength(_path)
			&& IsPathStartValid(_path)
			&& CheckForInvalidChars(_path)
			&& CheckForInvalidNames(_path);
	}
	private static bool StringExistAndValidLength(ReadOnlySpan<char> _path) =>
		!_path.IsEmpty && !_path.IsWhiteSpace() && _path.Length is > 3;
	private static bool IsPathStartValid(ReadOnlySpan<char> _path)
	{
		var _slice = _path.Slice(1, 2);

		bool _pathA = _slice.SequenceEqual(pathStartA);
		bool _pathB = _slice.SequenceEqual(pathStartB);

		return _pathA || _pathB;
	}
	private static bool CheckForInvalidNames(ReadOnlySpan<char> _path)
	{
		for(int i = 0; i < _path.Length - compareLength; i++)
		{
			var _slice = _path.Slice(i, 3);

			var _string = new string(_slice).ToUpperInvariant();

			if(!invalidNameSet.Contains(_string))
			{
				continue;
			}
			return false;
		}
		return true;
	}
	private static bool CheckForInvalidChars(ReadOnlySpan<char> _path)
	{
		for(int i = 0; i < _path.Length; i++)
		{
			if(!invalidCharsSet.Contains(_path[i]))
			{
				continue;
			}
			return false;
		}
		return true;
	}
}
