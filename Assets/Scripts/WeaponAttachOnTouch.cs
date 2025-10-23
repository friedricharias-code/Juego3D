using UnityEngine;

public class WeaponAttachOnTouch : MonoBehaviour
{
    public Transform weaponHolder;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            // Desactiva físicas del arma
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }

            Collider col = other.GetComponent<Collider>();
            if (col)
            {
                col.enabled = false;
            }

            // Pega el arma a la mano
            other.transform.SetParent(weaponHolder);
            other.transform.localPosition = Vector3.zero;
            other.transform.localRotation = Quaternion.identity;

            // Activa animaciones de "con arma"
            anim.SetBool("HasWeapon", true);
        }
    }
    void DropWeapon()
    {
        anim.SetBool("HasWeapon", false);
    }
}
