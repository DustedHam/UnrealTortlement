using SaltboxGames.Common.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnrealTortlement.Projectiles
{
    public class ProjectilePool
    {
        private GameObject prefab;
        private Pool<Projectile> pool;

        public ProjectilePool(GameObject prefab)
        {
            this.prefab = prefab;
            pool = new Pool<Projectile>(createBullet);
        }

        public Projectile spawn(Vector3 position, Vector3 velocity, float damage, string owner)
        {
            Projectile projectile = pool.Take();
            projectile.gameObject.SetActive(true);
            projectile.transform.forward = velocity.normalized;
            projectile.init(this, position, velocity, damage, owner);
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
