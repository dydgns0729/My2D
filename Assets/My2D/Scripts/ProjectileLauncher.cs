using UnityEngine;

namespace My2D
{
    //발사체(화살) 발사
    public class ProjectileLauncher : MonoBehaviour
    {
        #region Variables
        public GameObject projectilPrefab;
        public Transform firePoint;
        #endregion

        public void FireProjectile()
        {
            GameObject projectile = Instantiate(projectilPrefab, firePoint.position, projectilPrefab.transform.rotation);
            Destroy(projectile, 5f);
            Vector3 originScale = projectile.transform.localScale;

            //화살의 방향 결정
            projectile.transform.localScale = new Vector3(
                originScale.x * (transform.localScale.x) > 0 ? 1 : -1,
                originScale.y,
                originScale.z);

            
        }

    }
}