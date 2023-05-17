using NES_emu.CARDTIGE;

namespace NES_emu.PPU
{
    public class Ppu
    {
        private readonly Cartridge _cart;
        //registers
        public byte PPUCTRL { get; set; }
        public byte PPUMASK { get; set; }
        public byte PPUSTATUS { get; set; }
        public byte OAMADDR { get; set; }
        public byte OAMDATA { get; set; }
        public byte PPUSCROLL { get; set; }
        public byte PPUADDR { get; set; }
        public byte PPUDATA { get; set; }
        public byte OAMDMA { get; set; }

        public Ppu(Cartridge cart)
        {
            _cart = cart;
        }

        public void BusWrite(ushort address, byte data)
        {
            if (address >= 0x2000 || address <= 0x3FFF)
            {

            }
        }

        public byte BusRead(ushort address)
        {
            return 0;
        }

    }
}
