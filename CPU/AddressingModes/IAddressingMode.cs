namespace NES_emu.CPU.AddressingModes
{
    public interface IAddressingMode
    {
        //returns true if the addressing mode takes 1 more cycle (only used in structions that specifically specify this)
        public abstract static bool Fetch(Cpu cpu);
    }
}
