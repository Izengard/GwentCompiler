namespace ClassLibrary.SyntaxAnalysis.Visitor;

public class SemanticAnalayzer : IVisitor
{
    public void Visit(Variable variable)
    {
        throw new NotImplementedException();
    }

    public void Visit(UnaryExpression unaryExpression)
    {
        bool operandSemanticCheck = Operand.CheckSemantics(compilingErrors);

        if (Operator.Type == TokenType.Minus && Operand.Type != ExpressionType.Arithmetic)
        {
            Error semanticError = new Error(Operator.Line, Operator.Col, ErrorType.SemanticError, "Invalid Operand Type, Operand must evaluate to Number");
            compilingErrors.Add(semanticError);
            return false;
        }
        else if (Operator.Type == TokenType.Not && Operand.Type != ExpressionType.Boolean)
        {
            Error semanticError = new Error(Operator.Line, Operator.Col, ErrorType.SemanticError, "Invalid Operand Type, Operand must evaluate to Bool");
            compilingErrors.Add(semanticError);
            return false;
        }

        return operandSemanticCheck;
    }

    public void Visit(BinaryExpression binaryExpression)
    {
         bool operandsSemanticCheck = Left.CheckSemantics(compilingErrors) && Right.CheckSemantics(compilingErrors);

        bool thisSemanticCheck = true;
        switch (Operator.Type)
        {
            case TokenType.PLUS:
            case TokenType.MINUS:
            case TokenType.MUL:
            case TokenType.DIV:
            case TokenType.MOD:
            case TokenType.POWER:
            case TokenType.GREATER:
            case TokenType.GREATER_EQUAL:
            case TokenType.LESS:
            case TokenType.LESS_EQUAL:
                if (Left.Type != ExpressionType.Arithmetic || Right.Type != ExpressionType.Arithmetic)
                {
                    Error semanticError = new Error(Operator.Line, Operator.Col, ErrorType.SemanticError,
                                        "Invalid Operands Type, Both Operand must evaluate to Number");
                    compilingErrors.Add(semanticError);
                    thisSemanticCheck = false;
                }
                break;

            case TokenType.Concat:
            case TokenType.WhitespaceConcat:
                if (Left.Type != ExpressionType.String || Right.Type != ExpressionType.String)
                {
                    Error semanticError = new Error(Operator.Line, Operator.Col, ErrorType.SemanticError,
                                        "Invalid Operands Type, Both Operand must evaluate to String");
                    compilingErrors.Add(semanticError);
                    thisSemanticCheck = false;
                }
                break;
            case TokenType.And:
            case TokenType.Or:
                if (Left.Type != ExpressionType.Boolean || Right.Type != ExpressionType.Boolean)
                {
                    Error semanticError = new Error(Operator.Line, Operator.Col, ErrorType.SemanticError,
                                        "Invalid Operands Type, Both Operand must evaluate to Boolean");
                    compilingErrors.Add(semanticError);
                    thisSemanticCheck = false;
                }
                break;
            default: // If not any of above operators
                System.Console.WriteLine("Unstated operator");
                thisSemanticCheck = false;
                break;
        }

        return operandsSemanticCheck && thisSemanticCheck;
    }

    public void Visit(AssignExpression assignExpression)
    {
        throw new NotImplementedException();
    }

    public void Visit(Literal variable)
    {
        throw new NotImplementedException();
    }

    public void Visit(PostfixUnaryExpression unaryExpression)
    {
        throw new NotImplementedException();
    }

}