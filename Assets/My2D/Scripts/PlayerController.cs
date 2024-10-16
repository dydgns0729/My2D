using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        Rigidbody2D rb2d;
        Animator animator;
        TouchingDirections touchingDirections;

        //플레이어 이동 속도
        [SerializeField] float walkSpeed = 4f;
        [SerializeField] float runSpeed = 8f;
        [SerializeField] float airSpeed = 2f;

        public float CurrentMoveSpeed
        {
            get
            {
                if (CanMove)
                {
                    if (IsMove && touchingDirections.IsWall == false)
                    {
                        if (touchingDirections.IsGround)
                        {
                            if (IsRun)
                            {
                                return runSpeed;
                            }
                            else
                            {
                                return walkSpeed;
                            }
                        }
                        else
                        {
                            return airSpeed;
                        }
                    }
                    else
                    {
                        return 0f;  //idle state
                    }
                }
                else
                {
                    return 0f;  //움직이지 못할때
                }

            }
        }

        //이동여부
        public bool CanMove
        {
            get { return animator.GetBool(AnimationString.CanMove); }
        }

        //플레이어 이동과 관련된 입력값
        Vector2 inputMove;

        //걷기
        [SerializeField]
        private bool isMove;
        public bool IsMove
        {
            get { return isMove; }
            set
            {
                isMove = value;
                animator.SetBool(AnimationString.IsMove, value);
            }
        }

        //뛰기
        [SerializeField] private bool isRun;
        public bool IsRun
        {
            get { return isRun; }
            set
            {
                isRun = value;
                animator.SetBool(AnimationString.IsRun, value);
            }
        }

        //좌우 반전(스케일 컨트롤)
        [SerializeField] private bool isFacingRight;
        public bool IsFacingRight
        {
            get { return isFacingRight; }
            set
            {
                //반전
                if (isFacingRight != value)
                {
                    transform.localScale *= new Vector2(-1, 1);
                }
                isFacingRight = value;
            }
        }

        //점프
        [SerializeField] private float jumpForce = 5f;

        //죽음 체크
        public bool IsDeath
        {
            get { return animator.GetBool(AnimationString.IsDeath); }
        }
        #endregion
        void Awake()
        {
            //참조
            rb2d = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            touchingDirections = GetComponent<TouchingDirections>();
            //초기화
            isMove = false;
            isRun = false;
            isFacingRight = true;
        }

        private void FixedUpdate()
        {
            //플레이어 좌우 이동
            rb2d.velocity = new Vector2(inputMove.x * CurrentMoveSpeed, rb2d.velocity.y);

            //YVelocity 값 조절
            animator.SetFloat(AnimationString.YVelocity, rb2d.velocity.y);
        }

        //바라보는 방향으로 전환(스케일 컨트롤)
        void SetFacingDirection(Vector2 moveInput)
        {
            if (moveInput.x > 0f && !IsFacingRight)
            {
                //오른쪽을 바라본다
                IsFacingRight = true;
            }
            else if (moveInput.x < 0f && IsFacingRight)
            {
                //왼쪽을 바라본다
                IsFacingRight = false;
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {                
            inputMove = context.ReadValue<Vector2>();
            if (!IsDeath)
            {
                IsMove = inputMove != Vector2.zero;
                SetFacingDirection(inputMove);
            }
            else
            {
                IsMove = false;
            }
            
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            if (context.started)        //입력값이 있으면
            {
                IsRun = true;
            }
            else if (context.canceled)  //입력이 종료되는 순간
            {
                IsRun = false;
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started && touchingDirections.IsGround)        //입력값이 있으면
            {
                animator.SetTrigger(AnimationString.JumpTrigger);
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.started && touchingDirections.IsGround)
            {
                animator.SetTrigger(AnimationString.AttackTrigger);
            }
        }
    }
}