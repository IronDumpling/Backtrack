using UnityEngine;
using System.Collections;

namespace Backtrack.Core
{
    /// <summary>
    /// The event is triggered when the player wins a level.
    /// </summary>
    [CreateAssetMenu(fileName = nameof(LevelWinEvent),
        menuName = "Runner/" + nameof(LevelWinEvent))]
    public class LevelWinEvent : AbstractGameEvent
    {
        public override void Reset()
        {

        }
    }
}


