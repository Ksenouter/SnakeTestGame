using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Snake.Tail
{
    public class SnakeTailController : MonoBehaviour
    {
        
        [SerializeField] private Transform snakeHead, tailParent;
        [SerializeField] private GameObject tailPrefab;
        [SerializeField] private float startTailBias, tailBias;
        [SerializeField] private float tailSpeedBias;
        [SerializeField] private byte startTailElementsCount;
        [SerializeField] private byte maxTailElements;
        [SerializeField] private int tailNeedKills;
        
        private readonly List<SnakeTailElement> _tailElements = new List<SnakeTailElement>();
        private int _tailKillsCounter;
        
        #region MonoBehaviour CallBacks

        private void Awake()
        {
            SubscribeOnEvents();
        }

        private void Start()
        {
            for (var i = 0; i < startTailElementsCount; i++)
                CreateTailElement();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        #endregion
        
        #region Private Methods

        private void SubscribeOnEvents()
        {
            Persons.Events.PersonKilled.Subscribe(OnPersonKilled);
        }

        private void UnsubscribeFromEvents()
        {
            Persons.Events.PersonKilled.Unsubscribe(OnPersonKilled);
        }

        private void OnPersonKilled()
        {
            if (_tailElements.Count >= maxTailElements) return;
            
            _tailKillsCounter++;
            if (_tailKillsCounter < tailNeedKills) return;
            _tailKillsCounter -= tailNeedKills;
            CreateTailElement();
        }
        
        private void CreateTailElement()
        {
            var tailElements = _tailElements.Count;
            if (tailElements >= maxTailElements) return;
            
            var tailZPosition = (startTailBias + _tailElements.Count * tailBias) * -1;
            var tailTargetElement = _tailElements.Count == 0 ? snakeHead : _tailElements.Last().transform;
            var tailPosition = tailTargetElement.position;
            tailPosition.z = tailZPosition;
            
            var tailObject = Instantiate(tailPrefab, tailPosition, Quaternion.identity, tailParent);
            var tailElement = tailObject.GetComponent<SnakeTailElement>();
            tailElement.SetTargetElement(tailTargetElement);
            tailElement.SetTargetPositionZBias(tailElements == 0 ? startTailBias : tailBias);

            if (tailSpeedBias != 0 && tailElements > 1)
            {
                var tailSpeed = _tailElements[tailElements - 2].GetMoveSpeed() + tailSpeedBias;
                tailElement.SetMoveSpeed(tailSpeed);
            }

            if (tailElements >= startTailElementsCount)
            {
                var parentColor = tailTargetElement.GetComponent<SnakeElement>().CurrentColor;
                tailElement.GetComponent<SnakeElement>().SetColor(parentColor);
            }

            _tailElements.Add(tailElement);
        }
        
        #endregion

    }
}
