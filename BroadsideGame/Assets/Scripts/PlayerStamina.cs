using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    //public Slider playerStamina; FOR STAMINA UI OF THE PLAYER

    private int maxStamina = 100;
    private int currentStamina;
    private float regenerateStamina = 1f;
    private float regenerationTimer;

    public static PlayerStamina instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentStamina = maxStamina;
        //playerStamina.maxValue = maxStamina;
        //playerStamina.value = maxStamina;
        
    }

    public void UseStamina(int amount)
    {
        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            //playerStamina.value = currentStamina;
        }
        else
        {
            Debug.Log($"You are out of Stamina!");
        }


    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*
    void RegenStam()
    {
        float staminaPerSec = 10
        float staminaRate = 1
        while (currentStamina<maxStamina)
        {
            currentStamina += staminaPerSec
                Debug.Log($"Player regened {staminaPerSec} stamina this second")

            if (currentStamina > = maxStamina)
            {
                currentStamina = maxStamina;
                yield break;
            }

yield return new WaitForSeconds(staminaRate);
        }
    }
    */
    // Update is called once per frame
    void Update()
    {
        
    }
}
