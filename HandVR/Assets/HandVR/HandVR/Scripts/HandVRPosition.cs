using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HandVRPosition : MonoBehaviour
{
    public int Index;

    public int Id
    {
        set;
        get;
    }

    HandVRMain handVRMain_;
    Vector3 position_;
    Rigidbody rigidbody_;
    Renderer renderer_;

    void Start()
    {
        handVRMain_ = FindObjectOfType<HandVRMain>();
        rigidbody_ = GetComponent<Rigidbody>();
        renderer_ = GetComponent<Renderer>();
        renderer_.enabled = false;
    }

    void Update()
    {
        float[] posVecArray = handVRMain_.GetLandmark(Id, Index);
        if (posVecArray != null)
        {
            renderer_.enabled = true;

            position_ = new Vector3(posVecArray[0], posVecArray[1], posVecArray[2]);
        }
        else
        {
            renderer_.enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (rigidbody_ != null && renderer_.enabled)
        {
            rigidbody_.AddForce((position_ - transform.localPosition) / Time.fixedDeltaTime - rigidbody_.velocity, ForceMode.VelocityChange);
        }
        else
        {
            transform.localPosition = position_;
        }
    }
}
