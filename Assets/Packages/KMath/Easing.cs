using UnityEngine;
using System;

namespace KUtil {
	
	public enum EaseType {
		Lerp,
		QuadIn,
		QuadOut,
		QuadInOut,
		QuadOutIn,
		CubicIn,
		CubicOut,
		CubicInOut,
		CubicOutIn,
		QuartIn,
		QuartOut,
		QuartInOut,
		QuartOutIn,
		QuintIn,
		QuintOut,
		QuintInOut,
		QuintOutIn,
		SineIn,
		SineOut,
		SineInOut,
		SineOutIn,
		ExpoIn,
		ExpoOut,
		ExpoInOut,
		ExpoOutIn,
		CircIn,
		CircOut,
		CircInOut,
		CircOutIn,
		ElasticIn,
		ElasticOut,
		ElasticInOut,
		ElasticOutIn,
		BackIn,
		BackOut,
		BackInOut,
		BackOutIn,
		BounceIn,
		BounceOut,
		BounceInOut,
		BounceOutIn,
	};

	public static class Easing {
		
		#region public
		public static float Ease(EaseType type, float start, float end, float t) {
			return funcList [(int)type] (start, end, t);
		}

        //public static T Ease<T>(EaseType type, T start, T end, float t) where T : struct
        //{
        //    //return funcList[(int)type](start, end, t);
        //    return InternalEase(type, start, end, t);

        //    //if(typeof(T) == typeof(Vector2))
        //    //{
        //    //    Vector2 st = start as Vector2;
        //    //    return EaseVector2(type, (Vector2)start, end, t);
        //    //}
        //}

        static Vector2 vec2 = Vector2.zero;
        public static Vector2 Ease(EaseType type, Vector2 start, Vector2 end, float t)
        {
            float tt = funcList[(int)type](0f, 1f, t);
            vec2.x = Mathf.Lerp(start.x, end.x, tt);
            vec2.y = Mathf.Lerp(start.y, end.y, tt);
            return vec2;
        }

        static Vector3 vec3 = Vector3.zero;
        public static Vector3 Ease(EaseType type, Vector3 start, Vector3 end, float t)
        {
            float tt = funcList[(int)type](0f, 1f, t);
            vec3.x = Mathf.Lerp(start.x, end.x, tt);
            vec3.y = Mathf.Lerp(start.y, end.y, tt);
            vec3.z = Mathf.Lerp(start.z, end.z, tt);
            return vec3;
        }

        static Vector4 vec4 = Vector4.zero;
        public static Vector4 Ease(EaseType type, Vector4 start, Vector4 end, float t)
        {
            float tt = funcList[(int)type](0f, 1f, t);
            vec4.x = Mathf.Lerp(start.x, end.x, tt);
            vec4.y = Mathf.Lerp(start.y, end.y, tt);
            vec4.z = Mathf.Lerp(start.z, end.z, tt);
            vec4.w = Mathf.Lerp(start.w, end.w, tt);
            return vec4;
        }

        static Color col = Color.black;
        public static Color Ease(EaseType type, Color start, Color end, float t)
        {
            float tt = funcList[(int)type](0f, 1f, t);
            col.r = Mathf.Lerp(start.r, end.r, tt);
            col.g = Mathf.Lerp(start.g, end.g, tt);
            col.b = Mathf.Lerp(start.b, end.b, tt);
            col.a = Mathf.Lerp(start.a, end.a, tt);
            return col;
        }

        public static Quaternion Ease(EaseType type, Quaternion start, Quaternion end, float t)
        {
            float tt = funcList[(int)type](0f, 1f, t);
            return Quaternion.Slerp(start, end, tt);
        }

        public static float Lerp(float start, float end, float t)
		{
			return In(Linear, t, start, end - start);
		}

		public static float QuadIn(float start, float end, float t)
		{
			return In(Quad, t, start, end - start);
		}

		public static float QuadOut(float start, float end, float t)
		{
			return Out(Quad, t, start, end - start);
		}

		public static float QuadInOut(float start, float end, float t)
		{
			return InOut(Quad, t, start, end - start);
		}

