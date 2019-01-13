using SaltboxGames.Common.Utils;
using System.Collections;
using UnityEngine;

namespace UnrealTortlement.Weapons
{
    public enum FireMode
    {
        Single,
        Burst,
        Auto
    }

    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private Transform _spawnPoint;
        [SerializeField]
        private float _muzzleVelocity;

        [SerializeField]
        private float damage;

        public int _ammoWorth;
        public int _ammoCost;
        public AmmoType _ammoType;

        public FireMode _fireMode;
        [SerializeField]
        private float _coolDownTime;
        [SerializeField]
        private float _burstDelay;
        [SerializeField]
        private int _burstCount;

        private bool canFire = true;

        public bool tryFire(string owner)
        {
            if(!canFire)
            {
                return false;
            }

            if(_fireMode == FireMode.Burst)
            {
                StartCoroutine(BurstFire(_burstDelay, _burstCount, owner));
            }
            else
            {
                Game.bulletPool.spawn(_spawnPoint.position, transform.forward * _muzzleVelocity, damage, owner); 
            }

            if (_coolDownTime > 0)
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

        private IEnumerator BurstFire(float time, int count, string owner)
        {
            WaitForSeconds delay = new WaitForSeconds(time);
            for (int i = 0; i < count; i++)
            {
                Game.bulletPool.spawn(_spawnPoint.position, transform.forward * _muzzleVelocity, damage, owner);
                yield return delay;
            }
        }
    }
}
