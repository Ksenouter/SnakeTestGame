using Models.Events;
using UnityEngine;

namespace Game
{
    public class Events : MonoBehaviour
    {
        
        public static readonly ParamEvent<float> GameSpeedChanged = new ParamEvent<float>();
        public static readonly ParamEvent<int> KillsCountChanged = new ParamEvent<int>();
        public static readonly ParamEvent<int> CrystalsCountChanged = new ParamEvent<int>();
        public static readonly ActionEvent GameOver = new ActionEvent();
        
    }
}
