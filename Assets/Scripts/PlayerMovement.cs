using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    [Header("Movimiento")]
    private Vector2 movementInput;
    public float velocidad = 5f;
    public float velocidadCorrer = 9f; // 🔹 Nueva variable para velocidad de carrera
    private bool estaCorriendo = false; // 🔹 Indica si está corriendo
    private Vector2 inputSuave;
    public float suavizarMovimiento = 0.1f;

    [Header("Salto")]
    [SerializeField] private Transform posicionDetectorSuelo;
    [SerializeField] private LayerMask layerSuelo;
    public float jumpForce = 5f;

    [Header("Audio")]
    AudioSource audioSource;
    [SerializeField] AudioClip respiraSound;
    private float tiempoUltimaRespiracion;
    private float intervaloRespiracion;

    [Header("Ataque")]
    [SerializeField] private GameObject golpe;

    // 🔹 Detecta movimiento del joystick o teclas
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    // 🔹 Detecta salto
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && animator.GetBool("enSuelo"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // 🔹 Detecta cuándo se presiona o suelta Shift (acción "Run" en Input System)
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
            estaCorriendo = true;
        else if (context.canceled)
            estaCorriendo = false;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        intervaloRespiracion = respiraSound.length + 0.5f;
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (animator.GetBool("isHealing"))
            return;

        var checkSuelo = Physics.CheckSphere(posicionDetectorSuelo.position, 0.1f, layerSuelo);
        animator.SetBool("enSuelo", checkSuelo);

        // 🔹 Actualizar animaciones
        inputSuave = Vector2.Lerp(inputSuave, movementInput, suavizarMovimiento * Time.deltaTime);
        animator.SetFloat("ejeX", inputSuave.x);
        animator.SetFloat("ejeY", inputSuave.y);
        animator.SetBool("isRunning", estaCorriendo && movementInput.magnitude > 0); // 🔹 Nueva animación de correr

        // 🔹 Audio de respiración
        if (!audioSource.isPlaying && Time.time - tiempoUltimaRespiracion >= intervaloRespiracion)
        {
            audioSource.PlayOneShot(respiraSound);
            tiempoUltimaRespiracion = Time.time;
        }
    }

    void FixedUpdate()
    {
        if (animator.GetBool("isHealing"))
            return;

        Vector3 currentVelocity = rb.linearVelocity;

        // 🔹 Calcular dirección de movimiento
        Vector3 targetVelocity = new Vector3(movementInput.x, 0, movementInput.y);
        targetVelocity = transform.TransformDirection(targetVelocity);

        // 🔹 Cambiar velocidad si está corriendo
        float velocidadActual = estaCorriendo ? velocidadCorrer : velocidad;
        targetVelocity *= velocidadActual;

        Vector3 velocityChange = targetVelocity - new Vector3(currentVelocity.x, 0, currentVelocity.z);
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("puerta"))
        {
            animator.SetTrigger("abrir");
        }
        if (other.CompareTag("Victoria"))
        {
            animator.SetTrigger("win");
            this.enabled = false;
        }
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
        if (context.performed)
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
