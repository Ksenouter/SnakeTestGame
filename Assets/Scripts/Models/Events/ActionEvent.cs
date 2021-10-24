using System;
using System.Collections.Generic;

namespace Models.Events
{
    public class ActionEvent
    {
        
        private readonly List<Action> _callbacks = new List<Action>();

        public void Subscribe(Action callback)
        {
            _callbacks.Add(callback);
        }

        public void Publish()
        {
            foreach (var callback in _callbacks)
                callback();
        }

        public void Unsubscribe(Action callback)
        {
            _callbacks.Remove(callback);
        }
        
    }
}
