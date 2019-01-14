using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnrealTortlement.Turtle;
using UnrealTortlement.Weapons;

namespace UnrealTortlement
{
    public class UIController : MonoBehaviour
    {

        [SerializeField] Image healthBar;
        [SerializeField] Text healthWords;
        [SerializeField] Text currentClip;
        [SerializeField] Text totalAmmo;
        [SerializeField] Text killCount;

        private Weapon currentWeapon;
        private Player turtle;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void SetPlayers(Player player)
        {
            turtle = player;
            player.onHealthChange += OnHealthChange;
            player.onAmmoChange += OnPlayerAmmoChange;
            player.onWeaponChange += OnWeaponChange;
        }

        private void OnHealthChange(float current, float was)
        {
            healthBar.fillAmount = current / turtle.maxHealth;
            healthWords.text = current + "/" + turtle.maxHealth;
        }

        private void OnPlayerAmmoChange(int type, int amount)
        {
            totalAmmo.text = amount.ToString();
        }

        private void OnWeaponChange(Weapon weapon)
        {
            if(currentWeapon != null)
            {
                currentWeapon.onAmmoChange -= OnWeaponAmmoChange;
            }
            
            currentWeapon = weapon;
            weapon.onAmmoChange += OnWeaponAmmoChange;
            currentClip.text = weapon._ammoCount + "/" + weapon._clipSize;
        }

        private void OnWeaponAmmoChange(int current)
        {
            currentClip.text = current + "/" + currentWeapon._clipSize;
        }
    }
}