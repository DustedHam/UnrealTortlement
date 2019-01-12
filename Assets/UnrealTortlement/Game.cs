using SaltboxGames.Common.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnrealTortlement.LevelTools;
using UnrealTortlement.Projectiles;
using UnrealTortlement.Weapons;

namespace UnrealTortlement
{
    public static class Game
    {
        private static GameManager manager;

        public static List<PlayerSpawnPoint> spawnPoints = new List<PlayerSpawnPoint>();

        public static ProjectilePool bulletPool;

        public static void Init(GameManager gameManager)
        {
            manager = gameManager;

            bulletPool = new ProjectilePool(manager.BulletPrefab);
        }

        public static Weapon SpawnRandomWeapon(Vector3 position)
        {
            GameObject obj = GameObject.Instantiate(manager.WeaponPrefabs.GetRandom(), position, Quaternion.identity);
            Weapon weapon = obj.GetComponent<Weapon>();

            return weapon;
        }
    }
}
