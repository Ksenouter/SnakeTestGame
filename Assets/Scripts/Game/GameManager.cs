using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private float startGameSpeed = 10;
        [SerializeField] private int crystalsRate = 10;

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
        
        private int _crystalsCount;
        public int CrystalsCount
        {
            get => _crystalsCount;
            private set
            {
                _crystalsCount = value;
                Events.CrystalsCountChanged.Publish(value);
            }
        }
        
        private int _killsCount;
        public int KillsCount
        {
            get => _killsCount;
            private set
            {
                _killsCount = value;
                Events.KillsCountChanged.Publish(value);
            }
        }
        
        #region MonoBehaviour CallBacks

        private void Awake()
        {
            Application.targetFrameRate = TargetFrameRate;
            
            Instance = this;
            Speed = startGameSpeed;
        }
        
        #endregion
        
        #region Interface

        public void AddCrystals(int count = 1)
        {
            CrystalsCount += count * crystalsRate;
        }
        
        public void AddKills(int count = 1)
        {
            KillsCount += count;
        }

        public void ClearCrystals()
        {
            CrystalsCount = 0;
        }

        public void GameOver()
        {
            Speed = 0;
            Events.GameOver.Publish();
        }

        public void RestartGame()
        {
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
        
        #endregion

        private const int TargetFrameRate = 60;

    }
}
