using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0x6A, AddressingMode.ACC, 2)]
    [Instruction(0x66, AddressingMode.ZP, 5)]
    [Instruction(0x76, AddressingMode.ZPX, 6)]
    [Instruction(0x6E, AddressingMode.ABS, 6)]
    [Instruction(0x7E, AddressingMode.ABX, 7)]
    public class ROR : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            byte value = cpu.CurrentAddressingMode != AddressingMode.ACC ? cpu.Read(cpu.CurrentAddress) : cpu.A;

            cpu.SetFlag(Flag.C, (value & 1) != 0);

            value = (byte)(value >> 1);

            if (cpu.GetFlag(Flag.C))
            {
                value |= (1 << 7);
            }

            

            cpu.SetFlag(Flag.Z, value == 0x00);
            cpu.SetFlag(Flag.N, (value & (1 << 7)) != 0);

            if (cpu.CurrentAddressingMode != AddressingMode.ACC)
            {
                cpu.Write(cpu.CurrentAddress, (byte)value);
            }
            else
            {
                cpu.A = (byte)value;
            }

            return false;
        }
    }
}
