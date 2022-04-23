using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    public float speed; //скорость персонажа
    private Rigidbody2D rb;
    private Vector2 moveVelocity; //структура данных для движения персонажа
    public Animator animator;

    private void Start()
    { //подключаемся к компонентам Animator и Rigidbody2D в объекте Player
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        animator.SetFloat("Speed", Mathf.Abs(moveVelocity.x)); //берем модуль скорости по оси x

        if (moveVelocity.x < 0)
        { //поворачиваем спрайт персонажа в зависимости от направления движения
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.F))
        { //анимация удара при нажатии на кнопку F
            animator.SetBool("Strike", true);
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            animator.SetBool("Strike", false);
        }
    }

    private void FixedUpdate()
    { // передвигаем персонажа при помощи физики
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
