using Colors;
using Game;
using UnityEngine;

namespace Road.Objects
{
    public class RoadCrystal : MonoBehaviour, IEdibleObject
    {
        
        public void Eat()
        {
            GameManager.Instance.AddCrystals();
            Destroy(gameObject);
        }

        public GameColor GetEatColor()
        {
            return null;
        }
        
    }
}
