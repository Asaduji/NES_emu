using NES_emu.BUS;
using NES_emu.CPU.AddressingModes;
using NES_emu.CPU.Attributes;
using NES_emu.CPU.Instructions;
using System.Reflection;

namespace NES_emu.CPU
{
    public class Cpu
    {
        //Registers       
        public byte A { get; set; }
        public byte X { get; set; }
        public byte Y { get; set; }
        public ushort PC { get; set; }
        public byte S { get; set; } = 0xFD;
        public byte P { get; set; }

        private readonly Bus _bus;
        private readonly Func<Cpu, bool>[] _addressingModes;
        private readonly Instruction[] _opcodeTable = Enumerable.Range(0, 256).Select(i => new Instruction((byte)i, AddressingMode.IMP, 1)).ToArray();

        //helpers
        public ushort CurrentAddress { get; set; }
        public byte CurrentOpcode { get; set; }
        public AddressingMode CurrentAddressingMode { get; set; }
        public int Cycles { get; set; }

        public Cpu(Bus bus)
        {
            var addressingModeCount = Enum.GetValues(typeof(AddressingMode)).Length;
            _addressingModes = new Func<Cpu, bool>[addressingModeCount];

            //add addressing modes to the array
            _addressingModes[(int)AddressingMode.IMP] = IMP.Fetch;
            _addressingModes[(int)AddressingMode.IMM] = IMM.Fetch;
            _addressingModes[(int)AddressingMode.ZP] = ZP.Fetch;
            _addressingModes[(int)AddressingMode.ZPX] = ZPX.Fetch;
            _addressingModes[(int)AddressingMode.ZPY] = ZPY.Fetch;
            _addressingModes[(int)AddressingMode.IZX] = IZX.Fetch;
            _addressingModes[(int)AddressingMode.IZY] = IZY.Fetch;
            _addressingModes[(int)AddressingMode.ABS] = ABS.Fetch;
            _addressingModes[(int)AddressingMode.ABX] = ABX.Fetch;
            _addressingModes[(int)AddressingMode.ABY] = ABY.Fetch;
            _addressingModes[(int)AddressingMode.IND] = IND.Fetch;
            _addressingModes[(int)AddressingMode.REL] = REL.Fetch;
            _addressingModes[(int)AddressingMode.ACC] = ACC.Fetch;

            //set bus
            _bus = bus;

            //load instructions
            var instructions = Assembly.GetExecutingAssembly().GetTypes().Where(x => typeof(IInstructionHandler).IsAssignableFrom(x)).ToList();

            var count = 0;

            foreach (var type in instructions)
            {
                var attributes = type.GetCustomAttributes<Instruction>().ToList();

                if (attributes.Count < 1)
                {
                    continue;
                }

                var executeMethodInfo = type.GetMethod("Execute", BindingFlags.Static | BindingFlags.Public);

                if (executeMethodInfo is null)
                {
                    Console.WriteLine($"No Execute method for {type} !");
                    continue;
                }

                Action<Cpu> execute = (Action<Cpu>)Delegate.CreateDelegate(typeof(Action<Cpu>), executeMethodInfo);

                foreach (var instruction in attributes)
                {
                    instruction.Name = type.Name;
                    instruction.Execute = execute;
                    _opcodeTable[instruction.Opcode] = instruction;
                    ++count;
                }
            }

            Console.WriteLine($"Loaded {count} opcodes");
        }

        public bool GetFlag(Flag flag)
        {
            return (P & (byte)flag) != 0;
        }

        public void SetFlag(Flag flag, bool value)
        {
            if (value)
            {
                P |= (byte)flag;
            }
            else
            {
                P &= (byte)~flag;
            }
        }

        public byte Read(ushort address)
        {
            return _bus.Read(address);
        }

        public byte ReadNext()
        {
            return _bus.Read(PC++);
        }

        public void PushStack(byte value)
        {
            Write((ushort)(0x0100 + S), value);
            --S;
        }

        public byte PopStack()
        {
            ++S;
            return Read((ushort)(0x0100 + S));           
        }

        public void Write(ushort address, byte data)
        {
            _bus.Write(address, data);
        }

        public void Reset()
        {
            A = 0;
            X = 0;
            Y = 0;

            //stack is always at 0x01FD after reset since the cpu pushes 3 times to it
            S = 0xFD;

            P = 0;

            SetFlag(Flag.U, true);

            //Set pc as first instruction
            byte lowByte = Read(0xFFFC);
            byte highByte = Read(0xFFFD);

            PC = (ushort)(highByte << 8 | lowByte);

            CurrentAddress = 0;

            Cycles = 7;
        }

        public void Clock()
        {
            if (Cycles == 0)
            {
                CurrentOpcode = ReadNext();

                var instruction = _opcodeTable[CurrentOpcode];

                Cycles = instruction.Cycles;

                CurrentAddressingMode = instruction.AddressingMode;

                var pageChanged = _addressingModes[(int)instruction.AddressingMode](this);

                instruction.Execute(this);

                if (pageChanged && instruction.CycleOnPageChange)
                {
                    ++Cycles;
                }
                Console.WriteLine($"Executed: (0x{CurrentOpcode}) {instruction.Name}, remaining cycles: {Cycles}, PC: {PC}");
            }
            --Cycles;
        }
    }
}
