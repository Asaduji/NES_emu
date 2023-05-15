using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0x58, AddressingMode.IMP, 2)]
    public class CLI : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            cpu.SetFlag(Flag.I, false);
            return false;
        }
    }
}
