using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementInput;
    public float velocidad = 5f;
    private Vector2 inputSuave;
    public float suavizarMovimiento = 0.1f;
    [SerializeField] private Transform posicionDetectorSuelo;
    [SerializeField] private LayerMask layerSuelo;
    public float jumpForce = 5f;
    private Animator animator;
    private Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] AudioClip respiraSound;

    [SerializeField] private GameObject golpe;

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && animator.GetBool("enSuelo"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Bloquear movimiento si está curando
        if (animator.GetBool("isHealing"))
            return;
        var checkSuelo = Physics.CheckSphere(posicionDetectorSuelo.position, 0.1f, layerSuelo);
        animator.SetBool("enSuelo", checkSuelo);

        inputSuave = Vector2.Lerp(inputSuave, movementInput, suavizarMovimiento * Time.deltaTime);
        animator.SetFloat("ejeX", inputSuave.x);
        animator.SetFloat("ejeY", inputSuave.y);
        Vector3 movimientoPlayer = new Vector3(movementInput.x, 0, movementInput.y);
        transform.Translate(movimientoPlayer * velocidad * Time.deltaTime);

        audioSource.PlayOneShot(respiraSound);
    }
    public void BloquearMovimiento()
    {
        this.enabled = false;
    }

    public void ActivarMovimiento()
    {
        this.enabled = true;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed) // solo cuando se presiona
        {
            animator.SetTrigger("Golpe");
            
        }
    }

    public void dessactivarCollider()
    {
        golpe.GetComponent<Collider>().enabled = false;
    }
    public void activarCollider()
    {
        golpe.GetComponent<Collider>().enabled = true;
    }

}
