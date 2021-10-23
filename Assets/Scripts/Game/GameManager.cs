using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance { get; private set; }

        private float _speed;
        public float Speed
        {
            get => _speed;
            private set
            {
                _speed = value;
                Events.GameSpeedChanged.Publish(value);
            }
        }
        
        #region MonoBehaviour CallBacks

        private void Awake()
        {
            Instance = this;
            
            // TODO: temp value
            Speed = 10f;
        }
        
        #endregion
        
    }
}
