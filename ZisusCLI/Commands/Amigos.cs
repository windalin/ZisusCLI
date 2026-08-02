using System.Diagnostics;

namespace ZisusCLI.Commands {
	internal class Amigos : BaseCommand {
		protected override void Execute() {
			var clankers = Config.Value.GetSection("Amigos:Clankers").GetChildren();
			foreach (var clanker in clankers) Process.Start(new ProcessStartInfo(clanker.Value) { UseShellExecute = true });
		}
	}
}
