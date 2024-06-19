namespace NEG.Plugins.EasySave.ReturnType;

public enum LoadOutputResponce
{
	/// <summary>
	/// Indicates file passed could not be serialized, deserialization error
	/// </summary>
	BadFile,
	FileIOFailure,
	InvalidArguments,
	/// <summary>
	/// Could not combine sub-directory/ies with file name
	/// </summary>
	PathConcatenationFailure,
	BadStream,
	FileDoesNotExist,
}
