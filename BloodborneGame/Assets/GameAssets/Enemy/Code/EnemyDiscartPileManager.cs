using Assets.UnityFoundation.Code.Common;
using System.Collections.Generic;
using TMPro;
using Assets.UnityFoundation.Code;

public class EnemyDiscartPileManager : Singleton<EnemyDiscartPileManager>
{
    private List<EnemyBase> enemies;

    private TextMeshProUGUI discartedCardsCountText;

    protected override void OnAwake()
    {
        enemies = new List<EnemyBase>();

        discartedCardsCountText = transform.FindComponent<TextMeshProUGUI>(
            "enemy_deck_canvas.discart_pile.card_number.text"
        );
        discartedCardsCountText.text = "0";
    }

    public void Discart(EnemyBase enemy)
    {
        enemies.Add(enemy);
        discartedCardsCountText.text = enemies.Count.ToString();
        
        // enemy.transform.parent = transform.Find("plane");
        // enemy.transform.localPosition = new Vector3(0f, 1f, 0f);
    }
}
