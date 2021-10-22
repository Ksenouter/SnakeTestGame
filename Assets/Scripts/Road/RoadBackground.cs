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
        
        private float _scrollSpeed = 5;
        
        #region MonoBehaviour CallBacks

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
            _offsetVector = new Vector2();

            SubscribeOnEvents();
        }

        private void Start()
        {
            _scrollSpeed = GameManager.Instance.Speed;
        }

        private void Update()
        {
            _offsetVector.y -= Time.deltaTime / scrollSpeedRatio * _scrollSpeed;
            if (_offsetVector.y < 0) _offsetVector.y += scrollMaxOffset;
            
            _material.SetTextureOffset(MainTex, _offsetVector);
        }

        #endregion
        
        #region Private Methods

        private void SubscribeOnEvents()
        {
            Game.Events.GameSpeedChanged.Subscribe(gameSpeed => _scrollSpeed = gameSpeed);
        }
        
        #endregion

    }
}
