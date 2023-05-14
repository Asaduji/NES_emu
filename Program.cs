using NES_emu.BUS;
using NES_emu.CPU;

namespace NES_emu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var bus = new Bus();
            var cpu = new Cpu(bus);



            var value = (byte)(1);
            Console.WriteLine($"{value:X2}");
        }
    }
}