using System.Diagnostics;

namespace ZisusCLI.Commands {
	internal class Weather : BaseCommand {
		protected override void Execute() {
			foreach (var location in Config.Value.GetSection("Weather:Locations").GetChildren())
				Process.Start(new ProcessStartInfo($"https://www.google.com/search?q={location.Value} weather") { UseShellExecute = true });
		}
	}
}