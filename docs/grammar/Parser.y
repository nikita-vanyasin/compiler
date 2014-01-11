%namespace PySharpGrammarCheck
%token
ID INTEGER_VALUE BOOL CLASS ELSE FALSE WHILE 
IF INT PRIVATE PUBLIC RETURN STATIC TRUE
COMMA ASSIGNMENT LEFT_PAREN RIGHT_PAREN
LEFT_BRACE RIGHT_BRACE LEFT_BRACKET
RIGHT_BRACKET PLUS MINUS MULTIPLICATION DIV MOD
NOT AND OR LINE_END STRING STRING_LITERAL
BLOCK_START BLOCK_END PASS DOT EOF
LT GT GTE LTE EQUAL NOT_EQUAL
%start PS_PROGRAM
%%

PS_PROGRAM : CLASS_DEF EOF
           ;

CLASS_DEF : CLASS ID CLASS_BODY
          ;

CLASS_BODY : BLOCK_START CLASS_DECLARATIONS BLOCK_END
           ;

CLASS_DECLARATIONS : FIELD_DECLARATION CLASS_DECLARATIONS
                   | METHOD_DECLARATION CLASS_DECLARATIONS
                   | /* eps */
                   ;

FIELD_DECLARATION : VISIBILITY_MODIFIER STATIC_MODIFIER TYPE_DEFINITION ID LINE_END
                  ;

VISIBILITY_MODIFIER : PUBLIC
                    | PRIVATE
                    ;

STATIC_MODIFIER : STATIC
                | /* eps */
                ;

TYPE_DEFINITION : BOOL
                | INT
                | INT LEFT_BRACKET INTEGER_VALUE_LIST RIGHT_BRACKET
                | STRING
                ;

METHOD_DECLARATION : METHOD_HEADER BLOCK_START STATEMENTS_BLOCK BLOCK_END
                   ;

METHOD_HEADER : VISIBILITY_MODIFIER STATIC_MODIFIER TYPE_DEFINITION ID METHOD_ARGS
              ;

METHOD_ARGS : LEFT_PAREN ARGUMENTS_DEFINITION RIGHT_PAREN
            | LEFT_PAREN RIGHT_PAREN
            ;

ARGUMENTS_DEFINITION : ARGUMENT_DEFINITION
                     | ARGUMENT_DEFINITION COMMA ARGUMENTS_DEFINITION
                     ;

ARGUMENT_DEFINITION : TYPE_DEFINITION ID
                    ;

STATEMENTS_BLOCK : STATEMENTS
                 | PASS LINE_END
                 ;
              
STATEMENTS : STATEMENT STATEMENTS_S
           ;

STATEMENTS_S : STATEMENTS
             | /* eps */
             ;

STATEMENT : FUNC_CALL LINE_END
          | IF_STATEMENT
          | WHILE_STATEMENT
          | ASSIGN_STATEMENT LINE_END
          | RETURN EXPRESSION LINE_END
          ;

ASSIGN_STATEMENT : ID ASSIGNMENT EXPRESSION
                 | ID ARRAY ASSIGNMENT EXPRESSION
                 | ID ASSIGNMENT ARRAY_INITIALIZER
                 ;

ARRAY_INITIALIZER : LEFT_BRACE INTEGER_VALUE_LIST RIGHT_BRACE ;

INTEGER_VALUE_LIST : INTEGER_VALUE 
                   | INTEGER_VALUE COMMA INTEGER_VALUE_LIST
                   ; 

IF_STATEMENT : IF_THEN_STATEMENT
             | IF_THEN_STATEMENT ELSE BLOCK_START STATEMENTS_BLOCK BLOCK_END
             ;

IF_THEN_STATEMENT : IF LEFT_PAREN OR_TEST RIGHT_PAREN BLOCK_START STATEMENTS_BLOCK BLOCK_END
                  ;

WHILE_STATEMENT : WHILE LEFT_PAREN OR_TEST RIGHT_PAREN BLOCK_START STATEMENTS_BLOCK BLOCK_END
                ;

EXPRESSION : TERM
           | ADD_EXPRESSION
           | SUB_EXPRESSION
           ;

EXPRESSION_LIST : EXPRESSION
                | EXPRESSION COMMA EXPRESSION_LIST
                ;        
                 
TERM : UNARY_EXPRESSION
     | MUL_EXPRESSION
     | DIV_EXPRESSION
     | MOD_EXPRESSION
     ;

MUL_EXPRESSION : UNARY_EXPRESSION MULTIPLICATION TERM
               ;

DIV_EXPRESSION : UNARY_EXPRESSION DIV TERM
               ;

MOD_EXPRESSION : UNARY_EXPRESSION MOD TERM
               ;

UNARY_EXPRESSION : MINUS SIMPLE_TERM
                 | SIMPLE_TERM
                 ;
                 

SIMPLE_TERM : LEFT_PAREN EXPRESSION RIGHT_PAREN
            | ID
            | ID ARRAY
            | INTEGER_VALUE
            | BOOL_VALUE
            | FUNC_CALL
            | STRING_LITERAL
            ;

ARRAY : LEFT_BRACKET EXPRESSION_LIST RIGHT_BRACKET
      ;
                 
ADD_EXPRESSION : EXPRESSION PLUS TERM
               ;

SUB_EXPRESSION : EXPRESSION MINUS TERM
               ;

FUNC_CALL : THIS_METHOD_CALL
          | EXTERNAL_METHOD_CALL
          ;

THIS_METHOD_CALL : ID CALL_ARGS
                 ;

EXTERNAL_METHOD_CALL : ID DOT ID CALL_ARGS
                     ;

CALL_ARGS : LEFT_PAREN CALL_ARGS_LIST RIGHT_PAREN
          | LEFT_PAREN RIGHT_PAREN
          ;

CALL_ARGS_LIST : EXPRESSION
               | EXPRESSION COMMA CALL_ARGS_LIST
               ;

BOOL_VALUE : TRUE
           | FALSE
           ;

OR_TEST : AND_TEST
        | AND_TEST OR OR_TEST
        ;

AND_TEST : NOT_TEST
         | NOT_TEST AND AND_TEST
         ;

NOT_TEST : NOT NOT_TEST
         | EXPRESSION
         | COMPARISON
         ;           


COMPARISON : SIMPLE_TERM LT SIMPLE_TERM
           | SIMPLE_TERM GT SIMPLE_TERM
           | SIMPLE_TERM LTE SIMPLE_TERM
           | SIMPLE_TERM GTE SIMPLE_TERM
           | SIMPLE_TERM EQUAL SIMPLE_TERM
           | SIMPLE_TERM NOT_EQUAL SIMPLE_TERM
           ;