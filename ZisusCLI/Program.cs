using System;
using System.Threading.Tasks;

namespace ZisusCLI {
	internal class Program {
		static async Task<int> Main(string[] args) {
			if (args.Length == 0) return 1;
			
			string command = args[0];
			switch (command) {
				case "amigos":
					return new Commands.Amigos().Run();
				default:
					await Console.Out.WriteLineAsync($"Unknown command: {command}");
					return 1;
			}
		}
	}
}