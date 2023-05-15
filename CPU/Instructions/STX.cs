using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0x86, AddressingMode.ZP, 3)]
    [Instruction(0x96, AddressingMode.ZPY, 4)]
    [Instruction(0x8E, AddressingMode.ABS, 4)]
    public class STX : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            cpu.Write(cpu.CurrentAddress, cpu.X);

            return false;
        }
    }
}
