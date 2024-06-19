namespace NEG.Plugins.EasySave.ReturnType;

public enum SaveOutputResponce
{
	Success,
	FileIOFailure,
	PathConcatenationFailure,
	InvalidArguments,
	BadStream,
}
