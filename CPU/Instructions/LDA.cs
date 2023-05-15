using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0xA9, AddressingMode.IMM, 2)]
    [Instruction(0xA5, AddressingMode.ZP, 3)]
    [Instruction(0xB5, AddressingMode.ZPX, 4)]
    [Instruction(0xAD, AddressingMode.ABS, 4)]
    [Instruction(0xBD, AddressingMode.ABX, 4, true)]
    [Instruction(0xB9, AddressingMode.ABY, 4, true)]
    [Instruction(0xA1, AddressingMode.IZX, 6)]
    [Instruction(0xB1, AddressingMode.IZY, 5, true)]
    public class LDA : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            byte value = cpu.Read(cpu.CurrentAddress);

            cpu.A = value;

            cpu.SetFlag(Flag.Z, cpu.A == 0x00);

            cpu.SetFlag(Flag.N, (cpu.A & (1 << 7)) != 0);

            return true;
        }
    }
}
