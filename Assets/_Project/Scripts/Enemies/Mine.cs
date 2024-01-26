using Cinemachine;
using UnityEngine;

namespace Scripts.Enemies
{
    public class Mine : Enemy
    {
        [SerializeField] private ParticleSystem explosion;
        [SerializeField] private GameObject renderGameObject;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private CinemachineImpulseSource cinemachineImpulseSource;
        
        protected override void PlayerTriggered(Player.Player component)
        {
            base.PlayerTriggered(component);
            renderGameObject.SetActive(false);
            explosion.Play();
            cinemachineImpulseSource.GenerateImpulse();
            audioSource.PlayOneShot(audioClip);
        }
    }
}