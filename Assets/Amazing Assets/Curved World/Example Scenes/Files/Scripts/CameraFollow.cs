using UnityEngine;
using System.Collections;

namespace AmazingAssets.CurvedWorld.Example
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;           
        public float smoothing = 5f;
        public float aboveValue;
        public float belowValue;

        void LateUpdate ()
        {
            if (target.position.y > 5f)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, aboveValue, -5f), smoothing * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), smoothing * Time.deltaTime);
            } 
            else if (target.position.y < 5)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, belowValue, -4f), smoothing * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(10, 0, 0), smoothing * Time.deltaTime);
            }
        }
    }
}
