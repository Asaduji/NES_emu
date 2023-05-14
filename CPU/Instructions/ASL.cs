using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0x0A, AddressingMode.ACC, 2)]
    [Instruction(0x06, AddressingMode.ZP, 5)]
    [Instruction(0x16, AddressingMode.ZPX, 6)]
    [Instruction(0x0E, AddressingMode.ABS, 6)]
    [Instruction(0x1E, AddressingMode.ABX, 7)]
    public class ASL : IInstructionHandler
    {
        public static void Execute(Cpu cpu)
        {
            byte value = cpu.CurrentAddressingMode != AddressingMode.ACC ? cpu.Read(cpu.CurrentAddress) : cpu.A;

            cpu.SetFlag(Flag.C, (cpu.A & 1 << 8) != 0);

            cpu.A = (byte)(value << 1);

            cpu.SetFlag(Flag.Z, cpu.A == 0x00);
            cpu.SetFlag(Flag.N, (cpu.A & 1 << 8) != 0);
        }
    }
}
