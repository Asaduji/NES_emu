using NES_emu.BUS;
using NES_emu.CARDTIGE;
using NES_emu.CPU;
using NES_emu.NES;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

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

            cpu.PC = 0xC000; //test


            //using var nes = new Nes(800, 600, "Nes");
            
            //nes.Run();
            


            /*
            while (true)
            {
                cpu.Clock();
                Console.ReadLine();
                while (cpu.Cycles > 0) 
                { 
                   cpu.Clock(); 
                }
            }
            */
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