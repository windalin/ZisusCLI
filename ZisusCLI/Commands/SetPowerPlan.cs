using System.Diagnostics;

namespace ZisusCLI.Commands {
	internal class SetPowerPlan : BaseCommand {
		private readonly string _planGuid;

		public SetPowerPlan(string planGuid) {
			_planGuid = planGuid;
		}

		protected override void Execute() {
			RunProcess("powercfg", $"setactive {_planGuid}");
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