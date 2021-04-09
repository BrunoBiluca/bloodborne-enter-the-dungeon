using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledCardSelectionTurn : ITurn {

    public List<Hunter> hunters;

    public EnabledCardSelectionTurn(List<Hunter> hunters) {
        this.hunters = hunters;
    }

    public void Execute() {
        foreach(var hunter in hunters) {
            hunter.EnabledCardSelection();
        }
    }
}
