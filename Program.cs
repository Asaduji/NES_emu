using NES_emu.NES;

namespace NES_emu
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var rom = File.ReadAllBytes(Directory.GetCurrentDirectory() + @"\nestest.nes");

            var nes = new Nes(rom, 5);

            nes.Run();
        }
    }
}