using System.Diagnostics;

namespace ZisusCLI.Commands {
	internal class SetDisplayMode : BaseCommand {
		private readonly int _mode;

		public SetDisplayMode(int mode) {
			_mode = mode;
		}

		protected override void Execute() {
			string arg = _mode switch {
				1 => "/internal",
				2 => "/clone",
				3 => "/extend",
				4 => "/external",
				_ => "/extend"
			};

			using var process = Process.Start(new ProcessStartInfo("DisplaySwitch.exe", arg) {
				UseShellExecute = true
			});
			process?.WaitForExit();
		}
	}
}