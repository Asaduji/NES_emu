﻿using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0xD0, AddressingMode.REL, 2, true)]
    public class BNE : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            if (!cpu.GetFlag(Flag.Z))
            {
                cpu.PC = cpu.CurrentAddress;

                ++cpu.Cycles;

                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}
