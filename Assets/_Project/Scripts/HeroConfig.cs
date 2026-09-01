using UnityEngine;

namespace Bnny.Scripts
{
    [CreateAssetMenu(fileName = "HeroConfig", menuName = "Project/HeroConfig")]
    public class HeroConfig : ScriptableObject
    {
        public HeroData HeroData;
    }
}
