using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0xC6, AddressingMode.ZP, 5)]
    [Instruction(0xD6, AddressingMode.ZPX, 6)]
    [Instruction(0xCE, AddressingMode.ABS, 6)]
    [Instruction(0xDE, AddressingMode.ABX, 7)]
    public class DEC : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            byte data = cpu.Read(cpu.CurrentAddress);

            --data;

            cpu.SetFlag(Flag.Z, data == 0x00);
            cpu.SetFlag(Flag.N, (data & (1 << 7)) != 0);

            cpu.Write(cpu.CurrentAddress, data);

            return false;
        }
    }
}