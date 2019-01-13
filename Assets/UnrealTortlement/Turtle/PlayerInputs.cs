using System;
using UnityEngine;

namespace UnrealTortlement.Turtle
{
    [Serializable]
    public struct PlayerInputs
    {
        //Settings
        public float AimSensitivity;

        //Movement
        public string Horizontal;
        public string Vertical;
        public string Yaw;
        public string Pitch;
        public string Jump;

        //Weapons
        public string Fire;

        //UI

    }
}
