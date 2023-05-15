using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;
using NES_emu.CPU.Instructions;

namespace NES_emu.CPU.Opcodes
{
    [Instruction(0x60, AddressingMode.IMP, 6)]
    public class RTS : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            byte lowByte = cpu.PopStack();
            byte highByte = cpu.PopStack();         

            cpu.PC = (ushort)(highByte << 8 | lowByte);

            ++cpu.PC;

            return false;
        }
    }
}
