using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0x09, AddressingMode.IMM, 2)]
    [Instruction(0x05, AddressingMode.ZP, 3)]
    [Instruction(0x15, AddressingMode.ZPX, 4)]
    [Instruction(0x0D, AddressingMode.ABS, 4)]
    [Instruction(0x1D, AddressingMode.ABX, 4, true)]
    [Instruction(0x19, AddressingMode.ABY, 4, true)]
    [Instruction(0x01, AddressingMode.IZX, 6)]
    [Instruction(0x11, AddressingMode.IZY, 5, true)]
    public class ORA : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            byte value = cpu.Read(cpu.CurrentAddress);

            cpu.A |= value;

            cpu.SetFlag(Flag.Z, cpu.A == 0x00);

            cpu.SetFlag(Flag.N, (cpu.A & (1 << 7)) != 0);

            return true;
        }
    }
}
