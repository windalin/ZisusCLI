using System;
using System.Threading.Tasks;

namespace ZisusCLI {
	internal class Program {
		static async Task<int> Main(string[] args) {
			if (args.Length == 0) return 1;
			
			string command = args[0];
			switch (command) {
				case "amigos":			 return new Commands.Amigos().Run();
				case "balanced":		 return new Commands.SetPowerPlan(1).Run();
				case "editalias":		 return new Commands.EditAlias().Run();
				case "editbats":		 return new Commands.EditBats().Run();
				case "extend":			 return new Commands.SetDisplayMode(3).Run();
				case "inputswitch":		 return new Commands.InputSwitch().Run();
				case "psaver":			 return new Commands.SetPowerPlan(3).Run();
				case "secondscreenonly": return new Commands.SetDisplayMode(1).Run(); // windows main screen is physical second screen bc i dont want to change cable ports
				case "userchromeopen":   return new Commands.OpenUserChrome().Run();
				case "volfix":			 return new Commands.VolFix().Run();
				case "weather":			 return new Commands.Weather().Run();
				default:				 await Console.Out.WriteLineAsync($"Unknown command: {command}"); return 1;
			}
		}
	}
}