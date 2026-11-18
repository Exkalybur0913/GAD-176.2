using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    //public Slider playerEnergy; FOR PLAYER ENERGY UI

    private int maxEnergy = 100;
    private int currentEnergy;

    public static PlayerEnergy instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentEnergy = maxEnergy;
        //playerEnergy.maxValue = maxEnergy;
        //playerEnergy.value = maxEnergy;
    }

    public void UseEnergy(int amount)
    {
        if (currentEnergy - amount >= 0)
        {
            currentEnergy -= amount;
            //playerEnergy.value = currentEnergy;
        }
        else
        {
            Debug.Log($"You are out of Energy!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
