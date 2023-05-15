using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0x4C, AddressingMode.ABS, 3)]
    [Instruction(0x6C, AddressingMode.IND, 5)]
    public class JMP : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            cpu.PC = cpu.CurrentAddress;

            return false;
        }
    }
}