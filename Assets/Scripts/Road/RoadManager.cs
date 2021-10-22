using System.Collections.Generic;
using System.Linq;
using Game;
using UnityEngine;

namespace Road
{
    public class RoadManager : MonoBehaviour
    {
        
        [SerializeField] private GameObject roadFragmentPrefab;
        [SerializeField] private float startPositionZ, endPositionZ, fragmentsSpacing;
        
        public static RoadManager Instance { get; private set; }
        
        private readonly List<RoadFragment> _roadFragments = new List<RoadFragment>();
        
        #region MonoBehaviour CallBacks

        private void Awake()
        {
            Instance = this;
            
            SubscribeOnEvents();
        }

        private void Start()
        {
            CreateStartRoad();
        }

        #endregion
        
        #region Private Methods

        private void SubscribeOnEvents()
        {
            Game.Events.GameSpeedChanged.Subscribe(OnGameSpeedChanged);
        }

        private void OnGameSpeedChanged(float gameSpeed)
        {
            foreach (var roadFragment in _roadFragments)
                roadFragment.SetSpeed(gameSpeed);
        }

        private void CreateStartRoad()
        {
            for (var i = endPositionZ; i <= startPositionZ; i += fragmentsSpacing)
                CreateRoadFragment(i);
        }
        
        private void CreateRoadFragment(float zPosition)
        {
            var roadFragment = Instantiate(roadFragmentPrefab, transform).GetComponent<RoadFragment>();
            roadFragment.SetPosition(zPosition);
            roadFragment.SetSpeed(GameManager.Instance.Speed);
            
            _roadFragments.Add(roadFragment);
        }
        
        #endregion
        
        #region Interface

        public void CreateNewRoadFragment()
        {
            CreateRoadFragment(_roadFragments.Last().GetPositionZ() + fragmentsSpacing);
        }

        public void DestroyRoadFragment(RoadFragment fragment)
        {
            _roadFragments.Remove(fragment);
            Destroy(fragment.gameObject);
        }

        #endregion
        
    }
}
