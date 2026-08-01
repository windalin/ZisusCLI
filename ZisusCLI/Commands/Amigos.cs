using System.Diagnostics;

namespace ZisusCLI.Commands {
	internal class Amigos : BaseCommand {
		protected override void Execute() {
			Process.Start(new ProcessStartInfo("https://claude.ai") { UseShellExecute = true });
			Process.Start(new ProcessStartInfo("https://chatgpt.com") { UseShellExecute = true });
		}
	}
}
