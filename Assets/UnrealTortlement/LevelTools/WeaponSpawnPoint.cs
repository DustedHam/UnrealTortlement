using SaltboxGames.Common.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnrealTortlement.Turtle;
using UnrealTortlement.Weapons;

namespace UnrealTortlement.LevelTools
{
    [RequireComponent(typeof(BoxCollider))]
    public class WeaponSpawnPoint : MonoBehaviour
    {
        [SerializeField]
        private float _respawnTime;
        [SerializeField, Range(0, 100)]
        private float _rotateSpeed;

        [SerializeField, ReadOnlyField]
        private Weapon current;

        [SerializeField]
        private Transform _weaponPoint;

        private float rotation;

        private void Start()
        {
            StartCoroutine(Respawn(0.1f));
        }

        private void Update()
        {
            if (current != null)
            {
                current.transform.position = _weaponPoint.position;

                rotation += _rotateSpeed * Time.deltaTime;
                current.transform.eulerAngles = new Vector3(0, rotation, 0);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(current != null && other.tag == "Player")
            {
                Player player = other.GetComponent<Player>();
                assignWeapon(player);
            }
        }

        private void assignWeapon(Player player)
        {
            player.PickUpWeapon(current);
            current = null;
            StartCoroutine(Respawn(_respawnTime));
        }

        IEnumerator Respawn(float time)
        {
            yield return new WaitForSeconds(time);
            current = Game.SpawnRandomWeapon(_weaponPoint.position);         
        }

#if UNITY_EDITOR
        [SerializeField]
        private Color debugColor = new Color(1, 1, 0, 0.8f);

        private void OnDrawGizmos()
        {
            Gizmos.color = debugColor;
            Gizmos.DrawCube(transform.position, transform.localScale * 0.99f);
        }
#endif
    }
}


