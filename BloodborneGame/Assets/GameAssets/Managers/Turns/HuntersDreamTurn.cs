using System.Collections.Generic;

public class HuntersDreamTurn : ITurn
{
    private readonly GameManager gameManager;
    private readonly HuntersDreamUI huntersDreamCanvas;

    private bool huntersDreamEnded;
    private List<Hunter> huntersOnDream;

    public HuntersDreamTurn(GameManager gameManager, HuntersDreamUI huntersDreamCanvas)
    {
        this.gameManager = gameManager;
        this.huntersDreamCanvas = huntersDreamCanvas;
    }

    public void Execute()
    {
        huntersDreamEnded = false;

        huntersOnDream = new List<Hunter>();

        foreach(var hunter in gameManager.GetAliveHunters())
        {
            if(!hunter.CurrentCard.IsPresentAndGet(out HunterCardSO card))
                continue;

            if(card.effect is HuntersDreamEffect)
            {
                huntersOnDream.Add(hunter);
            }
        }

        if(huntersOnDream.Count == 0)
        {
            HuntersDreamEnded();
            return;
        }

        foreach(var hunter in huntersOnDream)
        {
            hunter.HealthSystem.HealFull();
            hunter.RecoverCards();
        }

        huntersDreamCanvas.OnFinished -= HuntersDreamEnded;
        huntersDreamCanvas.OnFinished += HuntersDreamEnded;

        huntersDreamCanvas.Show(huntersOnDream);
    }

    private void HuntersDreamEnded()
    {
        foreach (var hunter in huntersOnDream)
        {
            hunter.DiscartHunterDreamCard();
        }

        huntersDreamEnded = true;
    }

    public bool IsTurnFinished()
    {
        return huntersDreamEnded;
    }
}
