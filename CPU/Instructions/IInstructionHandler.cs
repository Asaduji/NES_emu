namespace NES_emu.CPU.Instructions
{
    public interface IInstructionHandler
    {
        public abstract static bool Execute(Cpu cpu);
    }
}
