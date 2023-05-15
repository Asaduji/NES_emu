using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;
using NES_emu.CPU.Instructions;

namespace NES_emu.CPU.Opcodes
{
    [Instruction(0x78, AddressingMode.IMP, 2)]
    public class SEI : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            cpu.SetFlag(Flag.I, true);
            return false;
        }
    }
}
