using System.Collections.Generic;

internal class ConfirmCardSelectionTurn : ITurn
{
    private List<Hunter> hunters;

    public ConfirmCardSelectionTurn(List<Hunter> hunters)
    {
        this.hunters = hunters;
    }

    public void Execute()
    {
        foreach(var hunter in hunters)
        {
            hunter.DisabledCardSelection();
        }
    }
}