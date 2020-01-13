using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CameraTarget : MonoBehaviour
{
    public Camera SubCamera;
    public Transform PoseDriverTrans;

    // トラッキングの位置中心
    Vector3 poseCenter_ = Vector3.zero;
    Quaternion poseCenterRotation_ = Quaternion.identity;

    // 認識した床
    ARPlane plane_ = null;

    public bool IsTracking
    {
        private set;
        get;
    } = false;

    void Update()
    {
        if (plane_ == null || Input.touchCount > 0)
        {
            // 新しい床の取得
            plane_ = null;
            ARPlane newPlane = null;
            float newPlaneDistance = float.PositiveInfinity;
            ARPlane[] planes = FindObjectsOfType<ARPlane>();
            foreach (ARPlane plane in planes)
            {
                if (plane.gameObject.activeSelf)
                {
                    float distance = Vector2.Distance(new Vector2(PoseDriverTrans.position.x, PoseDriverTrans.position.z), new Vector2(plane.transform.position.x, plane.transform.position.z));
                    if (newPlaneDistance >= distance)
                    {
                        newPlane = plane;
                        newPlaneDistance = distance;
                    }
                }
            }

            if (newPlane != null)
            {
                plane_ = newPlane;

                ResetPosition();

                // カメラ画像の切り替え
                StartCoroutine(delayedSubCameraDisable());

                IsTracking = true;
            }

            if (plane_ == null)
            {
                // カメラ画像の切り替え
                SubCamera.gameObject.SetActive(true);

                IsTracking = false;
            }
        }

        // 移動
        transform.position = Quaternion.Inverse(poseCenterRotation_) * (PoseDriverTrans.position - poseCenter_);
        transform.rotation = Quaternion.Inverse(poseCenterRotation_) * PoseDriverTrans.rotation;
    }

    // 位置のリセット
    public void ResetPosition()
    {
        poseCenter_ = PoseDriverTrans.position;
        if (plane_ != null)
        {
            poseCenter_ += new Vector3(0f, plane_.transform.position.y, 0f);
        }

        poseCenterRotation_ = Quaternion.Euler(0f, PoseDriverTrans.eulerAngles.y, 0f);
    }

    IEnumerator delayedSubCameraDisable()
    {
        yield return null;
        yield return null;

        SubCamera.gameObject.SetActive(false);
    }
}
