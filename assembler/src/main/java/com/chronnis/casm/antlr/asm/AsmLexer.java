// Generated from Asm.g by ANTLR 4.7.1

    package com.chronnis.casm.antlr.asm;

import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class AsmLexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.7.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		REG=1, REGREF=2, DIRECTIVE=3, KEYWORD=4, LABEL=5, ID=6, NUMBER=7, NUMPREFIX=8, 
		EOL=9, COMMENT=10, SEP=11, WS=12;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	public static final String[] ruleNames = {
		"REG", "REGREF", "DIRECTIVE", "KEYWORD", "LABEL", "ID", "NUMBER", "NUMPREFIX", 
		"EOL", "COMMENT", "SEP", "WS", "HEXCHAR", "NUM", "ALPHANUM", "ALPHA"
	};

	private static final String[] _LITERAL_NAMES = {
		null, null, null, "'d32'", null, null, null, null, null, null, null, "','"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, "REG", "REGREF", "DIRECTIVE", "KEYWORD", "LABEL", "ID", "NUMBER", 
		"NUMPREFIX", "EOL", "COMMENT", "SEP", "WS"
	};
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}


	public AsmLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "Asm.g"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\16\u00a7\b\1\4\2"+
		"\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4"+
		"\13\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\3\2\3"+
		"\2\3\2\3\2\5\2(\n\2\3\3\3\3\3\3\3\3\3\4\3\4\3\4\3\4\3\5\3\5\3\5\3\5\3"+
		"\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5"+
		"\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3"+
		"\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5"+
		"\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\5\5s\n\5\3\6\3\6\3\6\3\7\3\7\7\7z\n\7"+
		"\f\7\16\7}\13\7\3\b\5\b\u0080\n\b\3\b\6\b\u0083\n\b\r\b\16\b\u0084\3\t"+
		"\3\t\3\t\3\n\5\n\u008b\n\n\3\n\3\n\3\13\3\13\7\13\u0091\n\13\f\13\16\13"+
		"\u0094\13\13\3\13\3\13\3\f\3\f\3\f\3\f\3\r\3\r\3\r\3\r\3\16\3\16\3\17"+
		"\3\17\3\20\3\20\3\21\3\21\2\2\22\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23"+
		"\13\25\f\27\r\31\16\33\2\35\2\37\2!\2\3\2\13\3\2\63\63\3\2\62\67\4\2d"+
		"dzz\4\2\f\f\17\17\4\2\13\13\"\"\5\2\62;CHch\3\2\62;\6\2\62;C\\aac|\5\2"+
		"C\\aac|\2\u00be\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3"+
		"\2\2\2\2\r\3\2\2\2\2\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2"+
		"\2\27\3\2\2\2\2\31\3\2\2\2\3#\3\2\2\2\5)\3\2\2\2\7-\3\2\2\2\tr\3\2\2\2"+
		"\13t\3\2\2\2\rw\3\2\2\2\17\177\3\2\2\2\21\u0086\3\2\2\2\23\u008a\3\2\2"+
		"\2\25\u008e\3\2\2\2\27\u0097\3\2\2\2\31\u009b\3\2\2\2\33\u009f\3\2\2\2"+
		"\35\u00a1\3\2\2\2\37\u00a3\3\2\2\2!\u00a5\3\2\2\2#\'\7t\2\2$%\t\2\2\2"+
		"%(\t\3\2\2&(\5\35\17\2\'$\3\2\2\2\'&\3\2\2\2(\4\3\2\2\2)*\7]\2\2*+\5\3"+
		"\2\2+,\7_\2\2,\6\3\2\2\2-.\7f\2\2./\7\65\2\2/\60\7\64\2\2\60\b\3\2\2\2"+
		"\61\62\7o\2\2\62\63\7q\2\2\63\64\7x\2\2\64s\7n\2\2\65\66\7o\2\2\66\67"+
		"\7q\2\2\678\7x\2\28s\7j\2\29:\7o\2\2:;\7q\2\2;s\7x\2\2<=\7n\2\2=s\7f\2"+
		"\2>?\7u\2\2?@\7v\2\2@s\7t\2\2As\7l\2\2BC\7l\2\2Cs\7g\2\2DE\7l\2\2EF\7"+
		"p\2\2Fs\7g\2\2GH\7l\2\2Hs\7n\2\2IJ\7l\2\2Js\7i\2\2KL\7c\2\2LM\7f\2\2M"+
		"s\7f\2\2NO\7u\2\2OP\7w\2\2Ps\7d\2\2QR\7o\2\2RS\7w\2\2Ss\7n\2\2TU\7f\2"+
		"\2UV\7k\2\2Vs\7x\2\2WX\7t\2\2XY\7g\2\2Ys\7o\2\2Z[\7p\2\2[\\\7g\2\2\\s"+
		"\7i\2\2]^\7c\2\2^_\7p\2\2_s\7f\2\2`a\7q\2\2as\7t\2\2bc\7z\2\2cd\7q\2\2"+
		"ds\7t\2\2ef\7p\2\2fg\7q\2\2gs\7v\2\2hi\7n\2\2ij\7u\2\2js\7j\2\2kl\7t\2"+
		"\2lm\7u\2\2ms\7j\2\2no\7c\2\2op\7t\2\2pq\7u\2\2qs\7j\2\2r\61\3\2\2\2r"+
		"\65\3\2\2\2r9\3\2\2\2r<\3\2\2\2r>\3\2\2\2rA\3\2\2\2rB\3\2\2\2rD\3\2\2"+
		"\2rG\3\2\2\2rI\3\2\2\2rK\3\2\2\2rN\3\2\2\2rQ\3\2\2\2rT\3\2\2\2rW\3\2\2"+
		"\2rZ\3\2\2\2r]\3\2\2\2r`\3\2\2\2rb\3\2\2\2re\3\2\2\2rh\3\2\2\2rk\3\2\2"+
		"\2rn\3\2\2\2s\n\3\2\2\2tu\7\60\2\2uv\5\r\7\2v\f\3\2\2\2w{\5!\21\2xz\5"+
		"\37\20\2yx\3\2\2\2z}\3\2\2\2{y\3\2\2\2{|\3\2\2\2|\16\3\2\2\2}{\3\2\2\2"+
		"~\u0080\5\21\t\2\177~\3\2\2\2\177\u0080\3\2\2\2\u0080\u0082\3\2\2\2\u0081"+
		"\u0083\5\33\16\2\u0082\u0081\3\2\2\2\u0083\u0084\3\2\2\2\u0084\u0082\3"+
		"\2\2\2\u0084\u0085\3\2\2\2\u0085\20\3\2\2\2\u0086\u0087\7\62\2\2\u0087"+
		"\u0088\t\4\2\2\u0088\22\3\2\2\2\u0089\u008b\7\17\2\2\u008a\u0089\3\2\2"+
		"\2\u008a\u008b\3\2\2\2\u008b\u008c\3\2\2\2\u008c\u008d\7\f\2\2\u008d\24"+
		"\3\2\2\2\u008e\u0092\7=\2\2\u008f\u0091\n\5\2\2\u0090\u008f\3\2\2\2\u0091"+
		"\u0094\3\2\2\2\u0092\u0090\3\2\2\2\u0092\u0093\3\2\2\2\u0093\u0095\3\2"+
		"\2\2\u0094\u0092\3\2\2\2\u0095\u0096\b\13\2\2\u0096\26\3\2\2\2\u0097\u0098"+
		"\7.\2\2\u0098\u0099\3\2\2\2\u0099\u009a\b\f\2\2\u009a\30\3\2\2\2\u009b"+
		"\u009c\t\6\2\2\u009c\u009d\3\2\2\2\u009d\u009e\b\r\2\2\u009e\32\3\2\2"+
		"\2\u009f\u00a0\t\7\2\2\u00a0\34\3\2\2\2\u00a1\u00a2\t\b\2\2\u00a2\36\3"+
		"\2\2\2\u00a3\u00a4\t\t\2\2\u00a4 \3\2\2\2\u00a5\u00a6\t\n\2\2\u00a6\""+
		"\3\2\2\2\n\2\'r{\177\u0084\u008a\u0092\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}