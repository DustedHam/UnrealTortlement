using SaltboxGames.Common.Utils;
using System.Collections;
using UnityEngine;

namespace UnrealTortlement.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private Transform _spawnPoint;
        [SerializeField]
        private float _muzzleVelocity;

        public int _ammoWorth;
        public int _ammoCost;
        public AmmoType _ammoType;

        public bool _isAuto;
        [SerializeField]
        private float _coolDownTime;

        private bool canFire = true;

        public bool tryFire()
        {
            if(!canFire)
            {
                return false;
            }

            Game.bulletPool.spawn(_spawnPoint.position, transform.forward * _muzzleVelocity);

            if(_coolDownTime > 0)
            {
                StartCoroutine(CoolDown(_coolDownTime));
            }
            return true;            
        }

        private IEnumerator CoolDown(float time)
        {
            canFire = false;
            yield return new WaitForSeconds(time);
            canFire = true;
        }
    }
}
