using NES_emu.BUS;
using NES_emu.CARDTIGE;
using NES_emu.CPU;

namespace NES_emu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var bus = new Bus();
            var cpu = new Cpu(bus);

            var rom = File.ReadAllBytes(Directory.GetCurrentDirectory() + @"\nestest.nes");

            var cart = new Cartridge();
            cart.ReadRom(rom);

            bus.SetCartidge(cart);

            cpu.Reset();

            while (true)
            {
                for (var i = 0; i < 29833; i++)
                {
                    cpu.Clock();
                }
                Thread.Sleep(16);
            }
        }
    }
}