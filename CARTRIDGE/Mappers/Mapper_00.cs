namespace NES_emu.CARTRIDGE.Mappers
{
    public class Mapper_00 : IMapper
    {
        private readonly byte[] _prgRAM = new byte[1024 * 8];
        private readonly byte[] _rom;
        private readonly byte[] _chr;

        public Mapper_00(byte[] rom, byte[] chr)
        {
            _rom = rom;
            _chr = chr;
        }

        public byte BusRead(ushort address)
        {
            if (address >= 0x6000 && address <= 0x7FFF)
            {
                return _prgRAM[address % 0x6000];
            } 
            else if (address >= 0x8000 && address <= 0xBFFF)
            {
                return _rom[address % 0x8000];
            }
            else if (address >= 0xC000 && address <= 0xFFFF)
            {
                //if the rom is 16KB, this address range will just be a mirror
                return _rom[_rom.Length > 0x4000 ? (address % 0xC000) + 0x4000 : address % 0xC000];
            }
            
            return 0;
        }

        public void BusWrite(ushort address, byte value)
        {
            if (address >= 0x6000 && address <= 0x7FFF)
            {
                _prgRAM[address % 0x6000] = value;
            }
            else if (address >= 0x8000 && address <= 0xBFFF)
            {
                _rom[address % 0x8000] = value;
            }
            else if (address >= 0xC000 && address <= 0xFFFF)
            {
                //if the rom is 16KB, this address range will just be a mirror
                _rom[_rom.Length > 0x4000 ? (address % 0xC000) + 0x4000 : address % 0xC000] = value;
            }
        }

        public byte PpuRead(ushort address)
        {
            return _chr[address];
        }

        public void PpuWrite(ushort address, byte value)
        {

        }
    }
}
