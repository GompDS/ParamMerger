using System.Collections.Generic;
using ParamMerger.ViewModels;

namespace ParamMerger.Commands;

public class ParamIgnoreAllLocalCommand : CommandBase
{
    public ParamIgnoreAllLocalCommand(ParamViewModel param)
    {
        _param = param;
        _localUndoStack = new Stack<CommandBase>();
    }

    private ParamViewModel _param;
    private Stack<CommandBase> _localUndoStack;
    
    public override void Redo()
    {
        foreach (RowViewModel row in _param.Rows)
        {
            IgnoreLocalCommand command = new IgnoreLocalCommand(row);
            command.Redo();
            _localUndoStack.Push(command);
        }
    }

    public override void Undo()
    {
        foreach (CommandBase command in _localUndoStack)
        {
            command.Undo();
        }
        _localUndoStack.Clear();
    }
}