using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnrealTortlement.Turtle;

namespace UnrealTortlement.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        private static float MIN_SPEED = 5;

        [SerializeField]
        private int ricochet = 2;
        [SerializeField]
        private Rigidbody _rigidbody; 

        private ProjectilePool pool;

        private string owner;

        private int hitCount;
        private float damage;

        public void init(ProjectilePool pool, Vector3 position, Vector3 velocity, float damage, string owner)
        {
            this.pool = pool;
            this.damage = damage;
            this.owner = owner;
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
                    Player player = hit.transform.GetComponent<Player>();
                    if(velocity.magnitude > MIN_SPEED)
                    {
                        player.hurt(damage, owner);
                    }  
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
