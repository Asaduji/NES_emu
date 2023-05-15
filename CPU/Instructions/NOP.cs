using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;
using NES_emu.CPU.Instructions;

namespace NES_emu.CPU.Opcodes
{
    [Instruction(0xEA, AddressingMode.IMP, 2)]
    public class NOP : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            return false;
        }
    }
}
