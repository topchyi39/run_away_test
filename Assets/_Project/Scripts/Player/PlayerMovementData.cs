using UnityEngine;

namespace Scripts.Player
{
    [CreateAssetMenu(fileName = "PlayerMovementData", menuName = "Player/Movement/Data", order = 0)]
    public class PlayerMovementData : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: Header("Rotation")]
        [field: SerializeField] public bool LerpRotationOnStart { get; private set; }
        [field: SerializeField] public float LerpRotationDuration { get; private set; }
    }
}