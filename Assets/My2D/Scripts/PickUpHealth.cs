using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class PickUpHealth : MonoBehaviour
    {
        #region Variables
        //체력 회복 양
        [SerializeField] private float restoreHealth = 20f;
        [SerializeField] private Vector3 rotateSpeed = new Vector3(0f, 180f, 0f);
        #endregion

        private void Update()
        {
            transform.eulerAngles += rotateSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Damageable damageable = collision.GetComponent<Damageable>();
            if (damageable != null)
            {
                bool isHeal = damageable.Heal(restoreHealth);

                if (isHeal)
                {
                    //아이템을 먹으면 없어짐
                    Destroy(gameObject);
                }
                
            }
        }
    }
}