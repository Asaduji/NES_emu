﻿using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0xCA, AddressingMode.IMP, 2)]
    public class DEX : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            --cpu.X;

            cpu.SetFlag(Flag.Z, cpu.X == 0x00);
            cpu.SetFlag(Flag.N, (cpu.X & (1 << 7)) != 0);

            return false;
        }
    }
}