using System;
using UnityEngine;

namespace Scripts.Enemies
{
    
    [SelectionBase]
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] private KillType killType;
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                PlayerTriggered(player);
            }
        }

        protected virtual void PlayerTriggered(Player.Player component)
        {
            component.Kill(killType.ToString());
        }
        
        private enum KillType
        {
            Exploded,
            Caught
        }
    }
}