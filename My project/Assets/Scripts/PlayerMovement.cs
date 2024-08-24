using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    public Rigidbody2D rb;
    Vector2 movement;

    public Animator animator;
    private SpriteRenderer spriteRenderer;
    public float movementThreshold = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("movementX", movement.x);
        animator.SetFloat("movementY", movement.y);

        // Verifica se o personagem está se movendo
        if (Mathf.Abs(movement.x) > movementThreshold || Mathf.Abs(movement.y) > movementThreshold)
        {
            // Se o movimento for maior que a tolerância, ele está andando
            animator.SetFloat("lastMoveX", movement.x);
            animator.SetFloat("lastMoveY", movement.y);

            // Inverte a animação quando o personagem se mover para a direita ou esquerda
            spriteRenderer.flipX = movement.x < 0;
        }
        else
        {
            // Se o movimento for menor ou igual à tolerância, entra no estado de idle
            animator.SetFloat("lastMoveX", movement.x);
            animator.SetFloat("lastMoveY", movement.y);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
