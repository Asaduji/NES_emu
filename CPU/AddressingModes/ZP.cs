namespace NES_emu.CPU.AddressingModes
{
    public class ZP : IAddressingMode
    {
        public static bool Fetch(Cpu cpu)
        {
            byte highByte = 0x00;            
            byte lowByte = cpu.ReadNext();

            //no need to shift the high byte because it's 0
            cpu.CurrentAddress = (ushort)(highByte | lowByte);

            return false;
        }
    }
}
