using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
        public Transform playerTransform;
    Vector3 target;
    Vector3  mousePos;
    Vector3 refVel;
    Vector3 shakeOffset;
    Vector3 shakeVector;
    public float cameraDist = 3.5f;
    public float smoothTime = 0.2f;
    float zStart;
    float shakeMag;
    float shakeTimeEnd;
    bool shaking;
    //public float speed;
    // Start is called before the first frame update
    void Start()
    {
        target = playerTransform.position;
        zStart = transform.position.z;
    }

    void Update ()
    {
        if (playerTransform != null)
        {
            mousePos = CaptureMousePos();
            shakeOffset = UpdateShake();
            target = UpdateTargetPos();
            UpdateCameraPosition();
        }
    }

    Vector3 CaptureMousePos()
    {
        Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        ret *= 2;
        ret -= Vector2.one;
        float max = 0.9f;
        if (Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max)
        {
            ret = ret.normalized;
        }
        return ret;
    }

    Vector3 UpdateShake()
    {
        if (!shaking || Time.time > shakeTimeEnd)
        {
            shaking = false;
            return Vector3.zero;
        }
        Vector3 tempOffset = shakeVector;
        tempOffset *= shakeMag;
        return tempOffset;
    }

    Vector3 UpdateTargetPos()
    {
        Vector3 mouseOffset = mousePos * cameraDist;
        Vector3 ret = playerTransform.position + mouseOffset;
        ret += shakeOffset;
        ret.z = zStart;
        return ret;
    }

    void UpdateCameraPosition()
    {
        Vector3 tempPos;
        tempPos = Vector3.SmoothDamp(transform.position, target, ref refVel, smoothTime);
        transform.position = tempPos;
    }

    public void Shake(Vector3 direction, float magnitude, float length)
    {
        shaking = true;
        shakeVector = direction;
        shakeMag = magnitude;
        shakeTimeEnd = Time.time + length;
    }

}
