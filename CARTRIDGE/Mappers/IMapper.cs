using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NES_emu.CARTRIDGE.Mappers
{
    public interface IMapper
    {
        public byte Read(ushort address);
        public void Write(ushort address, byte value);
    }
}
