using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0xC0, AddressingMode.IMM, 2)]
    [Instruction(0xC4, AddressingMode.ZP, 3)]
    [Instruction(0xCC, AddressingMode.ABS, 4)]
    public class CPY : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            byte data = cpu.Read(cpu.CurrentAddress);

            cpu.SetFlag(Flag.C, cpu.Y >= data);
            cpu.SetFlag(Flag.Z, cpu.Y == data);
            cpu.SetFlag(Flag.N, (data & (1 << 7)) != 0);

            return false;
        }
    }
}