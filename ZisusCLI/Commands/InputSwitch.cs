using System;
using System.Diagnostics;
using System.IO;

namespace ZisusCLI.Commands {
	internal class InputSwitch : BaseCommand {
		private static readonly string FlagFile = Path.Combine(AppContext.BaseDirectory, "config", "currentinput.txt");

		protected override void Execute() {
			CreateConfig();

			string toolPath = Config.Value["InputSwitch:ControlMyMonitorPath"]!;
			bool isDp1 = File.ReadAllText(FlagFile).Trim() == "dp1";
			Process.Start(toolPath, $"/SetValue \"\\\\.\\DISPLAY2\\Monitor0\" 60 {(isDp1 ? 18 : 15)}");
			File.WriteAllText(FlagFile, isDp1 ? "hdmi2" : "dp1");
		}

		private void CreateConfig() {
			if (!File.Exists(FlagFile)) {
				Directory.CreateDirectory(Path.GetDirectoryName(FlagFile)!);
				File.WriteAllText(FlagFile, "dp1");
			}
		}
	}
}