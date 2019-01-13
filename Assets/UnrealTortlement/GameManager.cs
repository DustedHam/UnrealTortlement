using SaltboxGames.Common.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnrealTortlement.Turtle;

namespace UnrealTortlement
{
    public class GameManager : MonoBehaviour
    {
        public GameObject BulletPrefab;

        public GameObject PlayerPrefab;
        public GameObject PlayerCameraPrefab;

        public GameObject[] WeaponPrefabs;

        private void OnEnable()
        {
            Game.Init(this);
        }

        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            SpawnPlayer(Game.controlMaps["Keyboard"], Game.getSpawnPoint(10), new Rect(0, 0, 1, 0.5f));
            SpawnPlayer(Game.controlMaps["Joystick1"], Game.getSpawnPoint(10), new Rect(0, 0.5f, 1, 0.5f));
        }

        private Player SpawnPlayer(PlayerInputs controls, Vector3 position, Rect cameraRect)
        {
            GameObject obj = GameObject.Instantiate(PlayerPrefab);
            obj.transform.position = position;
            Player player = obj.GetComponent<Player>();

            GameObject camObj = GameObject.Instantiate(PlayerCameraPrefab);
            Camera camera = camObj.GetComponent<Camera>();
            camera.rect = cameraRect;

            player._controls = controls;
            player._camera = camera;

            Game.players.Add(player);

            return player;
        }
    }
}
