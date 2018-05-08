using Casm.Base;
using Xunit;
// ReSharper disable ConvertToConstant.Local
// ReSharper disable ArgumentsStyleLiteral
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace Casm.Base.Tests
{
    public class SingleInstructionTests
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

        [Fact]
        public void Instruction_movl_imm_rd()
        {
            var input = @"movl 0x15a9,r11";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b000, 0b000, 0b000_000, registerD: 11, immediate: 0x15a9u), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_movh_imm_rd()
        {
            var input = @"movh 0x5942,r11";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b000, 0b000, 0b000_001, registerD: 11, immediate: 0x5942u), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_mov_ra_rd()
        {
            var input = @"mov r3,r11";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b000, 0b001, 0b000_010, registerD: 11, registerB: 0, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_ldr_rb_rd()
        {
            var input = @"ldr [r7],r11";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b000, 0b010, 0b000_011, registerD: 11, registerB: 7, registerA: 0), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_str_ra_rb()
        {
            var input = @"str r3,[r7]";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b000, 0b001, 0b000_100, registerD: 0, registerB: 7, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_add_ra_rb_rd()
        {
            var input = @"add r3,r7,r11";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b001, 0b011, 0b000_000, registerD: 11, registerB: 7, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_sub_ra_rb_rd()
        {
            var input = @"sub r3,r7,r11";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b001, 0b011, 0b000_001, registerD: 11, registerB: 7, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_mul_ra_rb_rd()
        {
            var input = @"mul r3,r7,r11";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b001, 0b011, 0b000_010, registerD: 11, registerB: 7, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_div_ra_rb_rd()
        {
            var input = @"div r3,r7,r11";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b001, 0b011, 0b000_011, registerD: 11, registerB: 7, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_rem_ra_rb_rd()
        {
            var input = @"rem r3,r7,r11";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b001, 0b011, 0b000_100, registerD: 11, registerB: 7, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_neg_ra_rd()
        {
            var input = @"neg r3,r11";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b001, 0b011, 0b000_101, registerD: 11, registerB: 0, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_and_ra_rb_rd()
        {
            var input = @"and r3,r7,r11";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b001, 0b011, 0b000_110, registerD: 11, registerB: 7, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_or_ra_rb_rd()
        {
            var input = @"or r3,r7,r11";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b001, 0b011, 0b000_111, registerD: 11, registerB: 7, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_xor_ra_rb_rd()
        {
            var input = @"xor r3,r7,r11";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b001, 0b011, 0b001_000, registerD: 11, registerB: 7, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_not_ra_rd()
        {
            var input = @"not r3,r11";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b001, 0b011, 0b001_001, registerD: 11, registerB: 0, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_lsh_ra_rb_rd()
        {
            var input = @"lsh r3,r7,r11";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b001, 0b011, 0b001_010, registerD: 11, registerB: 7, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_rsh_ra_rb_rd()
        {
            var input = @"rsh r3,r7,r11";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b001, 0b011, 0b001_011, registerD: 11, registerB: 7, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_arsh_ra_rb_rd()
        {
            var input = @"arsh r3,r7,r11";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b001, 0b011, 0b001_100, registerD: 11, registerB: 7, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_cmp_ra_rb()
        {
            var input = @"cmp r3,r7";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b001, 0b011, 0b001_101, registerD: 0, registerB: 7, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_test_ra_rb()
        {
            var input = @"test r3,r7";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b001, 0b011, 0b001_110, registerD: 0, registerB: 7, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_j_ra()
        {
            var input = @"j r3";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b010, 0b001, 0b000_000, registerD: 0, registerB: 0, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_je_ra()
        {
            var input = @"je r3";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b010, 0b001, 0b000_001, registerD: 0, registerB: 0, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_jne_ra()
        {
            var input = @"jne r3";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b010, 0b001, 0b000_010, registerD: 0, registerB: 0, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_jl_ra()
        {
            var input = @"jl r3";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b010, 0b001, 0b000_011, registerD: 0, registerB: 0, registerA: 3), instruction.CreateMachineCode());
        }

        [Fact]
        public void Instruction_jg_ra()
        {
            var input = @"jg r3";

            var compiler = new Compiler();
            var result = compiler.Compile(input);

            var instruction = (Instruction)result.Nodes[0];

            Assert.Equal(BuildMachineCode(0b010, 0b001, 0b000_100, registerD: 0, registerB: 0, registerA: 3), instruction.CreateMachineCode());
        }
    }
}