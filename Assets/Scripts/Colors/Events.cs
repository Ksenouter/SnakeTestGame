using Models.Events;
using UnityEngine;

namespace Colors
{
    public static class Events
    {
        
        public static readonly ParamEvent<GameColor> ColorChanged = new ParamEvent<GameColor>();
        
    }
}
