using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnrealTortlement.Projectiles;

public class TestBullet : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Projectile>().init(null, transform.position, transform.forward * 35);
    }
}
