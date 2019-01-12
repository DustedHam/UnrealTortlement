using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnrealTortlement.LevelTools
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        public Vector3 Point
        {
            get { return transform.position; }
        }

        private void OnEnable()
        {
            Game.spawnPoints.Add(this);
        }

        private void OnDisable()
        {
            Game.spawnPoints.Remove(this);
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


