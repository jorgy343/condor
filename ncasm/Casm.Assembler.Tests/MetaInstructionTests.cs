using Casm.Assembler;
using Xunit;
// ReSharper disable ConvertToConstant.Local
// ReSharper disable ArgumentsStyleLiteral
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace Casm.Assembler.Tests
{
    public class MetaInstructionTests
    {
        private static uint BuildMachineCode(uint functionSelect, uint dBusSelect, uint opcode, uint registerD, uint immediate)
        {
            return
                (functionSelect << 29)
                | (dBusSelect << 26)
                | (opcode << 20)
                | (registerD << 16)
                | (immediate << 0);
        }

        private static uint BuildMachineCode(uint functionSelect, uint dBusSelect, uint opcode, uint registerD, uint registerB, uint registerA)
        {
            return
                (functionSelect << 29)
                | (dBusSelect << 26)
                | (opcode << 20)
                | (registerD << 16)
                | (registerB << 4)
                | (registerA << 0);
        }

        //[Fact]
        //public void Instruction_movlh()
        //{
        //    var input = @"movlh 0x5942_15a9,r11";

        //    var compiler = new Compiler();
        //    var result = compiler.Compile(input);

        //    var instruction1 = (Instr)result.Nodes[0];
        //    var instruction2 = (Instr)result.Nodes[1];

        //    Assert.Equal(BuildMachineCode(0b000, 0b000, 0b000_000, registerD: 11, immediate: 0x15a9u), instruction1.CreateMachineCode());
        //    Assert.Equal(BuildMachineCode(0b000, 0b000, 0b000_001, registerD: 11, immediate: 0x5942u), instruction2.CreateMachineCode());
        //}

        [Fact]
        public void Instruction_push()
        {
            var input = @"push r3";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction1 = (Instruction)result.Nodes[0];
            var instruction2 = (Instruction)result.Nodes[1];
            var instruction3 = (Instruction)result.Nodes[2];

            Assert.Equal(BuildMachineCode(0b000, 0b000, 0b000_000, registerD: 14, immediate: 4), instruction1.CreateMachineCode());
            Assert.Equal(BuildMachineCode(0b001, 0b011, 0b000_001, registerD: 15, registerB: 14, registerA: 15), instruction2.CreateMachineCode());
            Assert.Equal(BuildMachineCode(0b000, 0b001, 0b000_100, registerD: 0, registerB: 15, registerA: 3), instruction3.CreateMachineCode());
        }
        [Fact]
        public void Instruction_pop()
        {
            var input = @"pop r3";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction1 = (Instruction)result.Nodes[0];
            var instruction2 = (Instruction)result.Nodes[1];
            var instruction3 = (Instruction)result.Nodes[2];

            Assert.Equal(BuildMachineCode(0b000, 0b000, 0b000_000, registerD: 14, immediate: 4), instruction1.CreateMachineCode());
            Assert.Equal(BuildMachineCode(0b000, 0b010, 0b000_011, registerD: 3, registerB: 15, registerA: 0), instruction2.CreateMachineCode());
            Assert.Equal(BuildMachineCode(0b001, 0b011, 0b000_000, registerD: 15, registerB: 14, registerA: 15), instruction3.CreateMachineCode());
        }

        [Fact]
        public void Instruction_j_l()
        {
            var input = @"
                mov r0,r0
                lbl: j lbl
                ";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction1 = (Instruction)result.Nodes[2];
            var instruction2 = (Instruction)result.Nodes[3];
            var instruction3 = (Instruction)result.Nodes[4];

            Assert.Equal(BuildMachineCode(0b000, 0b000, 0b000_000, registerD: 14, immediate: 4u), instruction1.CreateMachineCode());
            Assert.Equal(BuildMachineCode(0b000, 0b000, 0b000_001, registerD: 14, immediate: 0u), instruction2.CreateMachineCode());
            Assert.Equal(BuildMachineCode(0b010, 0b001, 0b000_000, registerD: 0, registerB: 0, registerA: 14), instruction3.CreateMachineCode());
        }

        [Fact]
        public void Instruction_je_l()
        {
            var input = @"
                mov r0,r0
                lbl: je lbl
                ";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction1 = (Instruction)result.Nodes[2];
            var instruction2 = (Instruction)result.Nodes[3];
            var instruction3 = (Instruction)result.Nodes[4];

            Assert.Equal(BuildMachineCode(0b000, 0b000, 0b000_000, registerD: 14, immediate: 4u), instruction1.CreateMachineCode());
            Assert.Equal(BuildMachineCode(0b000, 0b000, 0b000_001, registerD: 14, immediate: 0u), instruction2.CreateMachineCode());
            Assert.Equal(BuildMachineCode(0b010, 0b001, 0b000_001, registerD: 0, registerB: 0, registerA: 14), instruction3.CreateMachineCode());
        }

        [Fact]
        public void Instruction_jne_l()
        {
            var input = @"
                mov r0,r0
                lbl: jne lbl
                ";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction1 = (Instruction)result.Nodes[2];
            var instruction2 = (Instruction)result.Nodes[3];
            var instruction3 = (Instruction)result.Nodes[4];

            Assert.Equal(BuildMachineCode(0b000, 0b000, 0b000_000, registerD: 14, immediate: 4u), instruction1.CreateMachineCode());
            Assert.Equal(BuildMachineCode(0b000, 0b000, 0b000_001, registerD: 14, immediate: 0u), instruction2.CreateMachineCode());
            Assert.Equal(BuildMachineCode(0b010, 0b001, 0b000_010, registerD: 0, registerB: 0, registerA: 14), instruction3.CreateMachineCode());
        }

        [Fact]
        public void Instruction_jl_l()
        {
            var input = @"
                mov r0,r0
                lbl: jl lbl
                ";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction1 = (Instruction)result.Nodes[2];
            var instruction2 = (Instruction)result.Nodes[3];
            var instruction3 = (Instruction)result.Nodes[4];

            Assert.Equal(BuildMachineCode(0b000, 0b000, 0b000_000, registerD: 14, immediate: 4u), instruction1.CreateMachineCode());
            Assert.Equal(BuildMachineCode(0b000, 0b000, 0b000_001, registerD: 14, immediate: 0u), instruction2.CreateMachineCode());
            Assert.Equal(BuildMachineCode(0b010, 0b001, 0b000_011, registerD: 0, registerB: 0, registerA: 14), instruction3.CreateMachineCode());
        }

        [Fact]
        public void Instruction_jg_l()
        {
            var input = @"
                mov r0,r0
                lbl: jg lbl
                ";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction1 = (Instruction)result.Nodes[2];
            var instruction2 = (Instruction)result.Nodes[3];
            var instruction3 = (Instruction)result.Nodes[4];

            Assert.Equal(BuildMachineCode(0b000, 0b000, 0b000_000, registerD: 14, immediate: 4u), instruction1.CreateMachineCode());
            Assert.Equal(BuildMachineCode(0b000, 0b000, 0b000_001, registerD: 14, immediate: 0u), instruction2.CreateMachineCode());
            Assert.Equal(BuildMachineCode(0b010, 0b001, 0b000_100, registerD: 0, registerB: 0, registerA: 14), instruction3.CreateMachineCode());
        }
    }
}