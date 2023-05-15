using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0x8A, AddressingMode.IMP, 2)]
    public class TXA : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            cpu.A = cpu.X;

            cpu.SetFlag(Flag.Z, cpu.A == 0x00);
            cpu.SetFlag(Flag.N, (cpu.A & (1 << 7)) != 0);

            return false;
        }
    }
}
