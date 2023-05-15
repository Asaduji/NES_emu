using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0xA0, AddressingMode.IMM, 2)]
    [Instruction(0xA4, AddressingMode.ZP, 3)]
    [Instruction(0xB4, AddressingMode.ZPX, 4)]
    [Instruction(0xAC, AddressingMode.ABS, 4)]
    [Instruction(0xBC, AddressingMode.ABX, 4, true)]
    public class LDY : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            byte value = cpu.Read(cpu.CurrentAddress);

            cpu.Y = value;

            cpu.SetFlag(Flag.Z, cpu.Y == 0x00);

            cpu.SetFlag(Flag.N, (cpu.Y & (1 << 7)) != 0);

            return true;
        }
    }
}
