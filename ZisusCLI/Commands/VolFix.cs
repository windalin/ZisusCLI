using System;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace ZisusCLI.Commands {
	internal class VolFix : BaseCommand {
		protected override void Execute() {
			var enumerator = new MMDeviceEnumerator();
			var device = enumerator.GetDefaultAudioEndpoint();
			string name = device.GetFriendlyName();

			Console.WriteLine($"Device: {name}");

			float? volume = name switch {
				var n when n.Contains("HUAWEI FreeLace", StringComparison.OrdinalIgnoreCase) => 0.27f,
				var n when n.Contains("WH-1000XM5", StringComparison.OrdinalIgnoreCase) => 0.38f,
				_ => null
			};

			if (volume.HasValue) {
				device.SetVolume(volume.Value);
				Console.WriteLine($"Set volume: {volume.Value * 100}%");

				string mediaPath = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.Windows),
					@"Media\Windows Background.wav");

				try {
					if (File.Exists(mediaPath)) {
						using var player = new SoundPlayer(mediaPath);
						player.PlaySync();
					}
				} catch (Exception e) {
					Console.WriteLine($"Error playing media: {e.Message}");
				}
			} else {
				Console.WriteLine("No match.");
			}
		}
	}

	internal static partial class NativeMethods {
		[LibraryImport("ole32.dll")]
		public static partial int CoCreateInstance(
			ref Guid rclsid,
			IntPtr pUnkOuter,
			uint dwClsContext,
			ref Guid riid,
			out IntPtr ppv);
	}

	internal class MMDeviceEnumerator {
		private static readonly StrategyBasedComWrappers Wrappers = new();
		private readonly IMMDeviceEnumerator enumerator;

		public MMDeviceEnumerator() {
			Guid clsid = new("BCDE0395-E52F-467C-8E3D-C4579291692E");
			Guid iid = new("A95664D2-9614-4F35-A746-DE8DB63617E6");

			NativeMethods.CoCreateInstance(ref clsid, IntPtr.Zero, (uint)CLSCTX.ALL, ref iid, out IntPtr ptr);

			enumerator = (IMMDeviceEnumerator)Wrappers.GetOrCreateObjectForComInstance(ptr, CreateObjectFlags.None);
		}

		public MMDevice GetDefaultAudioEndpoint() {
			enumerator.GetDefaultAudioEndpoint(
				EDataFlow.eRender,
				ERole.eMultimedia,
				out var device);

			return new MMDevice(device);
		}
	}

	internal class MMDevice {
		private static readonly StrategyBasedComWrappers Wrappers = new();
		private readonly IMMDevice device;

		public MMDevice(IMMDevice device) {
			this.device = device;
		}

		public string GetFriendlyName() {
			device.OpenPropertyStore(0, out var store);

			var key = new PROPERTYKEY {
				fmtid = new Guid("a45c254e-df1c-4efd-8020-67d146a850e0"),
				pid = 14
			};

			store.GetValue(ref key, out var value);
			return value.GetString();
		}

		public void SetVolume(float level) {
			Guid iid = typeof(IAudioEndpointVolume).GUID;
			device.Activate(ref iid, CLSCTX.ALL, IntPtr.Zero, out IntPtr ptr);

			var volume = (IAudioEndpointVolume)Wrappers.GetOrCreateObjectForComInstance(ptr, CreateObjectFlags.None);
			volume.SetMasterVolumeLevelScalar(level, Guid.Empty);
		}
	}

	[GeneratedComInterface]
	[Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
	internal partial interface IMMDeviceEnumerator {
		void EnumAudioEndpoints(int dataFlow, int stateMask, out IntPtr devices);

		void GetDefaultAudioEndpoint(
			EDataFlow dataFlow,
			ERole role,
			out IMMDevice endpoint);
	}

	[GeneratedComInterface]
	[Guid("D666063F-1587-4E43-81F1-B948E807363F")]
	internal partial interface IMMDevice {
		void Activate(
			ref Guid iid,
			CLSCTX clsCtx,
			IntPtr activationParams,
			out IntPtr instance);

		void OpenPropertyStore(
			int stgmAccess,
			out IPropertyStore store);
	}

	[GeneratedComInterface]
	[Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99")]
	internal partial interface IPropertyStore {
		void GetCount(out int count);

		void GetAt(int index, out PROPERTYKEY key);

		void GetValue(
			ref PROPERTYKEY key,
			out PROPVARIANT value);
	}

	[GeneratedComInterface]
	[Guid("5CDF2C82-841E-4546-9722-0CF74078229A")]
	internal partial interface IAudioEndpointVolume {
		void RegisterControlChangeNotify(IntPtr notify);
		void UnregisterControlChangeNotify(IntPtr notify);
		void GetChannelCount(out uint count);
		void SetMasterVolumeLevel(float level, Guid eventContext);
		void SetMasterVolumeLevelScalar(float level, Guid eventContext);
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct PROPERTYKEY {
		public Guid fmtid;
		public int pid;
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct PROPVARIANT {
		public short vt;
		public short wReserved1;
		public short wReserved2;
		public short wReserved3;
		public IntPtr data;

		public string GetString() {
			return Marshal.PtrToStringUni(data) ?? "";
		}
	}

	internal enum EDataFlow {
		eRender,
		eCapture,
		eAll
	}

	internal enum ERole {
		eConsole,
		eMultimedia,
		eCommunications
	}

	[Flags]
	internal enum CLSCTX : uint {
		ALL = 23
	}
}