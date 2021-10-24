using UnityEngine;

namespace UI
{
    public class KillText : MonoBehaviour
    {

        [SerializeField] private float lifeTime, moveSpeed;
        [SerializeField] private float startPositionBias;

        private RectTransform _rectTransform, _canvas;
        private Transform _target;
        private Camera _camera;
        
        #region MonoBehaviour CallBacks
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _camera = Camera.main;
            
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            UpdatePosition(false);
            
            _rectTransform.Translate(Vector2.up * (moveSpeed * Time.deltaTime));
        }

        private void UpdatePosition(bool updateYPosition = true)
        {
            var viewportPosition = _camera.WorldToViewportPoint(_target.position);
            var canvasSizeDelta = _canvas.sizeDelta;
            var screenPosition = new Vector2(
                viewportPosition.x * canvasSizeDelta.x - canvasSizeDelta.x * 0.5f,
                viewportPosition.y * canvasSizeDelta.y - canvasSizeDelta.y * 0.5f);
            if (!updateYPosition) screenPosition.y = _rectTransform.anchoredPosition.y;

            _rectTransform.anchoredPosition = screenPosition;
        }
        
        #endregion
        
        #region Interface

        public void SetData(RectTransform canvas, Transform target)
        {
            _canvas = canvas;
            _target = target;

            UpdatePosition();
            _rectTransform.anchoredPosition += new Vector2(0, startPositionBias);
        }
        
        #endregion
        
    }
}
