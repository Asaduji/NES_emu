using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0xA8, AddressingMode.IMP, 2)]
    public class TAY : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            cpu.Y = cpu.A;

            cpu.SetFlag(Flag.Z, cpu.Y == 0x00);
            cpu.SetFlag(Flag.N, (cpu.Y & (1 << 7)) != 0);

            return false;
        }
    }
}
