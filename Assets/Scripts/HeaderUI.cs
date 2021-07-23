using TMPro;
using UnityEngine;

public class HeaderUI : MonoBehaviour
{
    public TMP_Text totalMuffinsText;
    public TMP_Text muffinsPerSecondText;

    private Game game;

    private void Awake()
    {
        game = FindObjectOfType<Game>();
    }

    private void Update()
    {
        // If we only have a single muffin
        if (game.totalMuffins == 1)
        {
            // Update the total muffins text (singular)
            totalMuffinsText.text = game.totalMuffins + " muffin";
        }
        // Otherwise
        else
        {
            // Update the total muffins text (pluralized)
            totalMuffinsText.text = game.totalMuffins + " muffins";
        }

        // Update the number of muffins per second text
        muffinsPerSecondText.text = $"{game.muffinsPerSecond} {(game.muffinsPerSecond == 1 ? "muffin" : "muffins")} / sec";
    }
}
