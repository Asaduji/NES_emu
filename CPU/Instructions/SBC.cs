using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0xE9, AddressingMode.IMM, 2)]
    [Instruction(0xE5, AddressingMode.ZP, 3)]
    [Instruction(0xF5, AddressingMode.ZPX, 4)]
    [Instruction(0xED, AddressingMode.ABS, 4)]
    [Instruction(0xFD, AddressingMode.ABX, 4, true)]
    [Instruction(0xF9, AddressingMode.ABY, 4, true)]
    [Instruction(0xE1, AddressingMode.IZX, 6)]
    [Instruction(0xF1, AddressingMode.IZY, 5, true)]
    public class SBC : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            byte value = cpu.Read(cpu.CurrentAddress);

            ushort sub = (ushort)(cpu.A - value);

            cpu.SetFlag(Flag.V, ((value ^ cpu.A) & (1 << 7)) == 0 && ((value ^ (byte)sub) & (1 << 7)) == 0);
            
            cpu.A = (byte)sub;

            cpu.SetFlag(Flag.C, sub <= 0xFF);

            cpu.SetFlag(Flag.Z, cpu.A == 0x00);

            cpu.SetFlag(Flag.N, (cpu.A & (1 << 7)) != 0);

            return true;
        }
    }
}
