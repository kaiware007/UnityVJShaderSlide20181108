using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KUtil;
using System;

namespace KUtil
{
    public abstract class EasingObjectBase<T>
    {
        public float now = 1;   // 現在時間
        public float time = 1;  // 時間

        public T startParam;    // 開始時の値
        public T endParam;      // 終了時の値
        public T nowValue;         // 現在の値

        public EaseType type = EaseType.Lerp;

        /// <summary>
        /// 開始したか？
        /// </summary>
        public bool isStart = false;

        /// <summary>
        /// リピートするか？
        /// </summary>
        public bool isRepeat = false;

        /// <summary>
        /// 終了したか？
        /// </summary>
        public bool isEnd { get { return (now >= time); } }

        protected Action beatAction = null;

        public void Init(T value)
        {
            nowValue = startParam = endParam = value;
        }

        public void AddAction(Action action)
        {
            beatAction = action;
        }

        public void Start(EaseType easeType, T start, T end, float t, bool repeat = false)
        {
            now = 0;
            if (t <= 0)
            {
                t = 1;
                now = 1;
            }
            time = t;
            startParam = start;
            endParam = end;
            type = easeType;
            isStart = true;
            isRepeat = repeat;
        }

        public void Update(float dt)
        {
            if (isStart)
            {
                float now_ = now + dt;
                now = Mathf.Min(now_, time);
                nowValue = CalcEase();
                if (now >= time)
                {                    
                    if (isRepeat)
                    {
                        now = now_ - time;

                        beatAction?.Invoke();
                    }
                    else
                    {
                        isStart = false;
                    }
                }
            }
        }

        protected abstract T CalcEase();
    }

    public class EasingObjectFloat : EasingObjectBase<float>
    {
        protected override float CalcEase()
        {
            return Easing.Ease(type, startParam, endParam, now / time);  
        }
    }

    public class EasingObjectVector2 : EasingObjectBase<Vector2>
    {
        protected override Vector2 CalcEase()
        {
            return Easing.Ease(type, startParam, endParam, now / time);
        }
    }

    public class EasingObjectVector3 : EasingObjectBase<Vector3>
    {
        protected override Vector3 CalcEase()
        {
            return Easing.Ease(type, startParam, endParam, now / time);
        }
    }

    public class EasingObjectVector4 : EasingObjectBase<Vector4>
    {
        protected override Vector4 CalcEase()
        {
            return Easing.Ease(type, startParam, endParam, now / time);
        }
    }

    public class EasingObjectQuaternion : EasingObjectBase<Quaternion>
    {
        protected override Quaternion CalcEase()
        {
            return Easing.Ease(type, startParam, endParam, now / time);
        }
    }

    public class EasingObjectColor : EasingObjectBase<Color>
    {
        protected override Color CalcEase()
        {
            return Easing.Ease(type, startParam, endParam, now / time);
        }
    }
}