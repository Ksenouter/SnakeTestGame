using Colors;
using Game;
using Road.Objects;
using UnityEngine;

namespace Persons
{
    public class PersonController : MonoBehaviour, IEdibleObject
    {

        public GameColor CurrentColor { get; private set; }

        private MeshRenderer _meshRenderer;
        
        #region MonoBehaviour CallBacks

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }
        
        #endregion
        
        #region IEdibleObject CallBacks
        
        public void Eat()
        {
            Events.PersonKilled.Publish();
            GameManager.Instance.AddKills();
            Destroy(gameObject);
        }

        public GameColor GetEatColor()
        {
            return CurrentColor;
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
