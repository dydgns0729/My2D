using UnityEngine;
using UnityEngine.Events;

namespace My2D {
    //캐릭터와 관련된 이벤트 함수들을 관리하는 클래스
    public class CharacterEvents
    {
        //캐릭터가 공격받으면 호출되는 델리게이트 함수
        public static UnityAction<GameObject, float> characterDamaged;

        //캐릭터가 체력을 회복할때 호출되는 델리게이트 함수
        public static UnityAction<GameObject, float> characterHealed;


    }
}