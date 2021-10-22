using Models.Events;
using UnityEngine;

namespace Game
{
    public class Events : MonoBehaviour
    {
        
        public static readonly ParamEvent<float> GameSpeedChanged = new ParamEvent<float>();
        
    }
}
