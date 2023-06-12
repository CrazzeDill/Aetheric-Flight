using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffSelectionUI : MonoBehaviour
{
    private LogicScript logic;
    private BuffsScript buffs;

    public Text levelUpText;
    public Transform buttonParent;
    public GameObject buffButtonPrefab;
    public GameObject buffSelectionUI;

    private BuffSelection buffSelection = new();

    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        buffs = GameObject.FindGameObjectWithTag("Logic").GetComponent<BuffsScript>();

        //buffSelection = new BuffSelection();
        //buffSelectionUI.SetActive(false);
    }

    public void ShowBuffSelection()
    {

        //int newLevel = currentLevel + 1;
        //levelUpText.text = "Congratulations! You've reached level " + newLevel + ".";
        

        if (buffSelection == null)
        {
            Debug.LogError("BuffSelection object is not assigned or instantiated.");
            return;
        }

        List<string> availableBuffs = buffSelection.GetThreeRandomBuffs();
        if(availableBuffs.Count == 0)
        {
            logic.isPaused = false;
            Time.timeScale = 1f;
            return;
        }
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        logic.isPaused = true;
        Time.timeScale = 0f;
        // Destroy existing buttons
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        // Create new buttons
        for (int i = 0; i < availableBuffs.Count; i++)
        {
            GameObject buffButton = Instantiate(buffButtonPrefab, buttonParent);
            Text[] buttonTexts = buffButton.GetComponentsInChildren<Text>();
            buttonTexts[0].text = availableBuffs[i];
            buttonTexts[1].text = buffSelection.GetBuffDescription(availableBuffs[i]);


            int index = i; // Capture the value of 'i' in a local variable for the button's onClick event
            buffButton.GetComponent<Button>().onClick.AddListener(() => ChooseBuff(index));
        }

        buffSelectionUI.SetActive(true);
    }

    public void ChooseBuff(int buffIndex)
    {
        // Apply the chosen buff to the player's character
        string chosenBuff = buttonParent.GetChild(buffIndex).GetComponentInChildren<Text>().text;
        buffSelection.addPickedBuff(chosenBuff);
        buffs.ApplyBuff(chosenBuff);

        // Close the buff selection UI
        buffSelectionUI.SetActive(false);
        logic.isPaused = false;
        Time.timeScale = 1f;
    }


}

public class BuffSelection
{
    private Dictionary<string, string> buffDescriptions;
    private List<string> buffOptions;
    private List<string> pickedBuffs;

    public BuffSelection()
    {

        buffDescriptions = new Dictionary<string, string>
        {
            {"Aetheric Shield","Surround yourself with a protective barrier, making you immune to damage for a limited time." },
            {"Quantum Projection","Project yourself into the astral plane, allowing you to pass through obstacles for 3 seconds. Can be triggered every 30 seconds"},
            {"Hatred Reaction","Spawns enemies more often"},
            {"Wings of the Phoenix","Harness the mythical power of the Phoenix and revive once upon death."},
            {"Binary space","Halves the spawn rate of obstacles, but doubles the score given"},
            {"Fast learner","Gains 1.5x scores and exp from killing enemies"}
        };

        buffOptions = new List<string>(buffDescriptions.Keys);
        pickedBuffs = new List<string>();
    }

    public string GetRandomBuff(List<string> tempBuffs)
    {
        List<string> availableBuffs = new List<string>(buffOptions);
        availableBuffs.RemoveAll(buff => pickedBuffs.Contains(buff));
        availableBuffs.RemoveAll(buff => tempBuffs.Contains(buff));

        /*
        Debug.Log("==================================================");
        foreach (var item in availableBuffs)
        {
            Debug.Log(item);
        }
        */

        if (availableBuffs.Count == 0)
        {
            // If all buffs have been picked, reset the picked buffs list
            //pickedBuffs.Clear();
            //availableBuffs = new List<string>(buffOptions);
            return null;
        }

        int selectedIndex = Random.Range(0, availableBuffs.Count);
        string selectedBuff = availableBuffs[selectedIndex];
        

        return selectedBuff;
    }

    public List<string> GetThreeRandomBuffs()
    {
        List<string> threeRandomBuffs = new();

        for (int i = 0; i < 3; i++)
        {
            string selectedBuff = GetRandomBuff(threeRandomBuffs);
            if (selectedBuff is not null)
            {
                threeRandomBuffs.Add(selectedBuff);
            }
        }

        return threeRandomBuffs;
    }
    public void addPickedBuff(string selectedBuff)
    {
        pickedBuffs.Add(selectedBuff);
    }

    public string GetBuffDescription(string buffName)
    {
        string description;
        buffDescriptions.TryGetValue(buffName, out description);
        return description;
    }
}
