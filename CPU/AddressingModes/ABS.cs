namespace NES_emu.CPU.AddressingModes
{
    public class ABS : IAddressingMode
    {
        public static bool Fetch(Cpu cpu)
        {
            byte lowByte = cpu.ReadNext();
            byte highByte = cpu.ReadNext();                 

            cpu.CurrentAddress = (ushort)(highByte << 8 | lowByte);

            return false;
        }
    }
}
