using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        private GameManager manager;

        public bool IsAlive;

        void Start()
        {
            IsAlive = true;
            manager = GameManager.Instance;
        }
        
        public void Die()
        {
            manager.SwitchToGhostState();
            IsAlive = false;
        }
    }
}
