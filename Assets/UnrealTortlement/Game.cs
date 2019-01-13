using SaltboxGames.Common.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnrealTortlement.LevelTools;
using UnrealTortlement.Projectiles;
using UnrealTortlement.Turtle;
using UnrealTortlement.Weapons;

namespace UnrealTortlement
{
    public static class Game
    {
        private static GameManager manager;

        public static List<PlayerSpawnPoint> spawnPoints = new List<PlayerSpawnPoint>();
        public static Dictionary<string, PlayerInputs> controlMaps;

        public static ProjectilePool bulletPool;
        public static List<Player> players = new List<Player>();

        public static void Init(GameManager gameManager)
        {
            manager = gameManager;

            bulletPool = new ProjectilePool(manager.BulletPrefab);

            controlMaps = new Dictionary<string, PlayerInputs>();
            initControlMaps();
        }

        public static Weapon SpawnRandomWeapon(Vector3 position)
        {
            GameObject obj = GameObject.Instantiate(manager.WeaponPrefabs.GetRandom(), position, Quaternion.identity);
            Weapon weapon = obj.GetComponent<Weapon>();

            return weapon;
        }

        public static Vector3 getSpawnPoint(float minDistance)
        {
            int start = Random.Range(0, spawnPoints.Count);
            Vector3 point = Vector3.zero;

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                int index = (start + i) % spawnPoints.Count;
                point = spawnPoints[index].Point;
                if(checkDistanceToPlayers(point, minDistance))
                {
                    return point;
                }
            }
            return point;
        }

        private static bool checkDistanceToPlayers(Vector3 pos, float distance)
        {
            for (int i = 0; i < players.Count; i++)
            {
                Player player = players[i];
                if (player.isAlive && Vector3.Distance(player.transform.position, pos) <= distance)
                {
                    return false;
                }
            }
            return true;
        }

        private static void initControlMaps()
        {
            controlMaps.Add("Keyboard", new PlayerInputs()
            {
                AimSensitivity = 1.2f,

                Horizontal = "Horizontal",
                Vertical = "Vertical",
                Jump = "Jump",
                Fire = "Fire1",
                Yaw = "Mouse X",
                Pitch = "Mouse Y"
            });
            controlMaps.Add("Joystick1", new PlayerInputs()
            {
                AimSensitivity = 0.7f,

                Horizontal = "Joystick1-MoveX",
                Vertical = "Joystick1-MoveY",
                Jump = "Joystick1-Jump",
                Fire = "Joystick1-Fire",
                Yaw = "Joystick1-ViewX",
                Pitch = "Joystick1-ViewY"
            });
            controlMaps.Add("Joystick2", new PlayerInputs()
            {
                AimSensitivity = 0.7f,

                Horizontal = "Joystick2-MoveX",
                Vertical = "Joystick2-MoveY",
                Jump = "Joystick2-Jump",
                Fire = "Joystick2-Fire",
                Yaw = "Joystick2-ViewX",
                Pitch = "Joystick2-ViewY"
            });
        }
    }
}
