using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;
using NES_emu.CPU.Instructions;

namespace NES_emu.CPU.Opcodes
{
    [Instruction(0x00, AddressingMode.IMP, 7)]
    public class BRK : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            ++cpu.PC;

            cpu.PushStack((byte)cpu.PC);
            cpu.PushStack((byte)(cpu.PC >> 8));

            cpu.SetFlag(Flag.B, true);

            cpu.PushStack(cpu.P);


            byte highByte = cpu.Read(0xFFFF);
            byte lowByte = cpu.Read(0xFFFE);

            cpu.PC = (ushort)(highByte << 8 | lowByte);
            

            return false;
        }
    }
}
