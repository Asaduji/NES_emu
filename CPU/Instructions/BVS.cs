using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;

namespace NES_emu.CPU.Instructions
{
    [Instruction(0x70, AddressingMode.REL, 2, true)]
    public class BVS : IInstructionHandler
    {
        public static bool Execute(Cpu cpu)
        {
            if (cpu.GetFlag(Flag.V))
            {
                cpu.PC = cpu.CurrentAddress;

                ++cpu.Cycles;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
