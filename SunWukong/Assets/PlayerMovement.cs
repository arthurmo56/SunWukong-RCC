using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;

    // Update is called once per frame
    void Update()
    {
        // Obtenha o movimento horizontal e vertical
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;

        // Verifica se o botão de pulo foi pressionado
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        // Chame o método Move, incluindo tanto o movimento horizontal quanto o vertical
        controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime, jump);

        // Após o pulo, resete o valor de 'jump' para evitar múltiplos pulos seguidos
        jump = false;
    }
}
