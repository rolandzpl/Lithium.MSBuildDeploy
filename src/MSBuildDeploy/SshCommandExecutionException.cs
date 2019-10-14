using System;
using System.Runtime.Serialization;

namespace Lithium.Build
{
	[Serializable]
	internal class SshCommandExecutionException : Exception
	{
		public SshCommandExecutionException(string errorText, int exitStatus)
			: base($"SSH command execution error: {errorText}, exit status: {exitStatus}")
		{
		}
	}
}