using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;
using NES_emu.CPU.Instructions;

namespace NES_emu.CPU.Opcodes
{
    [Instruction(0x68, AddressingMode.IMP, 4)]
    public class PLA : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            cpu.A = cpu.PopStack();

            cpu.SetFlag(Flag.Z, cpu.A == 0x00);
            cpu.SetFlag(Flag.N, (cpu.A & (1 << 7)) == 0x00);

            return false;
        }
    }
}
