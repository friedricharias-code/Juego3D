using UnityEngine;

public class WeaponAttachOnTouch : MonoBehaviour
{
    public Transform weaponHolder;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            // Desactiva físicas ANTES de mover el arma
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = true;   // evita que afecte al jugador
                rb.useGravity = false;
            }

            Collider col = other.GetComponent<Collider>();
            if (col)
            {
                col.enabled = false;     // evita colisiones futuras
            }

            // Ahora sí, pega el arma a la mano
            other.transform.SetParent(weaponHolder);
            other.transform.localPosition = Vector3.zero;
            other.transform.localRotation = Quaternion.identity;
        }
    }
}
