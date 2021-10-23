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
        
        #region MonoBehaviour CallBacks

        private void Awake()
        {
            _targetPosition = transform.position;
            _targetPosition.y = fixedYPosition;
        }

        private void Update()
        {
            MoveToTarget();
        }
        
        #endregion
        
        #region Private Methods

        private void MoveToTarget()
        {
            _targetPosition = _target.TransformPoint(new Vector3(0, 0, targetZPositionBias));
            _targetPosition.y = fixedYPosition;
            
            transform.LookAt(_target.position);
            transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * moveSpeed);
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
