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
            SpawnPlayer(Game.controlMaps["Keyboard"], Game.getSpawnPoint(10), new Rect(0, 0, 1, 0.49f));
            SpawnPlayer(Game.controlMaps["Joystick1"], Game.getSpawnPoint(10), new Rect(0, 0.505f, 1, 0.49f));
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

        public void respawnPlayer(Player player)
        {
            StartCoroutine(respawnDelay(player, 2));
        }

        public IEnumerator respawnDelay(Player player, float delay)
        {
            player.gameObject.SetActive(false);
            yield return new WaitForSeconds(delay);

            player.transform.position = Game.getSpawnPoint(10);

            player.gameObject.SetActive(true);
        }
    }
}
