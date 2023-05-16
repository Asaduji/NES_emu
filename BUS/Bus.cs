using NES_emu.CARDTIGE;

namespace NES_emu.BUS
{
    public class Bus
    {
        //NES ram is 2KB
        private readonly byte[] _ram = new byte[2048];

        private Cartridge _cart { get; set; }

        public Bus() {

        }
        public byte Read(ushort address)
        {
            //ram range: $0000 - $07FF
            if (address <= 0x07FF)
            {
                return _ram[address];
            }
            //ram mirrors
            else if (address >= 0x0800 && address <= 0x0FFF)
            {
                return _ram[address % 0x0800];
            }
            else if (address >= 0x1000 && address <= 0x17FF)
            {
                return _ram[address % 0x0800];
            }
            else if (address >= 0x1800 && address <= 0x1FFF)
            {
                return _ram[address % 0x0800];
            }
            //cart
            else if (address >= 0x4020 && address <= 0xFFFF)
            {
                return _cart.Read(address);
            }
            else if (address >= 0x2000 && address <= 0x3FFF)
            {
                return 0x00;//0xFF;
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
            //ram mirrors
            else if (address >= 0x0800 && address <= 0x0FFF)
            {
                _ram[address % 0x0800] = data;
            }
            else if (address >= 0x1000 && address <= 0x17FF)
            {
                _ram[address % 0x0800] = data;
            }
            else if (address >= 0x1800 && address <= 0x1FFF)
            {
                 _ram[address % 0x0800] = data;
            }
            //cart
            else if (address >= 0x4020 && address <= 0xFFFF)
            {
                _cart.Write(address, data);
            }
        }

        public void SetCartidge(Cartridge cart)
        {
            _cart = cart;
        }
    }
}
