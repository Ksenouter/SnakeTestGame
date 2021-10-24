using Colors;
using UnityEngine;

namespace Persons
{
    public class PersonController : MonoBehaviour
    {

        public GameColor CurrentColor { get; private set; }

        private MeshRenderer _meshRenderer;
        
        #region MonoBehaviour CallBacks

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }
        
        #endregion
        
        #region Interface

        public void SetGameColor(GameColor color)
        {
            CurrentColor = color;
            _meshRenderer.material.color = color.Color;
        }
        
        #endregion

    }
}
