using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Backtrack.Core
{
    public interface IGameEventListener
    {
        // The event handler that is called when the subscribed event is triggered
        void OnEventRaised();
    }
}
