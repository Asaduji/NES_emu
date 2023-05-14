using NES_emu.CPU.AddressingModes;

namespace NES_emu.CPU.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Instruction : Attribute
    {
        public byte Opcode { get; set; }
        public AddressingMode AddressingMode { get; set; }
        public int Cycles { get; set; }
        public Func<Cpu, bool> Execute { get; set; } = (cpu) => false;
        public Instruction(byte opcode, AddressingMode addressingMode, int cycles)         
        {
            Opcode = opcode;
            AddressingMode = addressingMode;
            Cycles = cycles;
        }
    }
}
