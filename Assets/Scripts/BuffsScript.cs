using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffsScript : MonoBehaviour
{
    private List<string> activeBuffs = new List<string>();
    [SerializeField] private ShieldScript shield;
    [SerializeField] private BatSpawnerScript batSpawner;
    [SerializeField] private Generator obsGen;
    [SerializeField] private LogicScript logic;
    [SerializeField] private ExpManager expManager;



    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ApplyBuff(string buffName)
    {
        // Check if the buff is already active on the character
        if (!activeBuffs.Contains(buffName))
        {
            // Apply the buff to the character
            activeBuffs.Add(buffName);
            Debug.Log("You have chosen the buff: " + buffName + ".");


            // Perform actions based on the buff applied
            switch (buffName)
            {
                case "Aetheric Shield":
                    // Apply
                    shield.ActivateShield();
                    break;
                case "Quantum Projection":
                    // Apply 
                    break;
                case "Hatred Reaction":
                    batSpawner.ActivateHateReact();
                    // Apply 
                    break;
                case "Wings of the Phoenix":
                    logic.canRevive = true;
                    // Apply 
                    break;
                case "Binary space":
                    obsGen.MultiplySpawnRate(2f);
                    logic.MultiplyScoreMod(2f);
                    // Apply 
                    break;
                case "Fast learner":
                    expManager.MultiplyExpMod(1.5f);
                    // Apply
                    break;
            }
        }
    }
}
