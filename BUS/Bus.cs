namespace NES_emu.BUS
{
    public class Bus
    {
        //NES ram is 2KB
        private readonly byte[] _ram = new byte[2048];
        public Bus() {
            _ram[0] = 0x25;
            _ram[1] = 0x70;
            _ram[0x70] = 0x02;

        }
        public byte Read(ushort address)
        {
            //ram range: $0000 - $07FF
            if (address <= 0x07FF)
            {
                return _ram[address];
            }

            //for testing purposes, hardcode the entry point to be the start of the ram
            else if (address == 0xFFFC)
            {
                return 0x00;
            }
            else if (address == 0xFFFD)
            {
                return 0x00;
            }
            return 0;
        }

        public void Write(ushort address, byte data)
        {
            //ram range: $0000 - $07FF
            if (address <= 0x07FF)
            {
               _ram[address] = data;
            }
        }
    }
}
