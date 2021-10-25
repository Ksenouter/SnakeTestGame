using Colors;
using UnityEngine;

namespace Snake
{
    public class SnakeElement : MonoBehaviour
    {

        public GameColor CurrentColor { get; private set; }

        private MeshRenderer _meshRenderer;
        
        #region MonoBehaviour CallBacks

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            UpdateColor();
        }

        private void Start()
        {
            if (CurrentColor == null)
                SetColor(ColorsManager.Instance.CurrentColor);
        }
        
        #endregion
        
        #region Private Methods

        private void UpdateColor()
        {
            if (CurrentColor != null && _meshRenderer != null)
                _meshRenderer.material.color = CurrentColor.Color;
        }
        
        #endregion
        
        #region Interface

        public void SetColor(GameColor color)
        {
            CurrentColor = color;
            UpdateColor();
        }
        
        #endregion
        
    }
}
