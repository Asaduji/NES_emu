using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0x24, AddressingMode.ZP, 3)]
    [Instruction(0x2C, AddressingMode.ABS, 4)]
    public class BIT : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            byte value = cpu.Read(cpu.CurrentAddress);

            cpu.SetFlag(Flag.Z, (cpu.A & value) == 0);

            cpu.SetFlag(Flag.V, (value & (1 << 6)) != 0);

            cpu.SetFlag(Flag.N, (value & (1 << 7)) != 0);

            return false;
        }
    }
}
