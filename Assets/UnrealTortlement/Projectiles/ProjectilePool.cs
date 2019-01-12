using SaltboxGames.Common.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnrealTortlement.projectiles
{
    public class ProjectilePool
    {
        public GameObject prefab;
        private Pool<Projectile> pool;

        private void Awake()
        {
            pool = new Pool<Projectile>(createBullet);
        }

        public Projectile spawn(Vector3 position, Vector3 velocity)
        {
            Projectile projectile = pool.Take();
            projectile.gameObject.SetActive(true);
            projectile.init(this, position, velocity);
            return projectile;
        }

        public void despawn(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
            pool.Add(projectile);
        }

        private Projectile createBullet()
        {
            GameObject obj = GameObject.Instantiate(prefab);
            Projectile projectile = obj.GetComponent<Projectile>();
            return projectile;
        }
    }
}
