using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace My2D
{
    //지정된 충돌체 감지
    public class DetectionZone : MonoBehaviour
    {
        #region Variables
        //감지된 콜라이더
        public List<Collider2D> detectedColliders = new List<Collider2D>();

        //충돌체 리스트가 비어있다면 호출되는 함수
        public UnityAction noColliderRamain;

        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //충돌체가 감지되면 리스트에 추가한다
            detectedColliders.Add(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            //충돌체가 나가면 리스트에서 제거한다
            detectedColliders.Remove(collision);

            //충돌체가 하나도 남아있지 않다면
            if(detectedColliders.Count <= 0)
            {
                noColliderRamain?.Invoke();
            }
        }
    }
}