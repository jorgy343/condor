using System;

namespace Casm.Emulator
{
    public class EmulatorContext
    {
        private uint _pc;
        private uint _flags;

        private readonly uint[] _registers = new uint[16];

        private readonly uint[] _instructions =
        {
            0x00030075u,
            0x04250003u
        };

        private readonly uint[] _memory = new uint[16_777_216];

        private void SetZeroFlag(bool value) => _flags = (_flags & 0xFFFF_FFFE) | (value ? 1u : 0u);

        private void SetSignFlag(bool value) => _flags = (_flags & 0xFFFF_FFFD) | (value ? 1u : 0u);

        private void SetCarryFlag(bool value) => _flags = (_flags & 0xFFFF_FFFB) | (value ? 1u : 0u);

        private void SetOverflowFlag(bool value) => _flags = (_flags & 0xFFFF_FFF7) | (value ? 1u : 0u);

        public uint[] GetRegisterValues()
        {
            var registers = new uint[16];
            Array.Copy(_registers, registers, 16);

            return registers;
        }

        public void ExecuteNextInstruction()
        {
            var instruction = _instructions[_pc / 4];

            var functionSelect = (instruction >> 29) & 0b111;
            var dBusSelect = (instruction >> 26) & 0b111;
            var opcode = (instruction >> 20) & 0b111_111;
            var registerD = (instruction >> 16) & 0b1111;
            var registerB = (instruction >> 4) & 0b1111;
            var registerA = (instruction >> 0) & 0b1111;
            var immediate = (instruction >> 16) & 0xffff;

            var regD = _registers[registerD];
            var regB = _registers[registerB];
            var regA = _registers[registerA];

            if (functionSelect == 0b000)
            {
                switch (opcode)
                {
                    case 0b000_000: // movl
                        _registers[registerD] = (regD & 0xffff_0000) | (immediate & 0xffff);
                        break;

                    case 0b000_001: // movh
                        _registers[registerD] = (regD & 0x0000_ffff) | ((immediate & 0xffff) << 16);
                        break;

                    case 0b000_010: // mov
                        _registers[registerD] = _registers[registerA];
                        break;

                    case 0b000_011: // ldr
                        _registers[registerD] = _memory[registerB];
                        break;

                    case 0b000_100: // str
                        _memory[registerB] = _registers[registerA];
                        break;

                    case 0b000_101: // movf
                        _registers[registerD] = _flags;
                        break;
                }
            }
            else if (functionSelect == 0b001)
            {
                switch (opcode)
                {
                    case 0b000_000: // add
                        var result = regA + regB;
                        _registers[registerD] = result;

                        SetZeroFlag(result == 0);
                        SetSignFlag(result.GetSign());
                        SetCarryFlag(uint.MaxValue - regA < regB);
                        SetOverflowFlag(regA.GetSign() == regB.GetSign() != result.GetSign());

                        break;

                    case 0b000_001: // sub
                        _registers[registerD] = regA - regB;
                        break;

                    case 0b000_010: // mul
                        _registers[registerD] = (uint)((int)regA * (int)regB);
                        break;

                    case 0b000_011: // div
                        _registers[registerD] = (uint)((int)regA / (int)regB);
                        break;

                    case 0b000_100: // mod
                        _registers[registerD] = (uint)((int)regA % (int)regB);
                        break;

                    case 0b000_101: // neg
                        _registers[registerD] = (uint)-(int)regA;
                        break;

                    case 0b000_110: // and
                        _registers[registerD] = (uint)((int)regA % (int)regB);
                        break;

                    case 0b000_111: // or
                        _registers[registerD] = (uint)((int)regA % (int)regB);
                        break;

                    case 0b001_000: // xor
                        _registers[registerD] = (uint)((int)regA % (int)regB);
                        break;

                    case 0b001_001: // not
                        _registers[registerD] = (uint)~(int)regA;
                        break;

                    case 0b001_010: // lsh
                        _registers[registerD] = regA << (int)regB;
                        break;

                    case 0b001_011: // rsh
                        _registers[registerD] = regA >> (int)regB;
                        break;
                }
            }
            else if (functionSelect == 0b010)
            {

            }
            else
            {
                throw new InvalidOperationException("Invalid instruction.");
            }

            _pc += 4;
        }
    }
}