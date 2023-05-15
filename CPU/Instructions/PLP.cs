using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;
using NES_emu.CPU.Instructions;

namespace NES_emu.CPU.Opcodes
{
    [Instruction(0x28, AddressingMode.IMP, 4)]
    public class PLP : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            cpu.P = cpu.PopStack();

            return false;
        }
    }
}
