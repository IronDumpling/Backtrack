using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Backtrack.Core
{
    public abstract class AbstractGameEvent : ScriptableObject
    {
        readonly List<IGameEventListener> m_EventListeners = new();

        // Triggers the current event instance and notifies the subscribers
        public void Raise()
        {
            for (int i = m_EventListeners.Count - 1; i >= 0; i--)
                m_EventListeners[i].OnEventRaised();
            Reset();
        }

        // Adds a class to the list of observers for this event
        // The class that wants to observe this event
        public void AddListener(IGameEventListener listener)
        {
            if (!m_EventListeners.Contains(listener))
            {
                m_EventListeners.Add(listener);
            }
        }

        // Removes a class from the list of observers for this event
        // The class that doesn't want to observe this event anymore
        public void RemoveListener(IGameEventListener listener)
        {
            if (m_EventListeners.Contains(listener))
            {
                m_EventListeners.Remove(listener);
            }
        }

        // Each event resets immediately after it's triggered.
        // This method contains the reset logic for the derived classes.
        public abstract void Reset();
    }

}

