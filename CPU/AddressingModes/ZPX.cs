namespace NES_emu.CPU.AddressingModes
{
    public class ZPX : IAddressingMode
    {
        public static bool Fetch(Cpu cpu)
        {
            byte highByte = 0x00;
            
            //ZPX wraps around, if we just cast to a byte we essentially ignore the extra bits
            byte lowByte = (byte)(cpu.ReadNext() + cpu.X);

            //no need to shift the high byte because it's 0
            cpu.CurrentAddress = (ushort)(highByte | lowByte & 0xFF);

            return false;
        }
    }
}
