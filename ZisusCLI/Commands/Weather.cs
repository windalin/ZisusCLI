using System.Diagnostics;

namespace ZisusCLI.Commands {
	internal class Weather : BaseCommand {
		protected override void Execute() {
			using var process = Process.Start(new ProcessStartInfo("https://www.google.com/search?q=auckland+weather") {
				UseShellExecute = true
			});
		}
	}
}