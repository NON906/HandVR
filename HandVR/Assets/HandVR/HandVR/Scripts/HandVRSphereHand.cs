using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVRSphereHand : MonoBehaviour
{
    public int Id;

    Transform[] fingers_ = new Transform[21];
    bool[] fingerTracking_ = new bool[5];
    bool[] fingerOpened_ = new bool[5];

    bool calcFingerOpened(Vector3 rootVec, Vector3 tipVec)
    {
        rootVec.Normalize();
        tipVec.Normalize();

        float cos = rootVec.x * tipVec.x + rootVec.y * tipVec.y + rootVec.z * tipVec.z;

        return cos > 0.5f; 
    }

    void Start()
    {
        foreach (Transform child in transform)
        {
            HandVRPosition posObj = child.GetComponent<HandVRPosition>();
            if (posObj != null)
            {
                fingers_[posObj.Index] = child;
                posObj.Id = Id;
            }
        }
    }

    void Update()
    {
        for (int loop = 0; loop < 5; loop++)
        {
            bool opened;

            bool fullTracking = true;
            for (int loop2 = 0; loop2 < 4; loop2++)
            {
                if (fingers_[loop * 4 + loop2 + 1].GetComponent<Renderer>().enabled == false)
                {
                    fullTracking = false;
                    break;
                }
            }

            if (fullTracking)
            {
                opened = calcFingerOpened(fingers_[loop * 4 + 2].position - fingers_[loop * 4 + 1].position,
                    fingers_[loop * 4 + 4].position - fingers_[loop * 4 + 3].position);
            }
            else
            {
                opened = false;
            }

            fingerTracking_[loop] = fullTracking;
            fingerOpened_[loop] = opened;
        }
    }

    public bool GetFingerTracking(int index)
    {
        return fingerTracking_[index];
    }

    public bool GetFingerOpened(int index)
    {
        return fingerOpened_[index];
    }

    public Transform GetFinger(int index)
    {
        return fingers_[index];
    }
}
