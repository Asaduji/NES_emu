using NES_emu.CPU.AddressingModes;

namespace NES_emu.CPU.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class Instruction : Attribute
    {
        public byte Opcode { get; set; }
        public AddressingMode AddressingMode { get; set; }
        public int Cycles { get; set; }
        public bool ExtraCycleOnPageCross { get; set; }
        public Func<Cpu, bool> Execute { get; set; } = (cpu) => {
            Console.WriteLine($"Got unimplemented instruction! Opcode: {cpu.CurrentOpcode:X2}, PC: {cpu.PC:X4}");
            return false;
        };
        public string Name { get; set; } = "Unimplemented";
        public Instruction(byte opcode, AddressingMode addressingMode, int cycles, bool extraCycleOnPageCross = false)         
        {
            Opcode = opcode;
            AddressingMode = addressingMode;
            Cycles = cycles;
            ExtraCycleOnPageCross = extraCycleOnPageCross;
        }
    }
}
