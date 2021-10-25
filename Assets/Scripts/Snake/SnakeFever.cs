using System.Collections;
using Game;
using UnityEngine;

namespace Snake
{
    public class SnakeFever : MonoBehaviour
    {

        [SerializeField] private MoveToTouch snakeMoveController;
        [SerializeField] private float feverMultiplySpeed = 3;
        [SerializeField] private float feverTime = 5;
        [SerializeField] private int feverNeedCrystals = 40;

        private bool _feverState;
        
        #region MonoBehaviour CallBacks

        private void Awake()
        {
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
            Game.Events.CrystalsCountChanged.Subscribe(OnGameCrystalsCountChanged);
        }

        private void UnsubscribeFromEvents()
        {
            Game.Events.CrystalsCountChanged.Unsubscribe(OnGameCrystalsCountChanged);
        }

        private void OnGameCrystalsCountChanged(int crystalsCount)
        {
            if (crystalsCount >= feverNeedCrystals)
                ActivateFever();
        }
        
        #endregion
        
        #region IEnumarators

        private IEnumerator WaitAndDeactivateFever(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            DeactivateFever();
        }
        
        #endregion
        
        #region Interface

        private void ActivateFever()
        {
            if (_feverState) return;
            
            GameManager.Instance.ActivateGodMode();
            GameManager.Instance.MultiplySpeed(feverMultiplySpeed);
            
            snakeMoveController.DisableInput();
            snakeMoveController.SetXTargetPosition(0);
            
            StartCoroutine(WaitAndDeactivateFever(feverTime));

            _feverState = true;
        }

        public void DeactivateFever()
        {
            if (!_feverState) return;
            
            GameManager.Instance.DivideSpeed(feverMultiplySpeed);
            GameManager.Instance.DeactivateGodMode();
            GameManager.Instance.ClearCrystals();
            
            snakeMoveController.EnableInput();
            
            _feverState = false;
        }
        
        #endregion
        
    }
}
