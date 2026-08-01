using System.Diagnostics;

namespace ZisusCLI.Commands {
	internal class OpenUserChrome : BaseCommand {
		protected override void Execute() {
			string notepadPath = Config.Value["OpenUserChrome:NotepadPlusPlusPath"]!;
			string userChromePath = Config.Value["OpenUserChrome:UserChromePath"]!;

			using var process = Process.Start(new ProcessStartInfo(notepadPath, $"\"{userChromePath}\"") {
				UseShellExecute = true
			});
		}
	}
}