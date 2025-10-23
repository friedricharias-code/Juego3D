using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponFire : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
            animator = GetComponent<Animator>();
    }

    // Este método lo llamaremos desde el evento del Player Input
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed) // solo cuando se presiona
        {
            Shoot();
        }
    }

    void Shoot()
    {
        animator.SetTrigger("Golpe");
    }
}

