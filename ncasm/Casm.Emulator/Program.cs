using static System.Console;

namespace Casm.Emulator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var context = new EmulatorContext();

            context.ExecuteNextInstruction();
            context.ExecuteNextInstruction();

            PrintRegisters(context.GetRegisterValues());
        }

        private static void PrintRegisters(uint[] registers)
        {
            for (var i = 0; i < 16; ++i)
            {
                WriteLine($"r{i} = 0x{registers[i]:x}");
            }
        }
    }
}