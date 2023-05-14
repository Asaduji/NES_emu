namespace NES_emu.CPU.AddressingModes
{
    public class REL : IAddressingMode
    {
        public static bool Fetch(Cpu cpu)
        {
            byte rel = cpu.ReadNext();

            cpu.CurrentAddress = (ushort)(cpu.PC + (sbyte)rel);

            return false;
        }
    }
}
