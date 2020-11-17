using System;
using UnityEngine;

public class ClickBoostTimer
{
    public float Timer { get; private set; }
    private readonly float length;
    private readonly Action<float> onFinish;
    public readonly float amt;

    public bool Finished { get; private set; }

    public ClickBoostTimer(float _length, float _amt, Action<float> _onFinish)
    {
        length = _length;
        onFinish = _onFinish;
        amt = _amt;
        Timer = length;
    }

    public void OnUpdate()
    {
        Timer -= Time.deltaTime;
        if(Timer <= 0)
        {
            onFinish?.Invoke(amt);
            Finished = true;
        }
    }
}
