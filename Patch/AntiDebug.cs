using System;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace KanonSys.Patch
{
    public class AntiDebug
    {
        [DllImport("ker" + "nel" + "32" + ".d" + "ll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CheckRemoteDebuggerPresent(IntPtr ProcHHandle, out bool dwReason);

        [DllImport("Ntdll.dll", SetLastError = true)]
        private static extern uint NtSetInformationThread(IntPtr hThread, int ThreadInformationClass, IntPtr ThreadInformation, uint ThreadInformationLength);

        [DllImport("ker" + "nel" + "32" + ".d" + "ll", SetLastError = true)]
        private static extern IntPtr GetCurrentThread();

        public static void firsTech()
        {
            bool checkDebug;

            CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, out checkDebug);

            if (checkDebug)
                
                return;

        }
        public static uint secondTech()
        {
            uint Status;

            Status = NtSetInformationThread(GetCurrentThread(), 17, IntPtr.Zero, 0);

            if (Status != 0)
            {

                string errorMsg = String.Format("Error with NtSetInformationThread : 0x{0:x} n", Status);
                
                return 0;
            }

            

            return 0;
        }

    }
}