namespace EasySaveUnitTest.Utility;

internal static partial class UserDirectoryUtility
{
	public static readonly string LocalLow =
		string.Concat(Environment.GetEnvironmentVariable(LOCALAPPDATA)!, "Low");

	private const string LOCALAPPDATA = "LocalAppData";
}
