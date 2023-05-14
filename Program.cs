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

            cpu.Reset();

            cpu.A = 0x62;

            for (var i = 0; i < 8; i++)
            {
                cpu.Clock();
            }
            

            Console.WriteLine($"{cpu.A:X2}, {(byte)(0x62 & 0x02):X2}");
        }
    }
}