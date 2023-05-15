using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0xB8, AddressingMode.IMP, 2)]
    public class CLV : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            cpu.SetFlag(Flag.V, false);
            return false;
        }
    }
}
