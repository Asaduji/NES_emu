using NES_emu.BUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NES_emu.PPU
{
    public class Ppu
    {
        private readonly Bus _bus;
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

        public Ppu(Bus bus)
        {
            _bus = bus;
        }

        public void BusWrite(ushort address, byte data)
        {
            _bus.Write(address, data);
        }

        public void BusRead(ushort address, byte data)
        {
            _bus.Write(address, data);
        }

        public void PpuWrite(ushort address, byte data)
        {
            _bus.Write(address, data);
        }

        public void PpuRead(ushort address, byte data)
        {
            _bus.Write(address, data);
        }
    }
}
