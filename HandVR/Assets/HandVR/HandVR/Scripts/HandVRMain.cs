using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVRMain : MonoBehaviour
{
    const int RESIZE_HEIGHT = 512;

    public RenderTexture InputRenderTexture;

    AndroidJavaObject multiHandMain_;
    bool isStart_ = false;
    Texture2D texture2D_;

    void Start()
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (AndroidJavaObject currentUnityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        {
            multiHandMain_ = new AndroidJavaObject("online.mumeigames.mediapipe.apps.multihandtrackinggpu.MultiHandMain", currentUnityActivity);
        }

        texture2D_ = new Texture2D(RESIZE_HEIGHT * Screen.width / Screen.height, RESIZE_HEIGHT, TextureFormat.ARGB32, false, false);
    }

    void Update()
    {
        updateFrame();
    }

    void updateFrame()
    {
        if (!isStart_)
        {
            multiHandMain_.Call("startRunningGraph");
            isStart_ = true;
        }

        RenderTexture reshapedTexture = RenderTexture.GetTemporary(texture2D_.width, texture2D_.height);
        Graphics.Blit(InputRenderTexture, reshapedTexture);

        RenderTexture.active = reshapedTexture;
        texture2D_.ReadPixels(new Rect(0, 0, texture2D_.width, texture2D_.height), 0, 0, false);
        texture2D_.Apply();
        RenderTexture.active = null;

        RenderTexture.ReleaseTemporary(reshapedTexture);

        byte[] frameImage = ImageConversion.EncodeToJPG(texture2D_);
        sbyte[] frameImageSigned = Array.ConvertAll(frameImage, b => unchecked((sbyte)b));

        multiHandMain_.Call("setFrame", frameImageSigned);
    }

    public float[] GetLandmark(int id, int index)
    {
        float[] posVecArray = multiHandMain_.Call<float[]>("getLandmark", id, index);
        if (posVecArray == null)
        {
            return null;
        }

        posVecArray[0] = (posVecArray[0] - 0.5f) * 0.15f * Screen.width / Screen.height;
        posVecArray[1] = (posVecArray[1] - 0.5f) * -0.15f;
        posVecArray[2] = posVecArray[2] * 0.001f + 0.25f;

        return posVecArray;
    }

    void OnDestroy()
    {
        if (multiHandMain_ != null)
        {
            multiHandMain_.Dispose();
        }
    }
}
