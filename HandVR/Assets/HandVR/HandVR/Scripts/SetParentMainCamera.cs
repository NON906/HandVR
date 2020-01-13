using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParentMainCamera : MonoBehaviour
{
    IEnumerator Start()
    {
        while (Camera.main == null)
        {
            yield return null;
        }

        transform.parent = Camera.main.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
