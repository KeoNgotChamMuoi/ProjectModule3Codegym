using UnityEngine;

namespace Game.Interfaces {
    public interface IWeaponStrategy {
        void UseWeapon(Transform firePoint); 
        void Reload();
    }
}