using System;
using System.Text.RegularExpressions;
using Casm.Antlr;

namespace Casm.Assembler.Base
{
    public static class InstructionContextExtensions
    {
        private static readonly Regex RegisterReferenceRegex = new Regex(@"\[[rR](1[0-5]|[0-9])\]", RegexOptions.Compiled);

        /// <summary>
        /// Extracts the immediate value from the first argument of the instruction.
        /// </summary>
        /// <param name="context">The context of the instruction.</param>
        /// <param name="type">If <see cref="ImmediateType.High"/> then the value is right shifted by 16 bits.</param>
        /// <returns>The immediate value.</returns>
        public static ImmediateOperand ExtractImmediate1(this CasmParser.InstructionContext context, ImmediateType type)
        {
            var immediateText = context.expressionList().expression(0).NUMBER().GetText().Replace("_", "");

            uint value;

            if (immediateText.Contains("0x") || immediateText.Contains("0X"))
            {
                value = Convert.ToUInt32(immediateText, 16);
            }
            else if (immediateText.Contains("0b") || immediateText.Contains("0B"))
            {
                value =  Convert.ToUInt32(immediateText, 2);
            }
            else
            {
                value = uint.Parse(immediateText);
            }

            if (type == ImmediateType.High)
            {
                value >>= 16;
            }

            return new ImmediateOperand(value);
        }

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

        /// <summary>
        /// Extracts the register index from the first argument of the instruction.
        /// </summary>
        /// <param name="context">The context of the instruction.</param>
        /// <returns>The register index which is between 0 and 15.</returns>
        public static RegisterOperand ExtractRegister1(this CasmParser.InstructionContext context)
        {
            var registerText = context.expressionList().expression(0).register().GetText();

            return new RegisterOperand(uint.Parse(registerText.Substring(1)));
        }

        /// <summary>
        /// Extracts the register index from the second argument of the instruction.
        /// </summary>
        /// <param name="context">The context of the instruction.</param>
        /// <returns>The register index which is between 0 and 15.</returns>
        public static RegisterOperand ExtractRegister2(this CasmParser.InstructionContext context)
        {
            var registerText = context.expressionList().expression(1).register().GetText();

            return new RegisterOperand(uint.Parse(registerText.Substring(1)));
        }

        /// <summary>
        /// Extracts the register index from the third argument of the instruction.
        /// </summary>
        /// <param name="context">The context of the instruction.</param>
        /// <returns>The register index which is between 0 and 15.</returns>
        public static RegisterOperand ExtractRegister3(this CasmParser.InstructionContext context)
        {
            var registerText = context.expressionList().expression(2).register().GetText();

            return new RegisterOperand(uint.Parse(registerText.Substring(1)));
        }

        public static RegisterOperand ExtractRegisterReference1(this CasmParser.InstructionContext context)
        {
            var registerText = context.expressionList().expression(0).registerReference().REGISTERREFERENCE().GetText();

            return new RegisterOperand(uint.Parse(RegisterReferenceRegex.Match(registerText).Groups[1].Value));
        }

        public static RegisterOperand ExtractRegisterReference2(this CasmParser.InstructionContext context)
        {
            var registerText = context.expressionList().expression(1).registerReference().REGISTERREFERENCE().GetText();

            return new RegisterOperand(uint.Parse(RegisterReferenceRegex.Match(registerText).Groups[1].Value));
        }
    }
}