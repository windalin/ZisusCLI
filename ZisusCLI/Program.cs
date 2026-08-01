using System;
using System.Threading.Tasks;
using ZisusCLI.Commands.Constants;

namespace ZisusCLI {
	internal class Program {
		static async Task<int> Main(string[] args) {
			if (args.Length == 0) return 1;
			
			string command = args[0];
			switch (command) {
				case "amigos":
					return new Commands.Amigos().Run();
				case "balanced":
					return new Commands.SetPowerPlan(PowerPlans.Balanced).Run();
				case "psaver":
					return new Commands.SetPowerPlan(PowerPlans.PowerSaver).Run();
				case "extend":
					return new Commands.Extend().Run();
				case "inputswitch":
					return new Commands.InputSwitch().Run();
				default:
					await Console.Out.WriteLineAsync($"Unknown command: {command}");
					return 1;
			}
		}
	}
}