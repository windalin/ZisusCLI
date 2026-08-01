using System.Diagnostics;

namespace ZisusCLI.Commands {
	internal class Extend : BaseCommand {
		protected override void Execute() {
			using var process = Process.Start(new ProcessStartInfo("DisplaySwitch.exe", "/extend") {
				UseShellExecute = true
			});
			process?.WaitForExit();
		}
	}
}