		public static float QuadOutIn(float start, float end, float t)
		{
			return OutIn(Quad, t, start, end - start);
		}

		public static float CubicIn(float start, float end, float t)
		{
			return In(Cubic, t, start, end - start);
		}

		public static float CubicOut(float start, float end, float t)
		{
			return Out(Cubic, t, start, end - start);
		}

		public static float CubicInOut(float start, float end, float t)
		{
			return InOut(Cubic, t, start, end - start);
		}

		public static float CubicOutIn(float start, float end, float t)
		{
			return OutIn(Cubic, t, start, end - start);
		}

		public static float QuartIn(float start, float end, float t)
		{
			return In(Quart, t, start, end - start);
		}

		public static float QuartOut(float start, float end, float t)
		{
			return Out(Quart, t, start, end - start);
		}

		public static float QuartInOut(float start, float end, float t)
		{
			return InOut(Quart, t, start, end - start);
		}

		public static float QuartOutIn(float start, float end, float t)
		{
			return OutIn(Quart, t, start, end - start);
		}

		public static float QuintIn(float start, float end, float t)
		{
			return In(Quint, t, start, end - start);
		}

		public static float QuintOut(float start, float end, float t)
		{
			return Out(Quint, t, start, end - start);
		}

		public static float QuintInOut(float start, float end, float t)
		{
			return InOut(Quint, t, start, end - start);
		}

		public static float QuintOutIn(float start, float end, float t)
		{
			return OutIn(Quint, t, start, end - start);
		}

		public static float SineIn(float start, float end, float t)
		{
			return In(Sine, t, start, end - start);
		}

		public static float SineOut(float start, float end, float t)
		{
			return Out(Sine, t, start, end - start);
		}

		public static float SineInOut(float start, float end, float t)
		{
			return InOut(Sine, t, start, end - start);
		}

		public static float SineOutIn(float start, float end, float t)
		{
			return OutIn(Sine, t, start, end - start);
		}

		public static float ExpoIn(float start, float end, float t)
		{
			return In(Expo, t, start, end - start);
		}

		public static float ExpoOut(float start, float end, float t)
		{
			return Out(Expo, t, start, end - start);
		}

		public static float ExpoInOut(float start, float end, float t)
		{
			return InOut(Expo, t, start, end - start);
		}

		public static float ExpoOutIn(float start, float end, float t)
		{
			return OutIn(Expo, t, start, end - start);
		}

		public static float CircIn(float start, float end, float t)
		{
			return In(Circ, t, start, end - start);
		}

		public static float CircOut(float start, float end, float t)
		{
			return Out(Circ, t, start, end - start);
		}

		public static float CircInOut(float start, float end, float t)
		{
			return InOut(Circ, t, start, end - start);
		}

		public static float CircOutIn(float start, float end, float t)
		{
			return OutIn(Circ, t, start, end - start);
		}

		public static float ElasticIn(float start, float end, float t)
		{
			return In(Elastic, t, start, end - start);
		}

		public static float ElasticOut(float start, float end, float t)
		{
			return Out(Elastic, t, start, end - start);
		}

		public static float ElasticInOut(float start, float end, float t)
		{
			return InOut(Elastic, t, start, end - start);
		}

		public static float ElasticOutIn(float start, float end, float t)
		{
			return OutIn(Elastic, t, start, end - start);
		}

		public static float BackIn(float start, float end, float t)
		{
			return In(Back, t, start, end - start);
		}

		public static float BackOut(float start, float end, float t)
		{
			return Out(Back, t, start, end - start);
		}

		public static float BackInOut(float start, float end, float t)
		{
			return InOut(Back, t, start, end - start);
		}

		public static float BackOutIn(float start, float end, float t)
		{
			return OutIn(Back, t, start, end - start);
		}

		public static float BounceIn(float start, float end, float t)
		{
			return In(Bounce, t, start, end - start);
		}

		public static float BounceOut(float start, float end, float t)
		{
			return Out(Bounce, t, start, end - start);
		}

