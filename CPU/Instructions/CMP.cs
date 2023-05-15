using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0xC9, AddressingMode.IMM, 2)]
    [Instruction(0xC5, AddressingMode.ZP, 3)]
    [Instruction(0xD5, AddressingMode.ZPX, 4)]
    [Instruction(0xCD, AddressingMode.ABS, 4)]
    [Instruction(0xDD, AddressingMode.ABX, 4, true)]
    [Instruction(0xD9, AddressingMode.ABY, 4, true)]
    [Instruction(0xC1, AddressingMode.IZX, 6)]
    [Instruction(0xD1, AddressingMode.IZY, 5, true)]
    public class CMP : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            byte data = cpu.Read(cpu.CurrentAddress);

            cpu.SetFlag(Flag.C, cpu.A >= data);
            cpu.SetFlag(Flag.Z, cpu.A == data);
            cpu.SetFlag(Flag.N, (data & (1 << 7)) != 0);

            return true;
        }
    }
}