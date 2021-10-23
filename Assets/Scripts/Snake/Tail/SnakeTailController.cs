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
        
        private readonly List<SnakeTailElement> _tailElements = new List<SnakeTailElement>();
        
        #region MonoBehaviour CallBacks

        private void Start()
        {
            for (var i = 0; i < startTailElementsCount; i++)
                CreateTailElement();
        }

        #endregion
        
        #region Private Methods

        private void CreateTailElement()
        {
            var tailZPosition = (startTailBias + _tailElements.Count * tailBias) * -1;
            var tailTargetElement = _tailElements.Count == 0 ? snakeHead : _tailElements.Last().transform;
            var tailPosition = tailTargetElement.position;
            tailPosition.z = tailZPosition;

            var tailObject = Instantiate(tailPrefab, tailPosition, Quaternion.identity, tailParent);
            var tailElement = tailObject.GetComponent<SnakeTailElement>();
            tailElement.SetTargetElement(tailTargetElement);

            if (tailSpeedBias != 0)
            {
                var tailElements = _tailElements.Count;
                if (tailElements > 1)
                {
                    var tailSpeed = _tailElements[tailElements - 2].GetMoveSpeed() + tailSpeedBias;
                    tailElement.SetMoveSpeed(tailSpeed);
                }
            }

            _tailElements.Add(tailElement);
        }
        
        #endregion

    }
}
