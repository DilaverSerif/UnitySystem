using Sirenix.OdinInspector;
using UnityEngine;

namespace _SYSTEMS_._Character_Controller_
{
    [System.Serializable]
    public class RaycastChecker :MonoBehaviour, IRaycastChecker
    {
        [Title("Generally Data")]
        public PlayerMovementData playerMovementData;

        [Title("Raycast Data")]
        public RayData FrontRay;
        public RayData BackRay;
        public RayData DownRay;
        public RayData UpRay;
        
        public void Update()
        {
            IsGrounded();
            IsFrontHit();
        }

        public bool IsGrounded()
        {
            var hits = 0;

            foreach (var hitData in DownRay.rays)
            {
                var ray = hitData.hitDataStruct;
                
                var hit = new RaycastHit[1];
                hits = Physics.SphereCastNonAlloc(
                    transform.position + ray.offset, ray.radius, ray.direction, hit, ray.distance,
                    ray.checkLayer);
                
                hitData.hitTransform = hit[0].transform;
                hitData.isHit = hits > 0;
            }
            
            return hits > 0;
        }

        public void IsFrontHit()
        {
            foreach (var ray in FrontRay.rays)
            {
                var hit = new RaycastHit[1];
                var hits = Physics.RaycastNonAlloc(
                    transform.position + ray.hitDataStruct.offset, ray.hitDataStruct.direction, hit,
                    ray.hitDataStruct.distance, ray.hitDataStruct.checkLayer);

                ray.hitTransform = hit[0].transform;
                ray.isHit = hits > 0;
            }
        }
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            foreach (var ray in DownRay.rays)
            {
                if (ray.isHit)
                    Gizmos.color = Color.green;
                Gizmos.DrawSphere(transform.position + ray.hitDataStruct.offset, ray.hitDataStruct.radius);
            }
            
            Gizmos.color = Color.red;
            
            foreach (var ray in FrontRay.rays)
            {
                if (ray.isHit)
                    Gizmos.color = Color.green;
                Gizmos.DrawRay((transform.position + ray.hitDataStruct.offset  ), ray.hitDataStruct.direction * ray.hitDataStruct.distance);
            }
        }

      
        
    }
}
