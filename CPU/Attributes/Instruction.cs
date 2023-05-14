using NES_emu.CPU.AddressingModes;

namespace NES_emu.CPU.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class Instruction : Attribute
    {
        public byte Opcode { get; set; }
        public AddressingMode AddressingMode { get; set; }
        public int Cycles { get; set; }
        public Action<Cpu> Execute { get; set; } = (cpu) => {
            Console.WriteLine($"Got unimplemented instruction! Opcode: {cpu.CurrentOpcode:X2}");
        };
        public bool CycleOnPageChange { get; set; }
        public string Name { get; set; } = "Unimplemented";
        public Instruction(byte opcode, AddressingMode addressingMode, int cycles, bool cycleOnPageChange = false)         
        {
            Opcode = opcode;
            AddressingMode = addressingMode;
            Cycles = cycles;
            CycleOnPageChange = cycleOnPageChange;
        }
    }
}
