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
            address = (ushort)((address % 0x08) + 0x2000);

            if (address == 0x2000)
            {
                PPUCTRL = data;
            }
            else if (address == 0x2001)
            {
                PPUMASK = data;
            }
            //read only
            else if (address == 0x2002)
            {
                return;
            }
            else if (address == 0x2003)
            {
                OAMADDR = data;
            }
            else if (address == 0x2004)
            {
                OAMDATA = data;
            }
            else if (address == 0x2005)
            {
                PPUSCROLL = data;
            }
            //write only
            else if (address == 0x2006)
            {
                return;
            }
            else if (address == 0x2007)
            {
                
            }
            //write only
            else if (address == 0x4014)
            {
                return;
            }
        }

        public byte BusRead(ushort address)
        {
            address = (ushort)((address % 0x08) + 0x2000);

            //write only
            if (address == 0x2000)
            {
                return 0;
            } 
            //write only
            else if (address == 0x2001)
            {
                return 0;
            }
            else if (address == 0x2002)
            {
                return PPUSTATUS;
            }
            //write only
            else if (address == 0x2003)
            {
                return 0;
            }
            else if (address == 0x2004)
            {
                return OAMDATA;
            }
            //write only
            else if (address == 0x2005)
            {
                return 0;
            }
            //write only
            else if (address == 0x2006)
            {
                return 0;
            }
            else if (address == 0x2007)
            {
                return PPUDATA;
            }
            //write only
            else if (address == 0x4014)
            {
                return 0;
            }

            return 0;
        }

    }
}
