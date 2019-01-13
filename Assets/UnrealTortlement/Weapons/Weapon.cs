using SaltboxGames.Common.Utils;
using System;
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
        public Action<int> onAmmoChange;

        public string _name;

        [SerializeField]
        private Transform _spawnPoint;
        [SerializeField]
        private float _muzzleVelocity;

        [SerializeField]
        private float damage;

        public int _ammoWorth;
        public int _ammoCost;
        public int _clipSize;
        [ReadOnlyField]
        public int _ammoCount;
        public AmmoType _ammoType;

        public FireMode _fireMode;
        [SerializeField]
        private float _coolDownTime;
        [SerializeField]
        private float _burstDelay;
        [SerializeField]
        private int _burstCount;

        [SerializeField]
        private float _reloadTime;

        private bool canFire = true;
        [SerializeField]
        public bool isReloading = false;

        public bool tryFire(string owner)
        {
            if(!canFire)
            {
                return false;
            }

            if(_ammoCount == 0)
            {
                return false;
            }

            if (_fireMode == FireMode.Burst)
            {
                StartCoroutine(BurstFire(_burstDelay, _burstCount, owner));
            }
            else
            {
                spawnBullet(owner);
            }

            if (_coolDownTime > 0)
            {
                StartCoroutine(CoolDown(_coolDownTime));
            }
            return true;            
        }

        public void Reload(int ammoCount)
        {
            Debug.Log(ammoCount);
            _ammoCount = ammoCount;
            StartCoroutine(ReloadTime(_reloadTime));
        }

        private void spawnBullet(string owner)
        {
            Game.bulletPool.spawn(_spawnPoint.position, transform.forward * _muzzleVelocity, damage, owner);
            _ammoCount--;
            onAmmoChange?.Invoke(_ammoCount);
        }

        private IEnumerator CoolDown(float time)
        {
            canFire = false;
            yield return new WaitForSeconds(time);
            canFire = true;
        }

        private IEnumerator ReloadTime(float time)
        {
            isReloading = true;
            yield return new WaitForSeconds(time);
            isReloading = false;
        }

        private IEnumerator BurstFire(float time, int count, string owner)
        {
            WaitForSeconds delay = new WaitForSeconds(time);
            for (int i = 0; i < count; i++)
            {
                spawnBullet(owner);
                if (_ammoCount == 0)
                {
                    break;
                }
                yield return delay;
            }
        }
    }
}
