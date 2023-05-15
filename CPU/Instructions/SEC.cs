using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;
using NES_emu.CPU.Instructions;

namespace NES_emu.CPU.Opcodes
{
    [Instruction(0x38, AddressingMode.IMP, 2)]
    public class SEC : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            cpu.SetFlag(Flag.C, true);
            return false;
        }
    }
}
