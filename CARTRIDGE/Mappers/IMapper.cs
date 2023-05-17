namespace NES_emu.CARTRIDGE.Mappers
{
    public interface IMapper
    {
        public byte BusRead(ushort address);
        public void BusWrite(ushort address, byte value);
        public byte PpuRead(ushort address);
        public void PpuWrite(ushort address, byte value);
    }
}
