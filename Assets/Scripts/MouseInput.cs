//using UnityEngine;
//using System.Collections;

//[RequireComponent(typeof(CubeController))]
//public class MouseInput : MonoBehaviour
//{
//    private void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            RaycastHit hitInfo = new RaycastHit();
//            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
//            {
//                Cube hitCube = hitInfo.collider.GetComponent<Cube>();
//                if (hitCube != null)
//                {
//                    GetComponent<CubeController>().ClickCubeAt(hitCube.currentIndex);
//                }
//            }
//        }
//    }
//}
