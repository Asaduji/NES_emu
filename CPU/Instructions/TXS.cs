using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0x9A, AddressingMode.IMP, 2)]
    public class TXS : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            cpu.S = cpu.X;

            return false;
        }
    }
}
