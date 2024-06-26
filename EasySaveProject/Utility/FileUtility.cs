using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace NEG.Plugins.EasySave.Utility;

public static class FileUtility
{
	private const int compareLength = 3;

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
		'\0',
		'\\',
		'/',
		':',
	];

	public static bool TryGetFilesInDirectory(string _pathToDirectory, out string[] _filePaths)
	{
		_filePaths = [];
		if(!DirectoryUtility.DirectoryExists(_pathToDirectory, out var _directory))
		{
			return false;
		}

		try
		{
			_filePaths = Directory.GetFiles(_directory.FullName);
			return true;
		}
		catch(Exception _ex)
		{
			Debug.LogException(_ex);
			return false;
		}
	}
	public static bool IsFileNameValid(string _fileName)
	{
		return TryValidateFileName(_fileName);
	}
	private static bool TryValidateFileName(ReadOnlySpan<char> _file)
	{
		return StringExistAndValidLength(_file)
			&& IsValidChars(_file)
			&& IsValidName(_file);
	}
	private static bool StringExistAndValidLength(ReadOnlySpan<char> _file) =>
		!_file.IsEmpty && _file.Length > 0 && !_file.IsWhiteSpace();
	private static bool IsValidName(ReadOnlySpan<char> _path)
	{
		if(_path.Length < compareLength)
		{
			return true;
		}
		if(_path.Length == 3)
		{
			var _string = new string(_path).ToUpperInvariant();

			return !invalidNameSet.Contains(_string);
		}

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
	private static bool IsValidChars(ReadOnlySpan<char> _path)
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
