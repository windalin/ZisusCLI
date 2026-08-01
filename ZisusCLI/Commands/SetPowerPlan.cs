using System.Diagnostics;

namespace ZisusCLI.Commands {
	internal class SetPowerPlan : BaseCommand {
		private readonly int _plan;

		public SetPowerPlan(int plan) {
			_plan = plan;
		}

		protected override void Execute() {
			string guid = _plan switch {
				1 => "381b4222-f694-41f0-9685-ff5bb260df2e", // Balanced
				2 => "8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c", // High performance
				3 => "a1841308-3541-4fab-bc81-f71556f20b4a", // Power saver
				_ => "381b4222-f694-41f0-9685-ff5bb260df2e"
			};

			RunProcess("powercfg", $"setactive {guid}");
			RunProcess("control", "/name Microsoft.PowerOptions");
		}

		private static void RunProcess(string fileName, string arguments) {
			using var process = Process.Start(new ProcessStartInfo(fileName, arguments) {
				UseShellExecute = true
			});
			process?.WaitForExit();
		}
	}
}