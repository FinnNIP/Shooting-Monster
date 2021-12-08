using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class GunController : MonoBehaviour
    {
        [SerializeField]
        public Vector3 target;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            LookAtCursor();
        }
        private void LookAtCursor ()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                target = hit.point;
            }

            transform.LookAt(target);
        }
    }
}