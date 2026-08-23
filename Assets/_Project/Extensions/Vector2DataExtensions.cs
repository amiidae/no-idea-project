using UnityEngine;

namespace Extensions
{
    public static class Vector2DataExtensions
    {
        public static Vector2 ToUnityVector(this Vector2Data vector2Data)
        {
            return new Vector2(vector2Data.X, vector2Data.Y);
        }
        
        public static Vector2Data ToVector2Data(this Vector2 vec2)
        {
            return new Vector2Data()
            {
                X = vec2.x,
                Y = vec2.y
            };
        }
    }
}