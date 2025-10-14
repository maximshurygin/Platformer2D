using UnityEngine;
using Weapon;

public class AnimationEventRelay : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerWeapon _playerWeapon;

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
        
}
