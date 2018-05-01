using System;
using System.Text.RegularExpressions;
using Casm.Antlr;

namespace Casm.Assembler
{
    public static class InstructionContextExtensions
    {
        private static readonly Regex RegisterReferenceRegex = new Regex(@"\[[rR](1[0-5]|[0-9])\]", RegexOptions.Compiled);

        public static ImmediateOperand ExtractImmediate(this CasmParser.InstructionContext context, int operandPosition)
        {
            var immediateText = context.expressionList().expression(operandPosition).NUMBER().GetText().Replace("_", "");

            uint value;

            if (immediateText.Contains("0x") || immediateText.Contains("0X"))
            {
                value = Convert.ToUInt32(immediateText, 16);
            }
            else if (immediateText.Contains("0b") || immediateText.Contains("0B"))
            {
                value = Convert.ToUInt32(immediateText, 2);
            }
            else
            {
                value = uint.Parse(immediateText);
            }

            return new ImmediateOperand(value);
        }

        public static RegisterOperand ExtractRegister(this CasmParser.InstructionContext context, int operandPosition)
        {
            var registerText = context.expressionList().expression(operandPosition).register().GetText();

            return new RegisterOperand(uint.Parse(registerText.Substring(1)));
        }

        public static RegisterOperand ExtractRegisterReference(this CasmParser.InstructionContext context, int operandPosition)
        {
            var registerText = context.expressionList().expression(operandPosition).registerReference().REGISTERREFERENCE().GetText();

            return new RegisterOperand(uint.Parse(RegisterReferenceRegex.Match(registerText).Groups[1].Value));
        }
    }
}