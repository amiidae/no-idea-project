using System;
using UnityEngine;

namespace Bnny.Scripts.Data
{
    public static class DataExtensions
    {
        public static Vector3Data ToVector3Data(this Vector3 vector3)
        {
            return new Vector3Data(vector3.x, vector3.y, vector3.z);
        }

        // why to make it a monkeyPatch and not a class method
        public static Vector3 ToUnityVector3(this Vector3Data vector3Data)
        {
            return new Vector3(vector3Data.X, vector3Data.Y, vector3Data.Z);
        }
    }
}
