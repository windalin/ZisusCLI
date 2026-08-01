using System.Diagnostics;
using ZisusCLI.Commands.Constants;

namespace ZisusCLI.Commands {
	internal class Balanced : BaseCommand {
		protected override void Execute() {
			RunProcess("powercfg", $"setactive {PowerPlans.Balanced}");
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