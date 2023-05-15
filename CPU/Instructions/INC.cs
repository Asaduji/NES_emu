using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0xE6, AddressingMode.ZP, 5)]
    [Instruction(0xF6, AddressingMode.ZPX, 6)]
    [Instruction(0xEE, AddressingMode.ABS, 6)]
    [Instruction(0xFE, AddressingMode.ABX, 7)]
    public class INC : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            byte data = cpu.Read(cpu.CurrentAddress);

            ++data;

            cpu.SetFlag(Flag.Z, data == 0x00);
            cpu.SetFlag(Flag.N, (data & (1 << 7)) != 0);

            cpu.Write(cpu.CurrentAddress, data);

            return false;
        }
    }
}