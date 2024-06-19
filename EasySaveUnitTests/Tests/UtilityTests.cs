using EasySaveUnitTest.Utility;
using NEG.Plugins.EasySave.Utility;

namespace NEG.Plugins.EasySave.Tests;

internal sealed class UtilityTests
{
	private const string dataPath = @"\EasySave Tests";

	private static readonly string userPath = UserDirectoryUtility.LocalLow + dataPath;

	private readonly IReadOnlyList<string> testPassFiles =
	[
		"File",
		"lifi",
		"Extra Cool Name (21)"
	];
	private readonly IReadOnlyList<string> testFailFiles =
	[
		$"{userPath}\\Doijhdfsaoipoh\\DeeperDir\0",
		$"{userPath}\\Doijhdfsaoipoh\\..>",
		"C::",
		"NUL",
		"NULL",
		"CON",
		"AUX",
		"FileName.json",
		"Wee::"
	];

	private readonly IReadOnlyList<string> testPassDirectories =
	[
		$"{userPath}\\TestDir",
			$"{userPath}\\Dir",
			$"{userPath}\\Dir\\DeeperDir",
			$"{userPath}\\Doijhdfsaoipoh\\DeeperDir\\",
		];
	private readonly IReadOnlyList<string> testFailDirectories =
	[
		$"{userPath}\\Doijhdfsaoipoh\\DeeperDir\0",
		$"{userPath}\\Doijhdfsaoipoh\\..>",
		"Dingleberry",
		"C::",
			"NUL",
			"NULL",
			"CON",
			"AUX",
		];

	[Test(Author = "12Acorns", Description = "", TestOf = typeof(FileUtility))]
	public void ValidFileValidationTest()
	{
		foreach(var _test in testPassFiles)
		{
			Assert.That(FileUtility.IsFileNameValid(_test), Is.True);
		}
	}
	[Test(Author = "12Acorns", Description = "", TestOf = typeof(FileUtility))]
	public void InvalidFileValidationTest()
	{
		foreach(var _test in testFailFiles)
		{
			Assert.That(FileUtility.IsFileNameValid(_test), Is.False);
		}
	}

	[Test(Author = "12Acorns", Description = "", TestOf = typeof(DirectoryUtility))]
	public void ValidDirectoryValidationTest()
	{
		foreach(var _test in testPassDirectories)
		{
			Assert.That(DirectoryUtility.TryCreateDirectory(_test, out _), Is.True);
		}
		try
		{
			if(Directory.Exists(userPath))
			{
				Directory.Delete(userPath);
			}
		}
		catch { }
	}
	[Test(Author = "12Acorns", Description = "", TestOf = typeof(DirectoryUtility))]
	public void InvalidDirectoryValidationTest()
	{
		foreach(var _test in testFailDirectories)
		{
			Assert.That(DirectoryUtility.TryCreateDirectory(_test, out _), Is.False);
		}
	}
}