		public static float BounceInOut(float start, float end, float t)
		{
			return InOut(Bounce, t, start, end - start);
		}

		public static float BounceOutIn(float start, float end, float t)
		{
			return OutIn(Bounce, t, start, end - start);
		}
		#endregion

		#region private
		private static float In(Func<float, float, float> ease_f, float t, float b, float c, float d = 1)
		{
			if (t >= d)
				return b + c;
			if (t <= 0)
				return b;

			return c * ease_f(t, d) + b;
		}

		private static float Out(Func<float, float, float> ease_f, float t, float b, float c, float d = 1)
		{
			if (t >= d)
				return b + c;
			if (t <= 0)
				return b;

			return (b + c) - c * ease_f(d - t, d);
		}

		private static float InOut(Func<float, float, float> ease_f, float t, float b, float c, float d = 1)
		{
			if (t >= d)
				return b + c;
			if (t <= 0)
				return b;

			if (t < d / 2)
				return In(ease_f, t * 2, b, c / 2, d);

			return Out(ease_f, (t * 2) - d, b + c / 2, c / 2, d);
		}

		private static float OutIn(Func<float, float, float> ease_f, float t, float b, float c, float d = 1)
		{
			if (t >= d)
				return b + c;
			if (t <= 0)
				return b;

			if (t < d / 2)
				return Out(ease_f, t * 2, b, c / 2, d);

			return In(ease_f, (t * 2) - d, b + c / 2, c / 2, d);
		}
		#endregion

		#region Equations

		private static float Linear(float t, float d = 1)
		{
			return t / d;
		}

		private static float Quad(float t, float d = 1)
		{
			return (t /= d) * t;
		}

		private static float Cubic(float t, float d = 1)
		{
			return (t /= d) * t * t;
		}

		private static float Quart(float t, float d = 1)
		{
			return (t /= d) * t * t * t;
		}

		private static float Quint(float t, float d = 1)
		{
			return (t /= d) * t * t * t * t;
		}

		private static float Sine(float t, float d = 1)
		{
			return 1 - Mathf.Cos(t / d * (Mathf.PI / 2));
		}

		private static float Expo(float t, float d = 1)
		{
			return Mathf.Pow(2, 10 * (t / d - 1));
		}

		private static float Circ(float t, float d = 1)
		{
			return -(Mathf.Sqrt(1 - (t /= d) * t) - 1);
		}

		private static float Elastic(float t, float d = 1)
		{
			t /= d;
			var p = d * .3f;
			var s = p / 4;
			return -(Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p));
		}

		private static float Back(float t, float d = 1)
		{
			return (t /= d) * t * ((1.70158f + 1) * t - 1.70158f);
		}

		private static float Bounce(float t, float d = 1)
		{
			t = d - t;
			if ((t /= d) < (1 / 2.75f))
				return 1 - (7.5625f * t * t);
			else if (t < (2 / 2.75f))
				return 1 - (7.5625f * (t -= (1.5f / 2.75f)) * t + .75f);
			else if (t < (2.5f / 2.75f))
				return 1 - (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f);
			else
				return 1 - (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f);
		}

		// Private Function List
		private static Func<float, float, float, float>[] funcList = {
			Lerp,
			QuadIn,
			QuadOut,
			QuadInOut,
			QuadOutIn,
			CubicIn,
			CubicOut,
			CubicInOut,
			CubicOutIn,
			QuartIn,
			QuartOut,
			QuartInOut,
			QuartOutIn,
			QuintIn,
			QuintOut,
			QuintInOut,
			QuintOutIn,
			SineIn,
			SineOut,
			SineInOut,
			SineOutIn,
			ExpoIn,
			ExpoOut,
			ExpoInOut,
			ExpoOutIn,
			CircIn,
			CircOut,
			CircInOut,
			CircOutIn,
			ElasticIn,
			ElasticOut,
			ElasticInOut,
			ElasticOutIn,
			BackIn,
			BackOut,
			BackInOut,
			BackOutIn,
			BounceIn,
			BounceOut,
			BounceInOut,
			BounceOutIn,
		};

