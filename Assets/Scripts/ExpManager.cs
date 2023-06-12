using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ExpPreset
{
    public int level; 
    public float expRequired;

    public ExpPreset(int level, float expRequired)
    {
        this.level = level;
        this.expRequired = expRequired;
    }
}

public class ExpManager : MonoBehaviour
{
    private List<ExpPreset> expPresets = new();
    public Slider expBar;
    public float expMod = 7f;
    private float currentExp;
    private int currentLevel;
    public GameObject levelupmenu;
    private LogicScript logic;
    public BuffSelectionUI buffSelectionUI;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 12; i++)
        {
            expPresets.Add(new ExpPreset(i,100f*(i+1.33f)));
        }
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        currentExp = 0f;
        currentLevel = 0;
        SetMaxExp();
    }

    public void SetMaxExp()
    {
        expBar.maxValue = expPresets[currentLevel].expRequired;
    }

    public void SetExp()
    {
        expBar.value = currentExp;
    }

    public void MultiplyExpMod(float amount){
        expMod *= amount;
    }

    public void LevelUpAction()
    {
        buffSelectionUI.ShowBuffSelection();
    }

    public void AddExp(int expAmount)
    {
        currentExp += expAmount*expMod;
        // Check if the current exp exceeds the required exp for the next level
        while (currentLevel < expPresets.Count-1 && currentExp >= expPresets[currentLevel].expRequired)
        {
            // Level up
            currentExp -= expPresets[currentLevel].expRequired; // Subtract the required exp for the current level
            currentLevel++;
            // Perform level up actions or rewards here
            LevelUpAction();
            SetMaxExp();
        }

        // Update the UI or perform other actions based on the current level and exp
        SetExp();

    }

    

}
