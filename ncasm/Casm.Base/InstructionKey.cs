using System;
using System.Collections.Generic;

namespace Casm.Base
{
    public class InstructionKey : IEquatable<InstructionKey>
    {
        public InstructionKey(string name, params OperandType[] operandTypes)
        {
            Name = name;
            OperandTypes = operandTypes;
        }

        public string Name { get; }
        public OperandType[] OperandTypes { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as InstructionKey);
        }

        public override int GetHashCode()
        {
            var hash = 0;

            foreach (var operandType in OperandTypes)
            {
                hash ^= operandType.GetHashCode() * 397;
            }

            hash ^= (Name?.GetHashCode() ?? 0) * 397;

            return hash;
        }

        public bool Equals(InstructionKey other)
        {
            if (!Name.Equals(other.Name))
            {
                return false;
            }

            if (OperandTypes.Length != other.OperandTypes.Length)
            {
                return false;
            }

            for (var i = 0; i < OperandTypes.Length; ++i)
            {
                if (!OperandTypes[i].Equals(other.OperandTypes[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator ==(InstructionKey key1, InstructionKey key2)
        {
            return EqualityComparer<InstructionKey>.Default.Equals(key1, key2);
        }

        public static bool operator !=(InstructionKey key1, InstructionKey key2)
        {
            return !(key1 == key2);
        }
    }
}