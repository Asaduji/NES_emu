using NES_emu.CARTRIDGE.Mappers;

namespace NES_emu.CARDTIGE
{
    public class Cartridge
    {
        private byte[] _prgROM = Array.Empty<byte>();
        private byte[] _chrROM = Array.Empty<byte>();
        private IMapper _mapper = new Mapper_00(new byte[0x4000], new byte[0x4000]); //use 00 as default

        public void ReadRom(byte[] rom)
        {
            using var stream = new MemoryStream(rom);
            using var reader = new BinaryReader(stream);

            // HEADER

            //Skip "NES" header string
            stream.Position = 4;
            var prgRomSize = reader.ReadByte();
            var chrRomSize = reader.ReadByte();
            var flags6 = reader.ReadByte();
            var flags7 = reader.ReadByte();
            var flags8 = reader.ReadByte();
            var flags9 = reader.ReadByte();
            var flags10 = reader.ReadByte();

            //skip padding
            stream.Position += 5;

            //TRAINER

            //we don't care, skip if present
            if ((flags6 & (1 << 2)) != 0)
            {
                stream.Position += 512;
            }
            
            _prgROM = reader.ReadBytes(16384 * prgRomSize);
            _chrROM = reader.ReadBytes(8192 * chrRomSize);

            //get mapper

            var mapperNumber = (byte)((flags6 >> 4) | (flags7 & 0xF0));

            Console.WriteLine($"Got mapper number {mapperNumber}");

            if (mapperNumber == 0) 
            { 
                _mapper = new Mapper_00(_prgROM, _chrROM);
            }
        }

        public byte Read(ushort address)
        {
            return _mapper.Read(address);
        }

        public void Write(ushort address, byte value)
        {
            _mapper.Write(address, value);
        }
    }
}
