using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;

public class HealthScript : MonoBehaviour, IKillable
{
    bool dead;
    public float integrity;
    public float shields;
    public float shieldRechargeRate = 10f;
    public float energyDrainPerSecond = 2f;
    [SerializeField] float maxIntegrity = 250;
    [SerializeField] float maxShields = 150;
    [SerializeField] float DoTRate;
    [SerializeField] Transform frontFire;
    [SerializeField] Transform rearFire;
    [SerializeField] Transform sideFire;
    [SerializeField] GameObject infernoRound;
    [SerializeField] GameObject macCannonRound;
    bool debugEnabled;
    private Energy energyStamina;
    

    private float armorType;
    private float mediumArmor = 0.75f;
    [SerializeField] ParticleSystem deathExplosion;




    void Start() /*will definitely be an overrided class for different health values based on armor type. Heavy will get more health,
                            more shields and a lower armorType value. May also contain other overrided things like some ships starting at different shield percentages                            
                           */
    {
        dead = false;
        InitialiseHealth();

        debugEnabled = false;
    }

    protected virtual void InitialiseHealth()
    {
        integrity = maxIntegrity;
        shields = maxShields;
        armorType = mediumArmor;
        energyStamina = GetComponent<Energy>();
    }
    
    public void CalculatePointDamage(Transform damageSourceDirection, float baseDamage)
    {
        if (shields > 0)
        {
            TakePointDamage(baseDamage);
            Debug.Log($"Player has taken {baseDamage} damage to shields");
        }
        else if (shields <= 0)
        {
            Vector3 myForward = transform.forward;
            myForward.y = 0f;
            myForward.Normalize();
            Vector3 myPosition = transform.position;
            Vector3 directionToAttacker =
                damageSourceDirection.position - myPosition;

            directionToAttacker.y = 0f;
            directionToAttacker.Normalize();



            float dot = Vector3.Dot(myForward, directionToAttacker);

            float multiplier;

            if (dot >= 0.5f)
            {
                multiplier = 0.75f;
            }
            else if (dot <= -0.5f)
            {
                multiplier = 1.5f;
            }
            else
            {
                multiplier = 1.0f;
            }

            float finalDamage = (baseDamage * armorType) * multiplier;

            TakePointDamage(finalDamage);

            Debug.Log($"Base damage: {finalDamage}, " +
                      $"Multiplier: {multiplier}, " +
                      $"Final damage: {finalDamage} straight to health");
        }
    }

    public void CalculateDamageOverTime(float baseDuration, float DoT)
    {
        float finalDoT = DoT * armorType;
        if (shields > 0)
        {
            shields -= finalDoT;         
        }
        else
        {
            Debug.Log($"Shields DOWN!!");
            StartCoroutine(TakeDamageOverTime(baseDuration, finalDoT));
        }
    }

    void TakePointDamage(float finalPointDamage)
    {
        if (shields > 0)
        {
            shields -= finalPointDamage;
        }
        else
        {
            Debug.Log($"Shields DOWN!!");
            float critChance = Random.Range(0, integrity);
            if (critChance < 20)
            {
                integrity -= finalPointDamage * 1.25f;
                if (integrity <= 0)
                {
                    Die();
                }
            }
            else
            {
                integrity -= finalPointDamage;
                if (integrity <= 0)
                {
                    Die();
                }
            }
        }
    }

    IEnumerator TakeDamageOverTime(float duration, float damagePerTick)
    {
        Debug.Log($"DoT started: {damagePerTick} damage every {DoTRate}s for {duration}s");

        float elapsedTime = 0f;

        while (elapsedTime < duration && !dead)
        {
            integrity -= damagePerTick;

            Debug.Log($"DoT tick! Integrity remaining: {integrity}");

            if (integrity <= 0)
            {
                integrity = 0;
                Die();
                yield break;
            }

            yield return new WaitForSeconds(DoTRate);
            elapsedTime += DoTRate;
        }

        Debug.Log("DoT ended.");
    }

    IEnumerator ShieldRecharge (float rechargeAmount, float energyDrain)
    {
        if (energyStamina.energy <= 0)
        {
            yield break;
        }

        else
        {
            shields += rechargeAmount * Time.deltaTime;
            energyStamina.energy -= energyDrain;
        }
    
        if (shields >= maxShields)
        {
            shields = maxShields;
            yield break;
        }
        yield return new WaitForSeconds(0.5f);
    }
    public void Die()
    {
        if (dead) return;
        dead = true;
        StopAllCoroutines();
        Instantiate(deathExplosion, transform.position, Quaternion.identity);
        Debug.Log($"U ARE DIE LMAO");
        Destroy(gameObject);
    }


    //This is a debug method to be turned into a comment or disabled when game is in build mode
    void DebugDamageFire()
    {
        if (debugEnabled)
        {
            //fired at side
            if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                GameObject round = Instantiate(macCannonRound, sideFire.position, sideFire.rotation);

                Vector3 direction = (transform.position - sideFire.position).normalized;

                round.GetComponent<Rigidbody>().linearVelocity = direction * 3f;
            }
            //fired at front
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                GameObject round = Instantiate(macCannonRound, frontFire.position, frontFire.rotation);

                Vector3 direction = (transform.position - frontFire.position).normalized;

                round.GetComponent<Rigidbody>().linearVelocity = direction * 3f;
            }
            //fired at rear
            if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                GameObject round = Instantiate(macCannonRound, rearFire.position, rearFire.rotation);

                Vector3 direction = (transform.position - rearFire.position).normalized;

                round.GetComponent<Rigidbody>().linearVelocity = direction * 3f;
            }
            //inferno rounds/ Damage over time
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                GameObject round = Instantiate(infernoRound, sideFire.position, sideFire.rotation);

                Vector3 direction = (transform.position - sideFire.position).normalized;

                round.GetComponent<Rigidbody>().linearVelocity = direction * 3f;
            }
        }
    }

    void InputHandler()
    {
        if (Keyboard.current.qKey.isPressed)
        {
            ShieldRecharge(shieldRechargeRate, energyDrainPerSecond);
        }

        if (Keyboard.current.insertKey.isPressed)
        {
            debugEnabled = !debugEnabled;
        }
    }
    void Update()
    {
        InputHandler();
        DebugDamageFire();
        if (shields<= 0)
        {
            shields = 0;
        }
        Debug.Log($"Health: {integrity}, Shields: {shields}");
    }
}
