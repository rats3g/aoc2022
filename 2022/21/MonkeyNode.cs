namespace AdventOfCode;

internal class MonkeyNode
{
    private readonly Dictionary<string, MonkeyNode> _nodes;

    private readonly Operation _operation;
    private readonly (string leftOperand, string rightOperand) _operands;

    private long? _value;

    public MonkeyNode(Dictionary<string, MonkeyNode> nodes, long value)
    {
        _nodes = nodes;
        _value = value;
    }

    public MonkeyNode(Dictionary<string, MonkeyNode> nodes, (string leftOperand, string rightOperand) operands, Operation operation)
    {
        _nodes = nodes;
        _operands = operands;
        _operation = operation;
    }

    public long GetValue()
    {
        if (_value != null) return _value.Value;

        var leftOperand = _nodes[_operands.leftOperand].GetValue();
        var rightOperand = _nodes[_operands.rightOperand].GetValue();

        // Each node is only used once so storing the value is useless.
        // In addition, for part two it is advantageous for nodes to always recompute.
        return _operation switch
        {
            Operation.Add => leftOperand + rightOperand,
            Operation.Subtract => leftOperand - rightOperand,
            Operation.Multiply => leftOperand * rightOperand,
            Operation.Divide => leftOperand / rightOperand
        };
    }

    public void SetValue(long value)
    {
        _value = value;
    }

    public (string leftOperand, string rightOperand) GetOperands() => _operands;

    public long InverseValue(string node, long value)
    {
        if (_value != null) return value;

        var leftValue = value;
        var rightValue = _operands.leftOperand == node
            ? _nodes[_operands.rightOperand].GetValue()
            : _nodes[_operands.leftOperand].GetValue();

        return _operation switch
        {
            Operation.Add => leftValue - rightValue,
            Operation.Subtract => leftValue + rightValue,
            Operation.Multiply => leftValue / rightValue,
            Operation.Divide => leftValue * rightValue
        };
    }
}