using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace My2D
{
    public class Damageable : MonoBehaviour
    {
        #region Variables
        private Animator animator;

        //데미지 입을때 등록된 함수 호출
        public UnityAction<float, Vector2> hitAction;

        //체력
        [SerializeField] private float maxHealth = 100f;
        public float MaxHealth
        {
            get { return maxHealth; }
            private set { maxHealth = value; }
        }
        [SerializeField] private float currentHealth;
        public float CurrentHealth
        {
            get { return currentHealth; }
            private set
            {
                currentHealth = value;

                //죽음처리
                if (currentHealth <= 0)
                {
                    IsDeath = true;
                }
            }
        }
        private bool isDeath;
        public bool IsDeath
        {
            get { return isDeath; }
            private set
            {
                isDeath = value;
                //애니메이션
                animator.SetBool(AnimationString.IsDeath, value);
            }
        }
        //무적모드
        private bool isInvincible;
        [SerializeField] private float invincibleTimer = 3f;
        private float countdown = 0f;

        //
        public bool LockVelocity
        {
            get
            {
                return animator.GetBool(AnimationString.LockVelocity);
            }
            private set
            {
                animator.SetBool(AnimationString.LockVelocity, value);
            }
        }
        #endregion

        private void Awake()
        {
            //참조
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            //초기화
            currentHealth = maxHealth;
            countdown = invincibleTimer;
            isDeath = false;
            isInvincible = false;

        }

        private void Update()
        {
            //무적상태이면
            if (isInvincible)
            {
                if (countdown <= 0)
                {
                    isInvincible = false;
                    countdown = invincibleTimer;
                }
                countdown -= Time.deltaTime;

            }
        }

        //TakeDamage
        public void TakeDamage(float damage, Vector2 knockback)
        {
            if (!IsDeath && !isInvincible)
            {
                //무적모드 초기화
                isInvincible = true;

                //공격받기 전의 HP
                float beforeHealth = CurrentHealth;

                CurrentHealth -= damage;
                CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

                Debug.Log($"{transform.name} 의 현재 체력은 {CurrentHealth}");
                LockVelocity = true;

                //애니메이션
                animator.SetTrigger(AnimationString.HitTrigger);

                float realDamage = beforeHealth - CurrentHealth;


                //데미지 효과
                hitAction?.Invoke(realDamage, knockback);

                CharacterEvents.characterDamaged?.Invoke(this.gameObject, realDamage);
            }
        }

        //체력 회복
        public bool Heal(float amount)
        {
            //충돌한 오브젝트 damagealbe을 검사하여 힐한다
            if (CurrentHealth >= MaxHealth)
            {
                return false;
            }

            //힐하기 전의 HP
            float beforeHealth = CurrentHealth;

            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

            //실제 힐 hp값
            float realHealth = CurrentHealth - beforeHealth;


            Debug.Log($"{transform.name} 의 현재 체력은 {CurrentHealth}");

            CharacterEvents.characterHealed?.Invoke(this.gameObject, realHealth);

            return true;
        }


    }
}