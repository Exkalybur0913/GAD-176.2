using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using Unity.VisualScripting;
using UnityEngine;

public enum WeaponType
{
    PointDamage,
    DamageOverTime
}

[CreateAssetMenu (fileName = "NewWeapons", menuName = "shipWeaponType")]
public class WeaponsSystem : ScriptableObject
{
    public string weaponName;
    public WeaponType weaponType = WeaponType.PointDamage;

    public GameObject kineticCannonPrefab;
    public GameObject infernoCannonPrefab;

    public float DoTDuration;
    public float DoTValue;
    public float roundSpeed;
    public float spread;
    public int roundCount;
    public float pointDamage;
    public bool doTEnabled;
}
