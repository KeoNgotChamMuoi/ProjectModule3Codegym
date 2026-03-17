using UnityEngine;
using Game.Interfaces;

namespace Game.Core 
{
    public class AttackManager : MonoBehaviour 
    {
        private IWeaponStrategy _currentWeapon;
        [SerializeField] private Transform firePoint; // Vị trí vũ khí/nòng súng

        public void EquipWeapon(IWeaponStrategy newWeapon) 
        {
            _currentWeapon = newWeapon;
        }

        public void ExecuteAttack() 
        {
            if (_currentWeapon != null) 
            {
                _currentWeapon.UseWeapon(firePoint);
            }
        }

        public void ExecuteReload() 
        {
            _currentWeapon?.Reload();
        }
    }
}