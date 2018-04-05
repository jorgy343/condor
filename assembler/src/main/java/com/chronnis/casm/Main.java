package com.chronnis.casm;


import com.chronnis.casm.antlr.CasmLexer;
import com.chronnis.casm.antlr.CasmParser;
import com.chronnis.casm.visitors.LabelListener;
import com.chronnis.casm.visitors.TestListener;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.CharStreams;
import org.antlr.v4.runtime.CommonTokenStream;
import org.antlr.v4.runtime.tree.ParseTreeWalker;

import java.io.InputStream;

/**
 * Created by Dylan on 4/3/2018.
 */
public class Main
{
    public static void main(String[] args)
    {
        try
        {
            ClassLoader classloader = Thread.currentThread().getContextClassLoader();
            InputStream is = classloader.getResourceAsStream("tests/jump-test-01.casm");
            CharStream inputStream = CharStreams.fromStream(is);
            CasmLexer asmLexer = new CasmLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(asmLexer);
            CasmParser casmParser = new CasmParser(commonTokenStream);
            CasmParser.ProgContext tree = casmParser.prog();
            ParseTreeWalker walker = new ParseTreeWalker();
            LabelListener listener = new LabelListener();
            walker.walk(listener, tree);
            TestListener testListener = new TestListener(listener.getLabelMap());
            walker.walk(testListener, tree);
//            System.out.println(testListener.getInstructionList());
            testListener.printSerializedInstructionList();
//            AstVisitor v = new AstVisitor();
//            Node p = v.visit(tree);
//            System.out.println(p);
//            System.out.println(listener.getLabelMap());

//            AstCompiler comp = new AstCompiler(listener.getLabelMap());
//            comp.compile(p);

        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
