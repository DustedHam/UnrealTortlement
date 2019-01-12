using SaltboxGames.Common.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnrealTortlement.Weapons;

namespace UnrealTortlement.Turtle
{   
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        public Camera _camera;

        [SerializeField]
        private float _speed = 5f;
        [SerializeField]
        private float _jumpHeight = 2f;
        [SerializeField]
        private float _groundDistance = 0.2f;
        [SerializeField]
        private float _turnSpeed = 0.2f;
        [SerializeField]
        private Transform _groundCheck;
        [SerializeField]
        private Transform _weaponPoint;
        [SerializeField]
        private Transform _cameraPoint;
        [SerializeField]
        private Rigidbody _rigidbody;
        [SerializeField]
        private LayerMask _ground;

        [SerializeField]
        public float camera_speedH = 2.0f;
        [SerializeField]
        public float camera_speedV = 2.0f;
        [SerializeField]
        private float yaw = 0.0f;
        [SerializeField]
        private float pitch = 0.0f;

        public PlayerInputs _controls;

        [SerializeField, ReadOnlyField]
        private Vector3 inputs = Vector3.zero;

        [SerializeField, ReadOnlyField]
        private Vector3 velocity = Vector3.zero;  
        
        [SerializeField, ReadOnlyField]
        private bool isGrounded = true;

        
        [SerializeField, ReadOnlyField]
        private int[] ammo;
        private List<Weapon> inventory;
        private int weaponIndex = -1;

        private int ignoreMask;

        private void Start()
        {
            //Ignore this player, and the raycast ignore layer
            ignoreMask = ~(1 << gameObject.layer | 1 << 2);

            ammo = new int[Enum.GetNames(typeof(AmmoType)).Length];
            inventory = new List<Weapon>();
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        public void PickUpWeapon(Weapon weap)
        {
            ammo[(int)weap._ammoType] += weap._ammoWorth;
            if (hasWeapon(weap))
            {
                Destroy(weap.gameObject);
            }
            else
            {
                inventory.Add(weap);
                if(weaponIndex == -1)
                {
                    weaponIndex = 0;
                }
                else
                {
                    weap.gameObject.SetActive(false);
                }
            }
        }

        private bool hasWeapon(Weapon weapon)
        {
            Type weapType = weapon.GetType();
            for (int i = 0; i < inventory.Count; i++)
            {
                if(inventory[i].GetType() == weapType)
                {
                    return true;
                }
            }
            return false;
        }

        private void Update()
        {
            Transform cameraTransfrom = _camera.transform;
            Vector3 cameraPosition = cameraTransfrom.position;
            Vector3 cameraForward = cameraTransfrom.forward;
            Vector3 cameraRight= cameraTransfrom.right;

            isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _ground, QueryTriggerInteraction.Ignore);

            // Move direction
            inputs = Vector3.zero;
            float horizontal = Input.GetAxis(_controls.Horizontal);
            float vertical = Input.GetAxis(_controls.Vertical);
            inputs = (cameraForward * vertical) + (cameraRight * horizontal);

            if (Input.GetButtonDown(_controls.Jump) && isGrounded)
            {
                _rigidbody.AddForce(Vector3.up * Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            }

            if (weaponIndex > -1)
            {
                Weapon current = inventory[weaponIndex];
                current.transform.position = _weaponPoint.position;

                if (Input.GetButtonDown(_controls.Fire))
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    int currentAmmo = ammo[(int)current._ammoType];

                    if(currentAmmo >= current._ammoCost)
                    {
                        if (current.tryFire())
                        {
                            currentAmmo -= current._ammoCost;
                        }
                    }

                    ammo[(int)current._ammoType] = currentAmmo;
                }
           
                RaycastHit hit;
                if (Physics.Raycast(cameraPosition, cameraForward, out hit, Mathf.Infinity, ignoreMask))
                {
                    current.transform.LookAt(hit.point);
                }
                else
                {
                    current.transform.LookAt(cameraForward * 100); //TODO: Lerp to this;
                }

                Debug.DrawRay(cameraPosition, cameraForward * 1000, Color.red);
            }

            // Camera rotation
            _camera.transform.position = _cameraPoint.position;

            yaw += camera_speedH * Input.GetAxis("Mouse X");
            pitch -= camera_speedV * Input.GetAxis("Mouse Y");
            pitch = Mathf.Clamp(pitch, -35, 75);

            transform.eulerAngles = new Vector3(0, yaw, 0);
            _camera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);


            if (Input.GetKey(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
            }
            velocity = _rigidbody.velocity;
        }

        void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + inputs * _speed * Time.fixedDeltaTime);
        }
    }
}
