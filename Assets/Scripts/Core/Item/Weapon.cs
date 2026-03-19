using UnityEngine;

public enum WeaponType
{
    Melee,
    Ranged
}
public class Weapon : Item
{
    public WeaponType weaponType;
    public float damage;
    public float ranger;
    public GameObject weaponPrefab;
    public Transform firePoint;
}
