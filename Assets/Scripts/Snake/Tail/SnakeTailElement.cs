using Game;
using UnityEngine;

namespace Snake.Tail
{
    public class SnakeTailElement : MonoBehaviour
    {

        [SerializeField] private float fixedYPosition;
        [SerializeField] private float moveSpeed, minMoveSpeed, maxMoveSpeed;
        [SerializeField] private float targetZPositionBias;
        
        private Transform _target;
        private Vector3 _targetPosition;
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
            _targetPosition = _target.TransformPoint(new Vector3(0, 0, targetZPositionBias));
            _targetPosition.y = fixedYPosition;
            
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

        #endregion

    }
}
