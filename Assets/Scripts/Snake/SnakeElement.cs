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
        }
        
        private void Start()
        {
            SetColor(ColorsManager.Instance.CurrentColor);
        }
        
        #endregion
        
        #region Interface

        public void SetColor(GameColor color)
        {
            CurrentColor = color;
            _meshRenderer.material.color = color.Color;
        }
        
        #endregion
        
    }
}
