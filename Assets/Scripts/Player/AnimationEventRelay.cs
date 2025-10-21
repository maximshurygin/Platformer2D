using UnityEngine;
using Weapon;

public class AnimationEventRelay : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerWeapon _playerWeapon;
    [SerializeField] private AudioSource _whooshAudioSource;
    [SerializeField] private AudioSource _stepAudioSource;


    private void AttackStart()
    {
        _playerWeapon?.EnableCollider();
    }
    
    private void AttackEnd()
    {
        _playerController?.OnAttackEnd();
    }

    private void HurtEnd()
    {
        _playerController?.OnHurtEnd();
    }

    private void WhooshSound()
    {
        _whooshAudioSource.Play();
    }

    private void StepSound()
    {
        _stepAudioSource.Play();
    }
        
}
