using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseBall : MonoBehaviour
{
    public UnityEvent OnLaunch;
    public UnityEvent OnCollision;
    public UnityEvent OnSettle;
    public UnityEvent OnSink;
}
