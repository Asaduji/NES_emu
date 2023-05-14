using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0x69, AddressingMode.IMM, 2)]
    [Instruction(0x65, AddressingMode.ZP, 3)]
    [Instruction(0x75, AddressingMode.ZPX, 4)]
    [Instruction(0x6D, AddressingMode.ABS, 4)]
    [Instruction(0x7D, AddressingMode.ABX, 4, true)]
    [Instruction(0x79, AddressingMode.ABY, 4, true)]
    [Instruction(0x61, AddressingMode.IZX, 6)]
    [Instruction(0x71, AddressingMode.IZY, 5, true)]
    public class ADC : IInstructionHandler
    {
        public static void Execute(Cpu cpu)
        {
            byte value = cpu.Read(cpu.CurrentAddress);

            ushort sum = (ushort)(cpu.A + value);


            cpu.SetFlag(Flag.V, ((value ^ cpu.A ^ (byte)sum) & (1 << 8)) != 0 && (byte)(sum >> 8) != 0);
            

            cpu.A = (byte)sum;

            cpu.SetFlag(Flag.C, sum > 0xFF);

            cpu.SetFlag(Flag.Z, cpu.A == 0x00);

            cpu.SetFlag(Flag.N, (cpu.A & 1 << 8) != 0);
        }
    }
}
