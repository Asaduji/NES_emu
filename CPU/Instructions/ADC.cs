using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0x69, AddressingMode.IMM, 2)]
    public class ADC : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            
        }
    }
}
