using ParamMerger.ViewModels;

namespace ParamMerger.Commands;

public class IgnoreLocalCommand : CommandBase
{
    public IgnoreLocalCommand(RowViewModel row)
    {
        _row = row;
        if (_row.Remote != null) _isRemoteIgnored = _row.Remote.IsIgnored;
    }

    private RowViewModel _row;
    private bool _isRemoteIgnored;
    
    public override void Redo()
    {
        _row.IsIgnored = true;
        if (_row.Remote == null) return;
        if (_isRemoteIgnored)
        {
            _row.Remote.IsIgnored = false;
        }
    }
    
    public override void Undo()
    {
        _row.IsIgnored = false;
        if (_row.Remote == null) return;
        if (_isRemoteIgnored)
        {
            _row.Remote.IsIgnored = true;
        }
    }
}