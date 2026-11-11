using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerWeaponsManager : MonoBehaviour
{
    [SerializeField] List<WeaponsSystem>
           weaponLoadout;
    private int currentWeaponIndex = 0;
    private float weaponCooldownTimer = 0f;
    void FixedUpdate()
    {
        HandleSwitchWeapons();
        HandleWeaponsFire();
        weaponCooldownTimer -= Time.deltaTime;
    }

    void HandleSwitchWeapons()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;
        if (scroll > 0)
        {
            currentWeaponIndex++;
           if (currentWeaponIndex > 2)
            {
                currentWeaponIndex = 0;
            }
            if (currentWeaponIndex < 2)
            {
                currentWeaponIndex = 0;
            }
        }

        Debug.Log("Weapon: " + currentWeaponIndex);
        EquipWeapon(currentWeaponIndex);
    }

    void EquipWeapon(int currentWeapon)
    {

    }
    private void HandleWeaponsFire()
    {
        
    }
}
