using NES_emu.CARDTIGE;
using NES_emu.PPU;

namespace NES_emu.BUS
{
    public class Bus
    {
        //NES ram is 2KB
        private readonly byte[] _ram = new byte[2048];

        private readonly Cartridge _cart;
        private readonly Ppu _ppu;

        public Bus(Cartridge cart, Ppu ppu) {
            _cart = cart;
            _ppu = ppu;
        }
        public byte Read(ushort address)
        {
            //ram range: $0000 - $07FF, 0x0800 size, mirrors up to 0x1FFF
            if (address >= 0x0000 && address <= 0x1FFF)
            {
                return _ram[address % 0x0800];
            }
            //ppu registers, range: $2000 - $2007, 0x08 size, mirrors up to 0x3FFF
            if (address >= 0x2000 && address <= 0x3FFF)
            {
                return _ppu.BusRead(address);
            }
            //cart
            else if (address >= 0x4020 && address <= 0xFFFF)
            {
                return _cart.BusRead(address);
            }
            
            return 0;
        }

        public void Write(ushort address, byte data)
        {
            //ram range: $0000 - $07FF, 0x0800 size, mirrors up to 0x1FFF
            if (address >= 0x0000 && address <= 0x1FFF)
            {
                _ram[address % 0x0800] = data;
            }
            //ppu registers, range: $2000 - $2007, 0x08 size, mirrors up to 0x3FFF
            if (address >= 0x2000 && address <= 0x3FFF)
            {
                _ppu.BusWrite(address, data);
            }
            //cart
            else if (address >= 0x4020 && address <= 0xFFFF)
            {
                _cart.BusWrite(address, data);
            }
        }
    }
}
