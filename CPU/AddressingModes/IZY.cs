namespace NES_emu.CPU.AddressingModes
{
    public class IZY : IAddressingMode
    {
        public static bool Fetch(Cpu cpu)
        {
            byte indirectZero = (byte)(cpu.ReadNext() + cpu.Y);
            byte lowByte = cpu.Read(indirectZero); //it's the same as doing 0x00 | indirectZero
            byte highByte = cpu.Read((byte)(indirectZero + 1)); //it's the same as doing 0x00 | (byte)(indirectZero + 1)

            cpu.CurrentAddress = (ushort)(highByte << 8 | lowByte);

            //if low byte was at the end of the page, a page boundary crossing happens and an extra cycle is needed
            return indirectZero == 0xFF;
        }
    }
}
