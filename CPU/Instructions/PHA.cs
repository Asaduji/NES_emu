﻿using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;
using NES_emu.CPU.Instructions;

namespace NES_emu.CPU.Opcodes
{
    [Instruction(0x48, AddressingMode.IMP, 3)]
    public class PHA : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            cpu.PushStack(cpu.A);

            return false;
        }
    }
}
