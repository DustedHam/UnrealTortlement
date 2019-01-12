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
            SpawnPlayer(new PlayerInputs(), Game.spawnPoints.GetRandom().Point);
        }

        private Player SpawnPlayer(PlayerInputs controls, Vector3 position)
        {
            GameObject obj = GameObject.Instantiate(PlayerPrefab);
            obj.transform.position = position;
            Player player = obj.GetComponent<Player>();

            GameObject camObj = GameObject.Instantiate(PlayerCameraPrefab);
            Camera camera = camObj.GetComponent<Camera>();

            player._controls = controls;
            player._camera = camera;

            return player;
        }
    }
}
