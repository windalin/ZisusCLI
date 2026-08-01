using System.Diagnostics;

namespace ZisusCLI.Commands {
	internal class EditBats : BaseCommand {
		protected override void Execute() {
			string notepadPath = Config.Value["EditBats:NotepadPlusPlusPath"]!;
			string batFolder = Config.Value["EditBats:BatFolder"]!;

			using var process = Process.Start(new ProcessStartInfo(notepadPath, $"\"{batFolder}\\*.bat\"") {
				UseShellExecute = true
			});
		}
	}
}