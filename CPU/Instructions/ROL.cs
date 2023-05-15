using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0x2A, AddressingMode.ACC, 2)]
    [Instruction(0x26, AddressingMode.ZP, 5)]
    [Instruction(0x36, AddressingMode.ZPX, 6)]
    [Instruction(0x2E, AddressingMode.ABS, 6)]
    [Instruction(0x3E, AddressingMode.ABX, 7)]
    public class ROL : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            ushort value = cpu.CurrentAddressingMode != AddressingMode.ACC ? cpu.Read(cpu.CurrentAddress) : cpu.A;

            cpu.SetFlag(Flag.C, ((byte)value & 1 << 7) != 0);

            value = (ushort)(value << 1);

            value |= (byte)((value >> 8) & 1);

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
