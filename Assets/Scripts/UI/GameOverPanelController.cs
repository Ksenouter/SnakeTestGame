using Game;
using UnityEngine;

namespace UI
{
    public class GameOverPanelController : MonoBehaviour
    {

        [SerializeField] private GameObject panel;
        
        #region MonoBehaviour CallBacks

        private void Awake()
        {
            panel.SetActive(false);
            
            SubscribeOnEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        #endregion
        
        #region Private Methods

        private void SubscribeOnEvents()
        {
            Game.Events.GameOver.Subscribe(ShowPanel);
        }

        private void UnsubscribeFromEvents()
        {
            Game.Events.GameOver.Unsubscribe(ShowPanel);
        }

        private void ShowPanel()
        {
            panel.SetActive(true);
        }
        
        #endregion
        
        #region Public Methods

        public void OnClickRestartButton()
        {
            GameManager.Instance.RestartGame();
        }
        
        #endregion

    }
}
