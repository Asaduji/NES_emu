using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;
using NES_emu.CPU.Instructions;

namespace NES_emu.CPU.Opcodes
{
    [Instruction(0x40, AddressingMode.IMP, 6)]
    public class RTI : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            cpu.P = cpu.PopStack();

            byte lowByte = cpu.PopStack();
            byte highByte = cpu.PopStack();

            cpu.PC = (ushort)(highByte << 8 | lowByte);

            return false;
        }
    }
}
