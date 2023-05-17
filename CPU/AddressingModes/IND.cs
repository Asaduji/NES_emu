namespace NES_emu.CPU.AddressingModes
{
    public class IND : IAddressingMode
    {
        public static bool Fetch(Cpu cpu)
        {
            byte lowByte = cpu.ReadNext();
            byte highByte = cpu.ReadNext();            

            // 6502 bug, when the high byte of the target address is at the end of a page, the low byte
            // would be the first in the next page, but instead it just wraps around
            // and goes back to the start of the current page
            if (lowByte == 0xFF)
            {
                // Read the target address from the indirect address formed by the high and low bytes
                ushort indirect = (ushort)(highByte << 8 | lowByte);

                // Read the target address and set it
                cpu.CurrentAddress = (ushort)(cpu.Read((ushort)(indirect & 0xFF00)) << 8 | cpu.Read(indirect));
            }
            else
            {
                // Read the target address from the indirect address formed by the high and low bytes
                ushort indirect = (ushort)(highByte << 8 | lowByte);

                // Read the target address and set it
                cpu.CurrentAddress = (ushort)(cpu.Read((ushort)(indirect + 1)) << 8 | cpu.Read(indirect));
            }



            return false;
        }
    }
}
