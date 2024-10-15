using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	
	public CharacterController2D controller;
	public Animator animator;
	public float runSpeed = 40f;
	float horizontalMove = 0f;
	float verticalMove = 0f;

	// Update é chamado a cada frame
	
	void Update()
	
	{
		// Obtém o movimento horizontal e vertical
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;

		// Verifica o estado de movimento horizontal e vertical e ajusta as animações
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove) + Mathf.Abs(verticalMove));

		// Atualiza as animações de movimento para esquerda, direita, cima e baixo
		animator.SetBool("isMovingLeft", controller.IsMovingLeft());
		animator.SetBool("isMovingRight", controller.IsMovingRight());
		animator.SetBool("isMovingUp", controller.IsMovingUp());
		animator.SetBool("isMovingDown", controller.IsMovingDown());
	}

	void FixedUpdate()
	{
		// Move o personagem com base no movimento calculado
		controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime);
	}
}