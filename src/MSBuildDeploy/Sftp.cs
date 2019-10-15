using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lithium.Build
{
	public class Sftp : Task
	{
		public string Host { get; set; }

		public int Port { get; set; }

		public string User { get; set; }

		public string Keyfile { get; set; }

		public string Source { get; set; }

		public string Destination { get; set; }

		public override bool Execute()
		{
			try
			{
				var connectionInfo = new ConnectionInfo(Host, Port, User,
					new PrivateKeyAuthenticationMethod(User, new PrivateKeyFile(Keyfile)));
				using var client = new SftpClient(connectionInfo);
				client.Connect();
				Log.LogMessage(MessageImportance.Normal, $"Connected to {Host} as {User}");
				Log.LogMessage(MessageImportance.Normal, $"Synchronizing {Source} with {Destination}");
				Synchronize(client, Source, Destination);
				client.SynchronizeDirectories(Source, Destination, "*");
			}
			catch (Exception ex)
			{
				Log.LogError($"Failed synchronizing, Details: {ex}");
			}
			return !Log.HasLoggedErrors;
		}

		private void Synchronize(SftpClient client, string source, string destination)
		{
			if (IsSourceDirectory(source))
			{
				foreach (var f in GetLocalFiles(source))
				{
					var file = GetFileName(f);
					CopySourceFileToDestination(client, f, GetRemoteDestination(destination, file));
				}
				foreach (var d in GetSourceDirectories(source))
				{
					var directory = GetDirectoryName(d);
					if (!DestinationDirectoryExists(client, GetRemoteDestination(destination, directory)))
					{
						CreateDestinationDirectory(client, GetRemoteDestination(destination, directory));
					}
					Synchronize(client, d, GetRemoteDestination(destination, directory));
				}
			}
			else
			{
				CopySourceFileToDestination(client, source, GetRemoteDestination(destination, source));
			}
		}

		private string GetFileName(string path)
		{
			var f = new FileInfo(path);
			return f.Name;
		}

		private string GetDirectoryName(string directory)
		{
			var dir = new DirectoryInfo(directory);
			return dir.Name;
		}

		private void CreateDestinationDirectory(SftpClient client, string directory)
		{
			client.CreateDirectory(directory);
		}

		private bool DestinationDirectoryExists(SftpClient client, string d)
		{
			try
			{
				var attr = client.GetAttributes(d);
				return attr.IsDirectory;
			}
			catch
			{
				return false;
			}
		}

		private string GetRemoteDestination(string source, string directory)
		{
			return $"{source}/{directory}";
		}

		private IEnumerable<string> GetSourceDirectories(string path)
		{
			return Directory.GetDirectories(path);
		}

		private void CopySourceFileToDestination(SftpClient client, string file, string destination)
		{
			client.UploadFile(GetSourceStream(file), destination);
		}

		private Stream GetSourceStream(string path)
		{
			return File.OpenRead(path);
		}

		private IEnumerable<string> GetLocalFiles(string path)
		{
			return Directory.GetFiles(path);
		}

		private bool IsSourceDirectory(string path)
		{
			return Directory.Exists(path);
		}
	}
}
