using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;
using NES_emu.CPU.Instructions;

namespace NES_emu.CPU.Opcodes
{
    [Instruction(0xF8, AddressingMode.IMP, 2)]
    public class SED : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            cpu.SetFlag(Flag.D, true);
            return false;
        }
    }
}
