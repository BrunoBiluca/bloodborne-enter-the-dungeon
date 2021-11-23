using System.Collections.Generic;
using System.Linq;

public class HuntersChooseCardsTurn : ITurn
{

    public List<Hunter> hunters;

    public HuntersChooseCardsTurn(List<Hunter> hunters)
    {
        this.hunters = hunters;
    }

    public void Execute()
    {
        foreach(var hunter in hunters)
        {
            hunter.EnabledCardSelection();
        }
    }

    public bool IsTurnFinished()
    {
        foreach(var hunter in hunters)
        {
            if(hunter.CurrentCard.IsPresent)
                hunter.DisabledCardSelection();
        }

        return hunters.All(h => h.CurrentCard.IsPresent);
    }
}
