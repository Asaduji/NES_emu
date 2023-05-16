using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;
using NES_emu.CPU.Instructions;

namespace NES_emu.CPU.Opcodes
{
    [Instruction(0x08, AddressingMode.IMP, 3)]
    public class PHP : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            cpu.SetFlag(Flag.I, true);
            cpu.SetFlag(Flag.B, true);
            cpu.PushStack(cpu.P);

            return false;
        }
    }
}
