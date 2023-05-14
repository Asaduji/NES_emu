namespace NES_emu.CPU.AddressingModes
{
    public class IMM : IAddressingMode
    {
        public static bool Fetch(Cpu cpu)
        {
            cpu.CurrentAddress = cpu.PC++;
            return false;
        }
    }
}
