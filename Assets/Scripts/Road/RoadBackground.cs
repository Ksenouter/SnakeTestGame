using Game;
using UnityEngine;

namespace Road
{
    public class RoadBackground : MonoBehaviour
    {

        [SerializeField] private float scrollSpeedRatio = 14;
        [SerializeField] private float scrollMaxOffset = 10;
        
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        
        private Material _material;
        private Vector2 _offsetVector;
        
        private float _gameSpeed;
        
        #region MonoBehaviour CallBacks

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
            _offsetVector = new Vector2();
            
            _gameSpeed = GameManager.Instance != null ? GameManager.Instance.Speed : 0;
            
            SubscribeOnEvents();
        }
        
        private void FixedUpdate()
        {
            _offsetVector.y -= scrollSpeedRatio * _gameSpeed * Time.fixedDeltaTime;
            if (_offsetVector.y < 0) _offsetVector.y += scrollMaxOffset;
            
            _material.SetTextureOffset(MainTex, _offsetVector);
        }

        #endregion
        
        #region Private Methods

        private void SubscribeOnEvents()
        {
            Game.Events.GameSpeedChanged.Subscribe(gameSpeed => _gameSpeed = gameSpeed);
        }
        
        #endregion

    }
}
