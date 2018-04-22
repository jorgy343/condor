namespace Casm.Emulator
{
    public static class Extensions
    {
        public static bool GetSign(this uint source) => (source & 0x8000_0000) != 0;
    }
}