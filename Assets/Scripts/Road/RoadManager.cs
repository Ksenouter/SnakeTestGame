using System.Collections.Generic;
using System.Linq;
using Game;
using UnityEngine;

namespace Road
{
    public class RoadManager : MonoBehaviour
    {
        
        [SerializeField] private float startPositionZ, endPositionZ, fragmentsSpacing;
        [SerializeField] private GameObject emptyRoadFragment;
        [SerializeField] private byte startEmptyRoadFragments;
        [SerializeField] private GameObject[] roadFragmentsQueue;
        
        public static RoadManager Instance { get; private set; }
        
        private readonly List<RoadFragment> _roadFragments = new List<RoadFragment>();
        private byte _roadFragmentsQueueStep;
        
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
                CreateQueueRoadFragment(i);
        }
        
        private void CreateRoadFragment(float zPosition, GameObject prefab)
        {
            var roadFragment = Instantiate(prefab, transform).GetComponent<RoadFragment>();
            roadFragment.SetPosition(zPosition);
            roadFragment.SetSpeed(GameManager.Instance.Speed);
            
            _roadFragments.Add(roadFragment);
        }

        private void CreateQueueRoadFragment(float zPosition)
        {
            if (startEmptyRoadFragments > 0)
            {
                CreateRoadFragment(zPosition, emptyRoadFragment);
                startEmptyRoadFragments--;
                return;
            }
            
            CreateRoadFragment(zPosition, roadFragmentsQueue[_roadFragmentsQueueStep]);
            
            _roadFragmentsQueueStep++;
            if (_roadFragmentsQueueStep >= roadFragmentsQueue.Length)
                _roadFragmentsQueueStep = 0;
        }

        #endregion
        
        #region Interface

        public void CreateNewRoadFragment()
        {
            CreateQueueRoadFragment(_roadFragments.Last().GetPositionZ() + fragmentsSpacing);
        }

        public void DestroyRoadFragment(RoadFragment fragment)
        {
            _roadFragments.Remove(fragment);
            Destroy(fragment.gameObject);
        }

        #endregion
        
    }
}
