using NES_emu.BUS;
using NES_emu.CARDTIGE;
using NES_emu.CPU;
using NES_emu.NES;
using NES_emu.PPU;
using SDL2;
using static SDL2.SDL;

namespace NES_emu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            var rom = File.ReadAllBytes(Directory.GetCurrentDirectory() + @"\nestest.nes");

            var cart = new Cartridge();
            cart.ReadRom(rom);
            var ppu = new Ppu(cart);
            var bus = new Bus(cart, ppu);
            var cpu = new Cpu(bus);

            cpu.Reset();

            cpu.PC = 0xC000;
            using var renderer = new NesRenderer(4);
            renderer.RenderFrame();

            /*
            v

            // Example usage: Set a pixel at (100, 100) with RGB values (255, 0, 0)
            for (var i = 0; i < 255; i++)
            {
                for (var j = 0; j < 239; j++)
                {
                    renderer.SetPixel(i, j, 255, 0, 0);
                }
            }

            


            // Main loop
            bool quit = false;
            while (!quit)
            {
                // Render frame
                renderer.RenderFrame();
                // Handle events
                SDL.SDL_Event e;
                while (SDL.SDL_PollEvent(out e) != 0)
                {
                    if (e.type == SDL.SDL_EventType.SDL_QUIT)
                    {
                        quit = true;
                    }

                    if (e.type == SDL.SDL_EventType.SDL_KEYDOWN)
                    {
                        renderer.SetScale(2);
                        for (var i = 0; i < 255; i++)
                        {
                            for (var j = 0; j < 239; j++)
                            {
                                renderer.SetPixel(i, j, 255, 0, 0);
                            }
                        }
                    }
                }
            }
            */



            /*
             * 
             *             while (true)
            {
                for (var i = 0; i < 29833; i++)
                {
                    cpu.Clock();
                }
                Thread.Sleep(16);
            }
            
                        while (true)
            {
                for (var i = 0; i < 29833; i++)
                {
                    cpu.Clock();
                }
                Thread.Sleep(16);
            }
            */



            while (true)
            {
                while (SDL_PollEvent(out SDL_Event e) != 0)
                {
                    /*
                    for (var i = 0; i < 29833; i++)
                    {
                        cpu.Clock();
                    }
                    Thread.Sleep(16);

                    if (e.type == SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL_Keycode.SDLK_F1)
                    {
                        Console.WriteLine($"02: {bus.Read(0x02):X2}, 03: {bus.Read(0x03):X2}");

                    }
                    */

                    if (e.type == SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL_Keycode.SDLK_SPACE)
                    {
                        cpu.Clock();
                        while (cpu.Cycles > 0)
                        {
                            cpu.Clock();
                        }                     
                    }
                }
            }

        }
    }
}