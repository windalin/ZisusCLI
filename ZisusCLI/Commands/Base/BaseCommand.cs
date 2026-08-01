using System;

namespace ZisusCLI.Commands {
	internal abstract class BaseCommand {
		public int Run() {
			try {
				Execute();
				return 0;
			} catch (Exception ex) {
				Console.Error.WriteLine($"Command failed: {ex.Message}");
				return 1;
			}
		}

		protected abstract void Execute();
	}
}