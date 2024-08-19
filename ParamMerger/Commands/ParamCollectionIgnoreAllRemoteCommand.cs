using System.Collections.Generic;
using ParamMerger.ViewModels;

namespace ParamMerger.Commands;

public class ParamCollectionIgnoreAllLocalCommand : CommandBase
{
    public ParamCollectionIgnoreAllLocalCommand(ParamCollectionViewModel paramCollection)
    {
        _paramCollection = paramCollection;
        _localUndoStack = new Stack<CommandBase>();
    }

    private ParamCollectionViewModel _paramCollection;
    private Stack<CommandBase> _localUndoStack;
    
    public override void Redo()
    {
        foreach (ParamViewModel param in _paramCollection.Params)
        {
            ParamIgnoreAllLocalCommand command = new ParamIgnoreAllLocalCommand(param);
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