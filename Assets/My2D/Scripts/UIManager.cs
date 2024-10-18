using TMPro;
using UnityEngine;

namespace My2D
{
    public class UIManager : MonoBehaviour
    {
        #region Variables

        public GameObject damageTextPrefab;
        public GameObject healTextPrefab;

        private Canvas canvas;
        [SerializeField] private Vector3 healthTextOffset = Vector3.zero;
        #endregion

        private void Awake()
        {
            //참조
            canvas = FindObjectOfType<Canvas>();
        }

        private void OnEnable()
        {
            //캐릭터 관련 이벤트 함수 등록
            CharacterEvents.characterDamaged += CharacterTakeDamage;
            CharacterEvents.characterHealed += CharacterHeal;
        }

        private void OnDisable()
        {
            //캐릭터 관련 이벤트 함수 해제
            CharacterEvents.characterDamaged -= CharacterTakeDamage;
            CharacterEvents.characterHealed -= CharacterHeal;
        }

        public void CharacterTakeDamage(GameObject character, float damage)
        {
            //damageTextPrefab Spawn
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
            

            GameObject textGo = Instantiate(damageTextPrefab, spawnPosition + healthTextOffset, Quaternion.identity, canvas.transform);
            TextMeshProUGUI damageText = textGo.GetComponent<TextMeshProUGUI>();
            damageText.text = damage.ToString();
        }

        public void CharacterHeal(GameObject character, float restore)
        {
            //healTextPrefab Spawn
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

            GameObject textGo = Instantiate(healTextPrefab, spawnPosition + healthTextOffset, Quaternion.identity, canvas.transform);
            TextMeshProUGUI damageText = textGo.GetComponent<TextMeshProUGUI>();
            damageText.text = restore.ToString();
        }
    }
}