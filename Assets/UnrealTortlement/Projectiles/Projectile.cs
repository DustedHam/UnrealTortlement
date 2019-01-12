using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnrealTortlement.projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rigidbody;

        private ProjectilePool pool;

        public void init(ProjectilePool pool, Vector3 position, Vector3 velocity)
        {
            this.pool = pool;
            _rigidbody.position = position;
            _rigidbody.velocity = velocity;
        }
    }
}
