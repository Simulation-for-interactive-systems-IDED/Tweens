using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Easing
{
    Linear,
    EaseInQuad,
    EaseInCubic,
    EaseOutElastic,
    EaseInBack
}

/// <summary>
/// Class used to tween the fransform position of this object to a target object in a specific time.
/// Spanish: Un tween anima algo de un punto A a un punto B en un tiempo determinado (que nosotros especificamos)
/// </summary>
public class MyTween : MonoBehaviour
{
    [SerializeField]
    AnimationCurve customCurve;

    [SerializeField]
    Easing easing = Easing.Linear;

    [Range(0, 1)]
    [SerializeField]
    float t = 0;

    [SerializeField]
    float duration = 1f;
    float accumulatedTime = 0;
    bool isPlaying = false;

    [SerializeField]
    Transform target;

    Vector3 startPos;
    Vector3 endPos;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartTween();
        }

        if (!isPlaying)
        {
            return;
        }

        if (t >= 1)
        {
            Debug.Log("Completed!!!");
            isPlaying = false;
            return;
        }

        t = accumulatedTime / duration;

        // Calculate easing
        float finalT = 0;
        switch (easing)
        {
            case Easing.Linear:
                finalT = Linear(t);
                break;
            case Easing.EaseInQuad:
                finalT = EaseInQuad(t);
                break;
            case Easing.EaseInCubic:
                finalT = EaseInCubic(t);
                break;
            case Easing.EaseOutElastic:
                finalT = EaseOutElastic(t);
                break;
            case Easing.EaseInBack:
                finalT = EaseInBack(t);
                break;
            default:
                break;
        }

        //finalT = customCurve.Evaluate(t);
        transform.position = Vector2.LerpUnclamped(startPos, endPos, finalT);
        accumulatedTime += Time.deltaTime;
    }

    public void StartTween()
    {
        startPos = transform.position;
        endPos = target.position;
        accumulatedTime = 0;
        t = 0;
        isPlaying = true;
    }

    private float Linear(float t)
    {
        return t;
    }

    private float EaseInQuad(float t)
    {
        return t * t;
    }

    private float EaseInCubic(float t)
    {
        return t * t * t;
    }

    private float EaseInBack(float t)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1f;

        return c3 * t * t * t - c1 * t * t;
    }

    private float EaseOutElastic(float t)
    {
        float c4 = (2f * Mathf.PI) / 3f;

        return t == 0f
          ? 0f
          : t == 1f
          ? 1f
          : Mathf.Pow(2f, -10 * t) * Mathf.Sin((t * 10f - 0.75f) * c4) + 1f;
    }
}
