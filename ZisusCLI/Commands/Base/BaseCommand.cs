using Microsoft.Extensions.Configuration;
using System;

namespace ZisusCLI.Commands {
	internal abstract class BaseCommand {

		public static Lazy<IConfiguration> Config { get; } = new(() => new ConfigurationBuilder()
			.SetBasePath(AppContext.BaseDirectory)
			.AddJsonFile("appsettings.json")
			.AddJsonFile("appsettings.local.json", optional: true)
			.Build());

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