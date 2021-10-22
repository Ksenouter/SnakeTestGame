using UnityEngine;

namespace Snake
{
    public class MoveToTouch : MonoBehaviour
    {

        [SerializeField] private float fixedYPosition, fixedZPosition;
        [SerializeField] private float moveSpeed = 30;
        
        private Camera _mainCamera;
        private Vector3 _targetPosition;
        
        private bool _inputState;

        #region MonoBehaviour CallBacks

        private void Awake()
        {
            _mainCamera = Camera.main;
            
            _targetPosition = new Vector3(0, fixedYPosition, fixedZPosition);
            _inputState = true;
        }

        private void Update()
        {
            GetInput();
            MoveToTargetPosition();
        }

        #endregion
        
        #region Private Methods

        private void GetInput()
        {
            if (!_inputState) return;
            
            #if UNITY_EDITOR
                if (!Input.GetMouseButton(0)) return;
                var inputPosition = Input.mousePosition;
            #else
                if (Input.touchCount < 1) return;
                var inputPosition = Input.GetTouch(0).position;
            #endif
            
            var cameraRay = _mainCamera.ScreenPointToRay(inputPosition);
            if (!Physics.Raycast(cameraRay, out var hit)) return;
            
            _targetPosition.x = hit.point.x;
        }

        private void MoveToTargetPosition()
        {
            if (transform.position == _targetPosition) return;

            var moveTowardsSpeed = Time.deltaTime * moveSpeed;
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, moveTowardsSpeed);
        }
        
        #endregion
        
        #region Interface

        public void DisableInput()
        {
            _inputState = false;
        }

        public void EnableInput()
        {
            _inputState = true;
        }

        public void SetTargetPosition(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
        }
        
        #endregion
        
    }
}
