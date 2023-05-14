namespace NES_emu.CPU.AddressingModes
{
    public interface IAddressingMode
    {
        //returns true if the addressing mode has crossed a page
        public abstract static bool Fetch(Cpu cpu);
    }
}
