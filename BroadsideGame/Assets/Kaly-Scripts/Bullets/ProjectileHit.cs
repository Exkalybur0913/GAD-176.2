using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    [SerializeField] WeaponsSystem WeaponSystem;
    [SerializeField] ParticleSystem hitFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.name}");
        //Instantiate(hitFX,transform.position,Quaternion.identity);

        HealthScript entityHealth = other.GetComponentInParent<HealthScript>();
        if (entityHealth != null)
        {
            if (WeaponSystem.doTEnabled)
            {
                entityHealth.CalculateDamageOverTime(WeaponSystem.DoTDuration, WeaponSystem.DoTValue);
                entityHealth.CalculatePointDamage(transform, WeaponSystem.pointDamage);
                Debug.Log(other.name + "Object has been struck with a DoT weapon");
            }
            else
            {  
                entityHealth.CalculatePointDamage(transform, WeaponSystem.pointDamage);
                Debug.Log(other.name + "Object has been struck");             
            }
            
        }
        else
        {
            Debug.Log("Entity has no collider, or is undamageable");
        }
        Destroy(gameObject);
    }
}
