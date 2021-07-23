using System;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class SaveData
{
    public int totalMuffins = 0;
    public int muffinsPerClick = 1; // We always want at least 1 muffin per click, or the is unplayable
    public int muffinsPerSecond = 0;
    public int upgrade1Level = 0;
    public int upgrade2Level = 0;
    public int upgrade3Level = 0;
    // TODO: Add more saving here
}

public class Game : MonoBehaviour
{
    [SerializeField] private float clickClickChance = 0.1f;
    public int criticalClickMultiplier = 10;
    public GameObject floatingTextPrefab;
    public Transform floatingTextContainer;

    public int totalMuffins;
    private int muffinsPerClick = 1;
    public int muffinsPerSecond = 0;
    public UpgradeButton upgrade1Button;
    public UpgradeButton upgrade2Button;
    public UpgradeButton upgrade3Button;
    private float passiveMuffinCountdown;
    [SerializeField] private Donut _donutPrefab;
    [SerializeField] private Paddle _paddlePrefab;

    public void Start()
    {
        LoadGame();

        //InvokeRepeating(nameof(CollectPassiveMuffins), 1f, 1f);

        // Start the countdown to collect the passive muffins
        passiveMuffinCountdown = 1f;
    }

    private void CollectPassiveMuffins()
    {
        totalMuffins += muffinsPerSecond;
    }

    private void Update()
    {
        // Check if the F12 key is pressed
        if (Input.GetKeyDown(KeyCode.F12))
        {
            // Reset the game
            ResetGameCheat();
        }

        // Update the passive muffins countdown
        passiveMuffinCountdown -= Time.deltaTime;

        // If the countdown has run out
        if (passiveMuffinCountdown <= 0f)
        {
            // Collect the passive muffins
            CollectPassiveMuffins();

            // Reset the passive mufins countdown
            passiveMuffinCountdown = 1f;
        }
    }

    private void SaveGame()
    {
        // Create the save data object
        SaveData saveData = new SaveData();

        // Populate the save data object with the game's current state
        saveData.totalMuffins = totalMuffins;
        saveData.muffinsPerClick = muffinsPerClick;
        saveData.muffinsPerSecond = muffinsPerSecond;
        saveData.upgrade1Level = upgrade1Button.level;
        saveData.upgrade2Level = upgrade2Button.level;
        saveData.upgrade3Level = upgrade3Button.level;
        // TODO: Add more saving here

        // Convert the save data object to JSON
        string saveJSON = JsonUtility.ToJson(saveData);
        Debug.Log($"Saving JSON: {saveJSON}");

        // Save the JSON to the 'savegame' in player prefs
        PlayerPrefs.SetString("savegame", saveJSON);
    }

    private void LoadGame()
    {
        // Load the save game JSON from player pref
        string saveJSON = PlayerPrefs.GetString("savegame", "{}");
        Debug.Log($"Loading JSON: '{saveJSON}'");

        // Convert the JSON into a SaveData object
        SaveData saveData = JsonUtility.FromJson<SaveData>(saveJSON);

        // Restore the game's state from the save game
        totalMuffins = saveData.totalMuffins;
        muffinsPerClick = saveData.muffinsPerClick;
        muffinsPerSecond = saveData.muffinsPerSecond;
        upgrade1Button.level = saveData.upgrade1Level;
        upgrade2Button.level = saveData.upgrade2Level;
        upgrade3Button.level = saveData.upgrade3Level;

        // TODO: Add more loading here
    }

    private void ResetGameCheat()
    {
        Debug.Log("CHEAT: Reseting game");

        // Reset all the game state to the starting state
        totalMuffins = 0;
        muffinsPerClick = 1;
        muffinsPerSecond = 0;
        upgrade1Button.level = 0;
        upgrade2Button.level = 0;
        upgrade3Button.level = 0;
        // TODO: Reset any new variables
    }

    public void OnApplicationQuit()
    {
        SaveGame();
    }

    public void OnMuffinsCollected()
    {
        int muffinsAwarded = muffinsPerClick;

        // If this is a critical click
        if (Random.value <= clickClickChance)
        {
            // Increase the number of muffins awards
            muffinsAwarded *= criticalClickMultiplier;
        }

        // Increment the number of muffin collected
        totalMuffins += muffinsAwarded;

        // TODO: Eventually refactor out the floating text spawning somewhere else

        // Create the floating text notification
        CreateFloatingText("+" + muffinsAwarded);
    }

    private void CreateFloatingText(string message)
    {
        // Spawn a new floating text
        GameObject floatingText = Instantiate(floatingTextPrefab, floatingTextContainer);

        // Generate a random position around the muffin
        Vector2 randomPosition = GetRandomPositionAroundMuffin();

        // Position the new floating text at the random position
        floatingText.transform.localPosition = randomPosition;

        // Set the floating text's actual text
        floatingText.GetComponent<TMP_Text>().text = message;
    }

    private Vector2 GetRandomPositionAroundMuffin()
    {
        // Calculate a random X and Y coordinate for the position
        float x = Random.Range(-200f, 200f);
        float y = Random.Range(75f, 225f);

        // Create and return a Vector2 using the coordinates
        return new Vector2(x, y);
    }

    public bool TryToPurchaseUpgrade(UpgradeType type, int price, int level)
    {
        // If the player can't afford the upgrade
        if(totalMuffins < price)
        {
            // Bail and return false so the upgrade level doesn't increase
            return false;
        }

        // Take the player's muffins
        totalMuffins -= price;

        // Apply the upgrade
        switch(type)
        {
            case UpgradeType.MuffinsPerClick:
                // Increase the number of muffins pers click
                muffinsPerClick += 1;
                break;

            case UpgradeType.MuffinsPerSecond:
                // Apply the upgrade
                muffinsPerSecond += 1;
                break;
            
            case UpgradeType.Donut:
                SpawnDonut(level);
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }


        /*Version version = Version.Windows98;

        switch (version)
        {
            case Version.Windows31:
            case Version.Windows95:
            case Version.Windows98:
                // Do something for old desktop version
                break;
            case Version.WindowsNT:
            case Version.Windows2000:
                // Do something different for server versions
                break;
            case Version.WindowsXP:
                // Do something different for modern versions
                break;
        }*/

        // Return true so the upgrade level increases
        return true;
    }

    private void SpawnDonut(int level)
    {
        // Spawn the donut
        var donut = Instantiate(_donutPrefab, floatingTextContainer);
        donut.transform.localPosition = GetRandomPositionAroundMuffin();
        donut._level = level;
        
        // Spawn the paddle
        Instantiate(_paddlePrefab, floatingTextContainer);
    }

    /*public enum Version
    {
        Windows31,
        Windows95,
        Windows98,
        WindowsNT,
        Windows2000,
        WindowsXP,
        Linux,
        Mac
    }*/
    
    public void AwardDonutMuffins(int amount)
    {
        totalMuffins += amount;
        CreateFloatingText($"+{amount}");
    }
}
