using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f; // Suavização do movimento
    [SerializeField] private bool m_AirControl = false; // Controle no ar, não será mais usado sem o pulo
    [SerializeField] private LayerMask m_WhatIsGround; // Máscara do chão
    [SerializeField] private Transform m_GroundCheck; // Posição para checar se o personagem está no chão

    const float k_GroundedRadius = .2f; // Raio do círculo para verificar se o personagem está no chão
    private bool m_Grounded; // Se o personagem está no chão
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true; // Para determinar a direção que o personagem está virado
    private Vector3 m_Velocity = Vector3.zero;

    private float speedMultiplier = 1f; // Multiplicador de velocidade

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    // Flags para indicar a direção do movimento
    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    private bool isMovingUp = false;
    private bool isMovingDown = false;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // Verifica se o personagem está no chão com base no GroundCheck
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    // Move agora só lida com movimento horizontal e vertical, sem pulo
    public void Move(float horizontalMove, float verticalMove)
    {
        // Detecta se o Shift está pressionado
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speedMultiplier = 1.35f; // Aumenta a velocidade em 20%
        }
        else
        {
            speedMultiplier = 1f; // Velocidade normal
        }

        // Controla o personagem apenas se estiver no chão ou se o controle no ar estiver ativado
        if (m_Grounded || m_AirControl)
        {
            // Encontra a velocidade alvo para o movimento horizontal e vertical
            Vector2 targetVelocity = new Vector2(horizontalMove * 10f * speedMultiplier, verticalMove * 10f * speedMultiplier);

            // Suaviza o movimento e aplica no Rigidbody
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // Verifica a direção do personagem com base no movimento horizontal
            if (horizontalMove > 0)
            {
                if (!m_FacingRight) Flip();
                isMovingRight = true;
                isMovingLeft = false;
            }
            else if (horizontalMove < 0)
            {
                if (m_FacingRight) Flip();
                isMovingRight = false;
                isMovingLeft = true;
            }
            else
            {
                isMovingRight = false;
                isMovingLeft = false;
            }

            // Verifica a direção do personagem com base no movimento vertical
            if (verticalMove > 0)
            {
                isMovingUp = true;
                isMovingDown = false;
            }
            else if (verticalMove < 0)
            {
                isMovingUp = false;
                isMovingDown = true;
            }
            else
            {
                isMovingUp = false;
                isMovingDown = false;
            }
        }
    }

    // Método para inverter a direção do personagem
    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        // Multiplica a escala X do personagem por -1 para virar o sprite
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Funções para verificar o estado do movimento
    public bool IsMovingLeft()
    {
        return isMovingLeft;
    }

    public bool IsMovingRight()
    {
        return isMovingRight;
    }

    public bool IsMovingUp()
    {
        return isMovingUp;
    }

    public bool IsMovingDown()
    {
        return isMovingDown;
    }
}
