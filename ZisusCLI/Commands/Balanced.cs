using System.Diagnostics;

namespace ZisusCLI.Commands {
	internal class Balanced : BaseCommand {
		private const string PowerSchemeGuid = "381b4222-f694-41f0-9685-ff5bb260df2e"; // Balanced

		protected override void Execute() {
			RunProcess("powercfg", $"setactive {PowerSchemeGuid}");
			RunProcess("control", "/name Microsoft.PowerOptions");
		}

		private static void RunProcess(string fileName, string arguments) {
			using var process = Process.Start(new ProcessStartInfo(fileName, arguments) {
				UseShellExecute = true
			});
			process?.WaitForExit();
		}
	}
}