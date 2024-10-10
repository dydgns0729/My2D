using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        Rigidbody2D rb2d;
        //�÷��̾� �̵� �ӵ�
        [SerializeField] float walkSpeed = 4f;
        //�÷��̾� �̵��� ���õ� �Է°�
        Vector2 inputMove;
        #endregion
        void Awake()
        {
            //����
            rb2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            //�÷��̾� �¿� �̵�
            rb2d.velocity = new Vector2(inputMove.x * walkSpeed, rb2d.velocity.y);

        }

        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();

        }
        


    }
}