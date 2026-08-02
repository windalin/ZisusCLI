using System.Diagnostics;

namespace ZisusCLI.Commands {
	internal class Weather : BaseCommand {
		protected override void Execute() {
			var locations = Config.Value.GetSection("Weather:Locations").GetChildren();
			foreach (var location in locations) Process.Start(new ProcessStartInfo($"https://www.google.com/search?q={location.Value} weather") { UseShellExecute = true });
		}
	}
}