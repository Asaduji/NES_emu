using NES_emu.BUS;
using NES_emu.CARDTIGE;
using NES_emu.CPU;
using NES_emu.PPU;
using System.Diagnostics;
using static SDL2.SDL;

namespace NES_emu.NES
{
    public class Nes : IDisposable
    {
        //Render loop
        const float TARGET_FPS = 60.0f;
        const float TARGET_FRAME_TIME = 1.0f / TARGET_FPS;
        private bool _running = true;
        private long _previousTime = Stopwatch.GetTimestamp();
        private readonly float _frequency = Stopwatch.Frequency;
        private float _accumulatedTime = 0.0f;
        private readonly Stopwatch _stopWatch = new();
        private readonly NesRenderer _renderer;

        //NES
        private readonly Cpu _cpu;
        private readonly Cartridge _cart;
        private readonly Ppu _ppu;
        private readonly Bus _bus;

        public Nes(byte[] rom, int scale = 1)
        {
            _cart = new();
            _cart.ReadRom(rom);
            _ppu = new(_cart);
            _bus = new(_cart, _ppu);
            _cpu = new(_bus);
            _renderer = new(scale);
        }

        public void Run()
        {
            _cpu.Reset();

            while (_running)
            {
                var currentTime = Stopwatch.GetTimestamp();

                // Calculate the time elapsed since the previous frame in seconds
                var elapsed = (currentTime - _previousTime) / _frequency;

                _accumulatedTime += elapsed;

                // Update the map instance with the elapsed time
                while (_accumulatedTime >= TARGET_FRAME_TIME)
                {
                    var delta = _stopWatch.Elapsed.TotalSeconds;
                    _stopWatch.Restart();
                    Update((float)delta);
                    _accumulatedTime -= TARGET_FRAME_TIME;
                }

                // Set the timestamp of the previous frame to the current frame
                _previousTime = currentTime;

                // Sleep for a short time to reduce CPU usage
                Thread.Sleep(1);
            }
        }

        public void Dispose()
        {
            _renderer.Dispose();

            GC.SuppressFinalize(this);
        }

        static bool decreasing = false;
        static int index = 0;

        private void Update(float delta)
        {
            //First, handle events
            while (SDL_PollEvent(out var e) != 0)
            {
                if (e.type == SDL_EventType.SDL_QUIT)
                {
                    _running = false;
                    return;
                }

                if (e.type == SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL_Keycode.SDLK_p)
                {
                    Console.WriteLine($"PC: {_cpu.PC:X4}");
                }
            }

            //Run emulation for 1 frame
            _cpu.Clock();
            while (_cpu.Cycles > 0)
            {
                _cpu.Clock();
            }

            //Render the current state
            _renderer.BeginDraw();

            //Clear screen
            for (var i = 0; i < 256; i++)
            {
                for (var j = 0; j < 240; j++)
                {
                    _renderer.SetPixel(i, j, 0, 0, 0);
                }
            }

            //Do small animation
            for (var i = index; i < 256; i++)
            {
                for (var j = index; j < 240; j++)
                {
                    _renderer.SetPixel(i, j, 255, 0, 0);
                }
            }

            if (decreasing)
            {
                --index;
            }
            else
            {
                ++index;
            }

            if (index >= 100)
            {
                decreasing = true;
            }
            else if (index <= 0)
            {
                decreasing = false;
            }

            _renderer.EndDraw();
            _renderer.RenderFrame();
        }
    }
}
