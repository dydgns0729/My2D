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
        //플레이어 이동 속도
        [SerializeField] float walkSpeed = 4f;
        //플레이어 이동과 관련된 입력값
        Vector2 inputMove;
        #endregion
        void Awake()
        {
            //참조
            rb2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            //플레이어 좌우 이동
            rb2d.velocity = new Vector2(inputMove.x * walkSpeed, rb2d.velocity.y);

        }

        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();

        }
        


    }
}