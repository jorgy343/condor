package com.chronnis.casm.inst;

import com.chronnis.casm.ast.OpCode;

/**
 * Created by Dylan on 4/4/2018.
 */
public class InstructionMagic
{
    public static Instruction getInstruction(OpCode opcode) throws ClassNotFoundException, IllegalAccessException, InstantiationException
    {
        String className = InstructionMagic.class.getPackage().getName() + "." + opcode.name();
        return (Instruction) Class.forName(className).newInstance();
    }
}
