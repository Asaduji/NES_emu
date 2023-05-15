using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0x85, AddressingMode.ZP, 3)]
    [Instruction(0x95, AddressingMode.ZPX, 4)]
    [Instruction(0x8D, AddressingMode.ABS, 4)]
    [Instruction(0x9D, AddressingMode.ABX, 5)]
    [Instruction(0x99, AddressingMode.ABY, 5)]
    [Instruction(0x81, AddressingMode.IZX, 6)]
    [Instruction(0x91, AddressingMode.IZY, 6)]
    public class STA : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            cpu.Write(cpu.CurrentAddress, cpu.A);

            return false;
        }
    }
}
