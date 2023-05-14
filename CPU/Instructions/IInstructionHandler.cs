namespace NES_emu.CPU.Instructions
{
    public interface IInstructionHandler
    {
        public abstract static void Execute(Cpu cpu);
    }
}
