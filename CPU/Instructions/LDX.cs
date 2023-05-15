using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0xA2, AddressingMode.IMM, 2)]
    [Instruction(0xA6, AddressingMode.ZP, 3)]
    [Instruction(0xB6, AddressingMode.ZPY, 4)]
    [Instruction(0xAE, AddressingMode.ABS, 4)]
    [Instruction(0xBE, AddressingMode.ABY, 4, true)]
    public class LDX : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            byte value = cpu.Read(cpu.CurrentAddress);

            cpu.X = value;

            cpu.SetFlag(Flag.Z, cpu.X == 0x00);

            cpu.SetFlag(Flag.N, (cpu.X & (1 << 7)) != 0);

            return true;
        }
    }
}
