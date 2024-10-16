using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f; // Suavização do movimento
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true; // Para determinar a direção que o personagem está virado
	private Vector3 m_Velocity = Vector3.zero;

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

	// Move agora só lida com movimento horizontal e vertical
	public void Move(float horizontalMove, float verticalMove)
	{
		// Encontra a velocidade alvo para o movimento horizontal e vertical
		Vector2 targetVelocity = new Vector2(horizontalMove * 10f, verticalMove * 10f);

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
