using SaltboxGames.Common.Utils;
using System;
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

        public static Action<string> onGameOver;

        public static List<PlayerSpawnPoint> spawnPoints = new List<PlayerSpawnPoint>();
        public static Dictionary<string, PlayerInputs> controlMaps;

        public static ProjectilePool bulletPool;
        public static List<Player> players = new List<Player>();

        public static Dictionary<string, int> playerScores;

        public static void Init(GameManager gameManager)
        {
            manager = gameManager;
            bulletPool = new ProjectilePool(manager.BulletPrefab);

            controlMaps = new Dictionary<string, PlayerInputs>();
            initControlMaps();

            playerScores = new Dictionary<string, int>();
        }

        public static void respawnPlayer(Player player)
        {
            manager.respawnPlayer(player);
        }

        public static Weapon SpawnRandomWeapon(Vector3 position)
        {
            GameObject obj = GameObject.Instantiate(manager.WeaponPrefabs.GetRandom(), position, Quaternion.identity);
            Weapon weapon = obj.GetComponent<Weapon>();

            return weapon;
        }

        public static Vector3 getSpawnPoint(float minDistance)
        {
            int start = UnityEngine.Random.Range(0, spawnPoints.Count);
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

        public static void IncrementScore(string playerName)
        {
            playerScores.AddorUpdate(playerName, 1, (score) =>
            {
                int val = score + 1;
                Debug.Log($"{playerName} {val}");
                if (score >= manager.scoreToWin)
                {
                    Debug.Log("Game Over");
                    Game.onGameOver?.Invoke(playerName);
                }
                return val;
            });
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
                Pitch = "Mouse Y",
                ChangeWeap = "CWeap"
            });
            controlMaps.Add("Joystick1", new PlayerInputs()
            {
                AimSensitivity = 0.7f,

                Horizontal = "Joystick1-MoveX",
                Vertical = "Joystick1-MoveY",
                Jump = "Joystick1-Jump",
                Fire = "Joystick1-Fire",
                Yaw = "Joystick1-ViewX",
                Pitch = "Joystick1-ViewY",
                ChangeWeap = "Joystick1-CWeap"
            });
            controlMaps.Add("Joystick2", new PlayerInputs()
            {
                AimSensitivity = 0.7f,

                Horizontal = "Joystick2-MoveX",
                Vertical = "Joystick2-MoveY",
                Jump = "Joystick2-Jump",
                Fire = "Joystick2-Fire",
                Yaw = "Joystick2-ViewX",
                Pitch = "Joystick2-ViewY",
                ChangeWeap = "Joystick2-CWeap"
            });
        }
    }
}
