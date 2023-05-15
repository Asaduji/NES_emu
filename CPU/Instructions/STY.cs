using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0x84, AddressingMode.ZP, 3)]
    [Instruction(0x94, AddressingMode.ZPX, 4)]
    [Instruction(0x8C, AddressingMode.ABS, 4)]
    public class STY : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            cpu.Write(cpu.CurrentAddress, cpu.Y);

            return false;
        }
    }
}
