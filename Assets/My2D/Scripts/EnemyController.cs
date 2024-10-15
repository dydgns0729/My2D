using UnityEngine;

namespace My2D
{
    public class EnemyController : MonoBehaviour
    {
        #region Variables
        private Animator animator;
        private Rigidbody2D rb2d;
        private TouchingDirections touchingDirections;
        //플레이어 감지
        public DetectionZone detectionZone;

        //이동
        [SerializeField] private float runSpeed = 4f;
        //이동 방향
        private Vector2 directionVector;
        //이동 가능 방향
        public enum WalkableDirection { Left, Right }
        private WalkableDirection walkDirection;
        public WalkableDirection WalkDirection
        {
            get { return walkDirection; }
            set
            {
                transform.localScale *= new Vector2(-1, 1);
                //실제 이동하는 방향값
                if (value == WalkableDirection.Left)
                {
                    directionVector = Vector2.left;
                }
                else if (value == WalkableDirection.Right)
                {
                    directionVector = Vector2.right;
                }
                walkDirection = value;
            }
        }

        //공격 타겟 설정
        [SerializeField] private bool hasTarget;
        public bool HasTarget
        {
            get { return hasTarget; }
            private set
            {
                hasTarget = value;
                animator.SetBool(AnimationString.HasTarget, value);
            }
        }

        //이동 가능 상태 판별
        public bool CanMove
        {
            get { return animator.GetBool(AnimationString.CanMove); }
        }

        //감속 계수
        [SerializeField] private float stopRate = 0.2f;

        #endregion

        private void Awake()
        {
            //참조
            animator = GetComponent<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
            touchingDirections = GetComponent<TouchingDirections>();
            directionVector = Vector2.right;
            walkDirection = WalkableDirection.Right;
            hasTarget = false;
        }

        //DetectionZone
        private void Update()
        {
            //적 감지 충돌체의 리스트 갯수가 0보다 크면 적이 감지된 것
            HasTarget = (detectionZone.detectedColliders.Count > 0);
        }

        private void FixedUpdate()
        {
            //땅에서 이동시 벽을 만나면 방향 전환
            if (touchingDirections.IsWall && touchingDirections.IsGround)
            {
                //방향전환
                Flip();
            }
            if (CanMove)
            {
                rb2d.velocity = new Vector2(directionVector.x * runSpeed, rb2d.velocity.y);
            }
            else
            {
                //rb2d.velocity.x -> 0 : Lerp함수를 사용해서 멈춤
                rb2d.velocity = new Vector2(Mathf.Lerp(rb2d.velocity.x, 0f, stopRate), rb2d.velocity.y);
            }
        }

        private void Flip()
        {
            if (WalkDirection == WalkableDirection.Left)
            {
                WalkDirection = WalkableDirection.Right;
            }
            else if (WalkDirection == WalkableDirection.Right)
            {
                WalkDirection = WalkableDirection.Left;
            }
            else
            {
                Debug.Log("Error Flip Direction");
            }
        }
    }
}