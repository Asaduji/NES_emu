﻿using NES_emu.BUS;
using NES_emu.CARDTIGE;
using NES_emu.CPU;
using NES_emu.PPU;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Diagnostics;

namespace NES_emu.NES
{
    public class Nes : IDisposable
    {
        //Render loop
        const double TARGET_FPS = 60.0f;
        const double TARGET_FRAME_TIME = 1.0f / TARGET_FPS;
        private long _previousTime = Stopwatch.GetTimestamp();
        private readonly double _frequency = Stopwatch.Frequency;
        private double _accumulatedTime = 0.0f;
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

            while (!_renderer.Closed)
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

                _renderer.RenderFrame();

                // Set the timestamp of the previous frame to the current frame
                _previousTime = currentTime;

                //Thread.Sleep(1);
            }
        }

        public void Dispose()
        {
            _renderer.Dispose();

            GC.SuppressFinalize(this);
        }

        static float x = 0;
        static float y = 0;    
        

        private void Update(float delta)
        {
            //First, handle events
            _renderer.ProcessEvents();

            float speed = 250.0f;

            float distance = speed * delta;

            if (_renderer.KeyboardState.IsKeyDown(Keys.Up))
            {
                y = (y - distance + 240) % 240;
            }
            if (_renderer.KeyboardState.IsKeyDown(Keys.Down))
            {
                y = (y + distance) % 240;
            }
            if (_renderer.KeyboardState.IsKeyDown(Keys.Left))
            {
                x = (x - distance + 256) % 256;
            }
            if (_renderer.KeyboardState.IsKeyDown(Keys.Right))
            {
                x = (x + distance) % 256;
            }


            //Run emulation for 1 frame
            _cpu.Clock();
            while (_cpu.Cycles > 0)
            {
                _cpu.Clock();
            }

            //Clear screen
            
            for (var i = 0; i < 256; i++)
            {
                for (var j = 0; j < 240; j++)
                {
                    _renderer.SetPixel(i, j, 0, 0, 0);
                }
            }


            //Do small animation

            for (var i = 0; i < 10; i++)
            {
                _renderer.SetPixel((int)MathF.Round(x), (int)MathF.Round(y) + i, 255, 0, 0);
            }
        
        }
    }
}
