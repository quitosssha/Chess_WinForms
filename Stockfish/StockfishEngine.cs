using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Stockfish
{
	public class StockfishEngine
	{
		private Process stockfishProcess;

		public StockfishEngine(string pathToExecutable)
		{
			stockfishProcess = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = pathToExecutable,
					UseShellExecute = false,
					RedirectStandardInput = true,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				}
			};
		}

		public void StartEngine()
		{
			stockfishProcess.Start();
			SendCommand("uci");
		}

		public void SendCommand(string command)
		{
			stockfishProcess.StandardInput.WriteLine(command);
			stockfishProcess.StandardInput.Flush();
		}

		public async Task<string> ReadOutputAsync()
		{
			string output = await stockfishProcess.StandardOutput.ReadLineAsync();
			return output;
		}

		public void StopEngine()
		{
			SendCommand("quit");
			stockfishProcess.Close();
		}
	}
}
