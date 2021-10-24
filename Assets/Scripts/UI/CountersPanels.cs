using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CountersPanels : MonoBehaviour
    {

        [SerializeField] private RectTransform crystalsPanel, killsPanel;
        [SerializeField] private float panelWidth, panelHeight, numPanelWidth;
        [SerializeField] private Text crystalsText, killsText;
        
        #region MonoBehaviour CallBacks

        private void Awake()
        {
            UpdateCrystalsCounter(0);
            UpdateKillsCounter(0);
            
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
            Game.Events.CrystalsCountChanged.Subscribe(UpdateCrystalsCounter);
            Game.Events.KillsCountChanged.Subscribe(UpdateKillsCounter);
        }

        private void UnsubscribeFromEvents()
        {
            Game.Events.CrystalsCountChanged.Unsubscribe(UpdateCrystalsCounter);
            Game.Events.KillsCountChanged.Unsubscribe(UpdateKillsCounter);
        }

        private void UpdateCrystalsCounter(int count)
        {
            SetPanelSize(crystalsPanel, count);
            crystalsText.text = count.ToString();
        }
        
        private void UpdateKillsCounter(int count)
        {
            SetPanelSize(killsPanel, count);
            killsText.text = count.ToString();
        }

        private void SetPanelSize(RectTransform panel, int num)
        {
            var digitCount = num == 0 ? 1 : (int)Mathf.Log10(num) + 1;
            panel.sizeDelta = new Vector2(panelWidth + digitCount * numPanelWidth, panelHeight);
        }
        
        #endregion

    }
}
