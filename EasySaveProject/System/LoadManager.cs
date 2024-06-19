using NEG.Plugins.EasySave.Data;
using NEG.Plugins.EasySave.Data.Path;
using Newtonsoft.Json;

namespace NEG.Plugins.EasySave.System
{
    public sealed class LoadManager
    {
		public LoadManager(ApplicationPath _applicationPath)
		{
			ApplicationPath = _applicationPath;
		}

		public static LoadManager Instance { get; } = new(ApplicationPath.Instance);

		public ApplicationPath ApplicationPath { get; }

		public bool TryDeserializeFile<File>(string _fileContents, out File _file)
			where File : ISaveable
		{
			var _tmp = JsonConvert.DeserializeObject<File>(_fileContents);
			_file = _tmp!;
			return _tmp != null;
		}
	}
}