		#endregion
	}

    public class EasingParamFloat
    {
        public float value = 0;

        private float start = 0;
        private float end = 0;
        private float duration = 0;
        private float time = 0;
        private EaseType easeType = EaseType.Lerp;

        public void Init()
        {
            start = 0;
            end = 0;
            value = 0;
            duration = time = 0;
            easeType = EaseType.Lerp;
        }

        public void SetUp(EaseType easeType, float start, float end, float time)
        {
            this.easeType = easeType;
            this.start = start;
            this.value = start;
            this.end = end;
            this.time = time;
            this.duration = 0;
        }

        public float Update(float dt)
        {
            if (duration < time)
            {
                duration = Mathf.Min(duration + dt, time);

                float d = duration / time;

                value = Easing.Ease(easeType, start, end, d);
                return value;
            }
            else
            {
                value = end;
                return value;
            }
        }
    }

    //public class EasingParam<T> where T : struct
    //{
    //    public T value;

    //    protected T start;
    //    protected T end;
    //    protected float duration;
    //    protected float time;
    //    protected EaseType easeType;

    //    public void SetUp(EaseType easeType, T start, T end, float time)
    //    {
    //        this.easeType = easeType;
    //        this.start = start;
    //        this.value = start;
    //        this.end = end;
    //        this.time = time;
    //        this.duration = 0;
    //    }

    //    public T Update(float dt)
    //    {
    //        if (duration < time)
    //        {
    //            duration = Mathf.Min(duration + dt, time);

    //            float d = duration / time;

    //            value = Easing.Ease(easeType, start, end, d);
    //            return value;
    //        }
    //        else
    //        {
    //            value = end;
    //            return value;
    //        }
    //    }
    //}

    public abstract class EasingParamBase<T> where T : struct
    {
        public T value;

        public T start;
        public T end;
        public float duration;
        public float time;
        public EaseType easeType;

        public bool isEnd;

        public EasingParamBase()
        {
            Init();
        }

        public abstract void Init();

        protected abstract T InternalEase(float d);

        public void SetUp(EaseType easeType, T start, T end, float time)
        {
            this.easeType = easeType;
            this.start = start;
            this.value = start;
            this.end = end;
            this.time = time;
            this.duration = 0;
            this.isEnd = false;
        }

        public T Update(float dt)
        {
            if (duration < time)
            {
                duration = Mathf.Min(duration + dt, time);
                if(duration == time)
                {
                    isEnd = true;
                }

                float d = duration / time;

                value = InternalEase(d);
                return value;
            }
            else
            {
                value = end;
                return value;
            }
        }
    }

    public class EasingParamVector2 : EasingParamBase<Vector2>
    {
        public override void Init()
        {
            start = Vector2.zero;
            end = Vector2.zero;
            duration = time = 0;
            easeType = EaseType.Lerp;
        }

        protected override Vector2 InternalEase(float d)
        {
            return Easing.Ease(easeType, start, end, d);
        }
    }

    public class EasingParamVector3 : EasingParamBase<Vector3>
    {
        public override void Init()
        {
            start = Vector3.zero;
            end = Vector3.zero;
            duration = time = 0;
            easeType = EaseType.Lerp;
        }

        protected override Vector3 InternalEase(float d)
        {
            return Easing.Ease(easeType, start, end, d);
        }
    }

    public class EasingParamVector4 : EasingParamBase<Vector4>
    {
        public override void Init()
        {
            start = Vector4.zero;
            end = Vector4.zero;
            duration = time = 0;
            easeType = EaseType.Lerp;
        }

        protected override Vector4 InternalEase(float d)
        {
            return Easing.Ease(easeType, start, end, d);
        }
    }

    public class EasingParamColor : EasingParamBase<Color>
    {
        public override void Init()
        {
            start = Color.black;
            end = Color.black;
            duration = time = 0;
            easeType = EaseType.Lerp;
        }

        protected override Color InternalEase(float d)
        {
            return Easing.Ease(easeType, start, end, d);
        }
    }

    public class EasingParamQuaternion : EasingParamBase<Quaternion>
    {
        public override void Init()
        {
            start = Quaternion.identity;
            end = Quaternion.identity;
            duration = time = 0;
            easeType = EaseType.Lerp;
        }

        protected override Quaternion InternalEase(float d)
        {
            return Easing.Ease(easeType, start, end, d);
        }
    }
    //    public override void Init()
    //    {
    //        start = Vector2.zero;
    //        end = Vector2.zero;
    //        value = Vector2.zero;
    //        duration = time = 0;
    //        easeType = EaseType.Lerp;
    //    }
    //    public override void SetUp(EaseType easeType, Vector2 start, Vector2 end, float time)
    //    {
    //        this.easeType = easeType;
    //        this.start = start;
    //        this.value = start;
    //        this.end = end;
    //        this.time = time;
    //        this.duration = 0;
    //    }
    //    public override Vector2 Update(float dt)
    //    {
    //        if (duration < time)
    //        {
    //            duration = Mathf.Min(duration + dt, time);

    //            float d = duration / time;

    //            value = Easing.Ease(easeType, start, end, d);
    //            return value;
    //        }
    //        else
    //        {
    //            value = end;
    //            return value;
    //        }
    //    }
    //}

    //public class EasingParamVector3 : EasingParamBase<Vector3>
    //{
    //    public override void Init()
    //    {
    //        start = Vector3.zero;
    //        end = Vector3.zero;
    //        value = Vector3.zero;
    //        duration = time = 0;
    //        easeType = EaseType.Lerp;
    //    }
    //    public override void SetUp(EaseType easeType, Vector3 start, Vector3 end, float time)
    //    {
    //        this.easeType = easeType;
    //        this.start = start;
    //        this.value = start;
    //        this.end = end;
    //        this.time = time;
    //        this.duration = 0;
    //    }
    //    public override Vector3 Update(float dt)
    //    {
    //        if (duration < time)
    //        {
    //            duration = Mathf.Min(duration + dt, time);

    //            float d = duration / time;

    //            value = Easing.Ease(easeType, start, end, d);
    //            return value;
    //        }
    //        else
    //        {
    //            value = end;
    //            return value;
    //        }
    //    }
    //}

    //public class EasingParamColor : EasingParamBase<Color>
    //{
    //    public override void Init()
    //    {
    //        start = Color.black;
    //        end = Color.black;
    //        value = Color.black;
    //        duration = time = 0;
    //        easeType = EaseType.Lerp;
    //    }
    //    public override void SetUp(EaseType easeType, Color start, Color end, float time)
    //    {
    //        this.easeType = easeType;
    //        this.start = start;
    //        this.value = start;
    //        this.end = end;
    //        this.time = time;
    //        this.duration = 0;
    //    }
    //    public override Quaternion Update(float dt)
    //    {
    //        if (duration < time)
    //        {
    //            duration = Mathf.Min(duration + dt, time);

    //            float d = duration / time;

    //            value = Easing.Ease(easeType, start, end, d);
    //            return value;
    //        }
    //        else
    //        {
    //            value = end;
    //            return value;
    //        }
    //    }
    //}

    //public class EasingParamQuaternion : EasingParamBase<Quaternion>
    //{
    //    public override void Init()
    //    {
    //        start = Quaternion.identity;
    //        end = Quaternion.identity;
    //        value = Quaternion.identity;
    //        duration = time = 0;
    //        easeType = EaseType.Lerp;
    //    }
    //    public override void SetUp(EaseType easeType, Quaternion start, Quaternion end, float time)
    //    {
    //        this.easeType = easeType;
    //        this.start = start;
    //        this.value = start;
    //        this.end = end;
    //        this.time = time;
    //        this.duration = 0;
    //    }
    //    public override Quaternion Update(float dt)
    //    {
    //        if (duration < time)
    //        {
    //            duration = Mathf.Min(duration + dt, time);

    //            float d = duration / time;

    //            value = Easing.Ease(easeType, start, end, d);
    //            return value;
    //        }
    //        else
    //        {
    //            value = end;
    //            return value;
    //        }
    //    }
    //}
}