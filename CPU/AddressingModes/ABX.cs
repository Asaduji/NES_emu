namespace NES_emu.CPU.AddressingModes
{
    public class ABX : IAddressingMode
    {
        public static bool Fetch(Cpu cpu)
        {                     
            byte lowByte = cpu.ReadNext();
            byte highByte = cpu.ReadNext();

            cpu.CurrentAddress = (ushort)((highByte << 8 | lowByte) + cpu.X);

            //if the high byte changed, we're on a new page and we need 1 extra cycle
            return highByte != (byte)(cpu.CurrentAddress >> 8);
        }
    }
}
