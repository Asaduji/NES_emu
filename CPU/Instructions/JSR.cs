using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0x20, AddressingMode.ABS, 6)]
    public class JSR : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            --cpu.PC;
            cpu.PushStack((byte)(cpu.PC >> 8));
            cpu.PushStack((byte)cpu.PC);
            cpu.PC = cpu.CurrentAddress;

            return false;
        }
    }
}