using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnrealTortlement.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private int ricochet = 2;
        [SerializeField]
        private Rigidbody _rigidbody; 

        private ProjectilePool pool;

        private int hitCount;

        public void init(ProjectilePool pool, Vector3 position, Vector3 velocity)
        {
            this.pool = pool;
            transform.position = position;
            _rigidbody.velocity = velocity;

            hitCount = 0;
        }

        private void FixedUpdate()
        {
            Vector3 velocity = _rigidbody.velocity;
            Vector3 direction = velocity.normalized;
            float speed = velocity.magnitude;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, speed * Time.fixedDeltaTime))
            {
                direction = Vector3.Reflect(direction, hit.normal);
                _rigidbody.velocity = direction * speed * 0.3f;

                hitCount++;

                if(hit.transform.tag == "Player")
                {
                    Debug.Log("player hit");
                }

                if (hitCount > ricochet)
                {
                    pool.despawn(this);
                }
            }

            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
