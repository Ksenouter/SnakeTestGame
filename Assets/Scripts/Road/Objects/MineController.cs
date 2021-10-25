using Colors;
using UnityEngine;

namespace Road.Objects
{
    public class MineController : MonoBehaviour, IEdibleObject
    {
        
        #region IEdible CallBacks
        
        public void Eat()
        {
            Destroy(gameObject);
        }

        public GameColor GetEatColor()
        {
            return null;
        }
        
        #endregion
        
    }
}
