using System.Diagnostics;

namespace ZisusCLI.Commands {
	internal class EditAlias : BaseCommand {
		protected override void Execute() {
			using var process = Process.Start(new ProcessStartInfo("powershell", "-Command \"Import-Module CustomAliases; editalias\"") {
				UseShellExecute = true
			});
		}
	}
}