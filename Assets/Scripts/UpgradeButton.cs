using TMPro;
using UnityEngine;

public enum UpgradeType
{
    MuffinsPerClick,
    MuffinsPerSecond,
    Donut,
}

public class UpgradeButton : MonoBehaviour
{
    public TMP_Text levelText;
    public TMP_Text priceText;

    private Game game;
    public int level;
    private int price;
    public int pricePerLevel;
    public UpgradeType type;

    private void Awake()
    {
        game = FindObjectOfType<Game>();
    }

    public void Update()
    {
        // Update the level text
        levelText.text = level.ToString();

        // Calculate the current price of the upgrade
        price = (level + 1) * pricePerLevel;

        // Update the price text
        priceText.text = price.ToString();

        // Colour the price text according to whether the player can afford it or not
        // Turnary: <TEXT> ? <TRUE VALUE> : <FALSE VALUE>
        priceText.color = game.totalMuffins >= price ? Color.green : Color.red;
    }

    public void OnUpgradeClicked()
    {
        // Check if the upgrade was successfully purchased
        if(game.TryToPurchaseUpgrade(type, price, level + 1))
        {
            // If so, increase the upgrade's level
            level++;
        }
    }
}