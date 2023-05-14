using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NES_emu.BUS
{
    public class Bus
    {
        //NES ram is 2KB
        private readonly byte[] _ram = new byte[2048];
        public Bus() { }
        public byte Read(ushort address)
        {
            //ram range: $0000 - $07FF
            if (address <= 0x07FF)
            {
                return _ram[address];
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
