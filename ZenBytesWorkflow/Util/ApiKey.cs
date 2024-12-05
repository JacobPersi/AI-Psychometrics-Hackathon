namespace ZenBytesWorkflow.Util;

using System.IO;

public static class ApiKey
{
	private static string _cachedApiKey;
	private static string _filePath = "api_key.txt";

	public static string Value
	{
		get
		{
			if (_cachedApiKey == null)
			{
				if (!File.Exists(_filePath))
				{
					throw new FileNotFoundException("The API key file was not found.", _filePath);
				}

				_cachedApiKey = File.ReadAllText(_filePath).Trim();
			}

			return _cachedApiKey;
		}
	}
}
