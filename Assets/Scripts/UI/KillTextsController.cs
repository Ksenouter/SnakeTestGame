using UnityEngine;

namespace UI
{
    public class KillTextsController : MonoBehaviour
    {

        [SerializeField] private GameObject killTextPrefab;
        [SerializeField] private RectTransform canvas;
        [SerializeField] private Transform snakeHead;

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
            Persons.Events.PersonKilled.Subscribe(CreateKillText);
        }

        private void UnsubscribeFromEvents()
        {
            Persons.Events.PersonKilled.Unsubscribe(CreateKillText);
        }

        private void CreateKillText()
        {
            var killText = Instantiate(killTextPrefab, transform).GetComponent<KillText>();
            killText.SetData(canvas, snakeHead);
        }
        
        #endregion

    }
}
