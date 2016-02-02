using UnityEngine;
using System;
using System.Collections;

public static class GameObjectExtensions
{
    public static IEnumerator GoToPos(this GameObject go, Vector3 pos, float speed, Action onFinish = null)
    {
        //pos.y = transform.position.y;
        while (Vector3.SqrMagnitude(go.transform.position - pos) > 0.1f)
        {
            go.transform.position += (pos - go.transform.position).normalized * Time.deltaTime * speed;
            yield return null;
        }
        if (onFinish != null)
        {
            onFinish();
        }
    }
}
