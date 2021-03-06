using Game;
using UnityEngine;

namespace Snake.Tail
{
    public class SnakeTailElement : MonoBehaviour
    {

        [SerializeField] private float fixedYPosition;
        [SerializeField] private float moveSpeed, minMoveSpeed, maxMoveSpeed;
        [SerializeField] private float targetZLimitation;
        
        private Transform _target;
        private Vector3 _targetPosition;
        private float _targetPositionZBias;
        private float _gameSpeed;
        
        #region MonoBehaviour CallBacks

        private void Awake()
        {
            _targetPosition = transform.position;
            _targetPosition.y = fixedYPosition;
            
            _gameSpeed = GameManager.Instance != null ? GameManager.Instance.Speed : 0;
            
            SubscribeOnEvents();
        }

        private void FixedUpdate()
        {
            MoveToTarget();
        }
        
        #endregion
        
        #region Private Methods
        
        private void SubscribeOnEvents()
        {
            Game.Events.GameSpeedChanged.Subscribe(gameSpeed => _gameSpeed = gameSpeed);
        }
        
        private void MoveToTarget()
        {
            _targetPosition = _target.TransformPoint(new Vector3(0, 0, _targetPositionZBias));
            _targetPosition.y = fixedYPosition;

            if (targetZLimitation >= 0)
            {
                var zLimitation = _target.transform.position.z - targetZLimitation;
                if (_targetPosition.z > zLimitation)
                    _targetPosition.z = zLimitation;
            }

            transform.LookAt(_target.position);
            var timeMoveSpeed = moveSpeed * _gameSpeed * Time.fixedDeltaTime;
            transform.position = Vector3.Lerp(transform.position, _targetPosition, timeMoveSpeed);
        }
        
        #endregion
        
        #region Interface

        public void SetTargetElement(Transform target)
        {
            _target = target;
        }

        public float GetMoveSpeed()
        {
            return moveSpeed;
        }
        
        public void SetMoveSpeed(float speed)
        {
            moveSpeed = Mathf.Clamp(speed, minMoveSpeed, maxMoveSpeed);
        }

        public void SetTargetPositionZBias(float bias)
        {
            _targetPositionZBias = bias * -1;
        }

        #endregion

    }
}
