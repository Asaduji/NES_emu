namespace NES_emu.CPU.AddressingModes
{
    public class ABY : IAddressingMode
    {
        public static bool Fetch(Cpu cpu)
        {
            byte highByte = cpu.ReadNext();
            byte lowByte = cpu.ReadNext();

            cpu.CurrentAddress = (ushort)((highByte << 8 | lowByte) + cpu.Y);

            //if the high byte changed, we're on a new page and we need 1 extra cycle
            return highByte != (byte)(cpu.CurrentAddress >> 8);
        }
    }
}
