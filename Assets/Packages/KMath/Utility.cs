using UnityEngine;
using System.Collections;

namespace KUtil
{
    public static class Utility
    {
        /// <summary>
        /// 点Pと線ABの距離を求める
        /// </summary>
        /// <param name="p"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float DistanceDotAndLine(Vector3 p, Vector3 a, Vector3 b)
        {
            Vector3 ab = b - a;
            Vector3 ap = p - a;

            //AB、APを外積して求められたベクトルの長さが、平行四辺形Dの面積になる
            float d = Vector3.Magnitude(Vector3.Cross(ab, ap));

            //AB間の距離
            float l = Vector3.Distance(a, b);

            return d / l;
        }

        // 点Pと線ABの交点(最短距離の点)を求める
        public static Vector3 CrossDotAndLine(Vector3 p, Vector3 a, Vector3 b)
        {
            Vector3 ab = b - a;
            Vector3 ap = p - a;

            float r = Vector3.Dot(ab, ap) / Vector3.Dot(ab, ab);

            if (r <= 0.0)
            {
                return a;
            }
            else if (r >= 1.0)
            {
                return b;
            }
            else
            {
                return new Vector3(a.x + r * ab.x, a.y + r * ab.y, a.z + r * ab.z);
            }
        }

        /// <summary>
        /// 0～1の範囲で返すsmoothstep
        /// </summary>
        /// <param name="min">下限</param>
        /// <param name="max">上限</param>
        /// <param name="x">値</param>
        /// <returns></returns>
        public static float smoothStep(float min, float max, float x)
        {
            x = Mathf.Clamp((x - min) / (max - min), 0.0f, 1.0f);
            return x * x * (3.0f - 2.0f * x);
        }

        /// <summary>
        /// 極座標系→ユークリッド座標系（緯度と経度指定で３次元座標を返す）
        /// </summary>
        /// <param name="radY">緯度・仰角(radian)</param>
        /// <param name="radX">経度・方位角(radian)</param>
        /// <param name="r">半径</param>
        /// <returns>３次元座標(x,y,z)</returns>
        public static Vector3 GetPositionOnSphere(float radY, float radX, float r)
        {
            Vector3 p;

            p.x = Mathf.Cos(radY) * Mathf.Cos(radX) * r;
            p.y = Mathf.Sin(radY) * r;
            p.z = Mathf.Cos(radY) * Mathf.Sin(radX) * r;
            
            return p;
        }

        /// <summary>
        /// 水平視野角を取得
        /// </summary>
        /// <param name="fov"></param>
        /// <returns></returns>
        public static float GetCameraHorizontalAngle(float fov)
        {
            float w = Screen.width;
            float h = Screen.height;
            float dist = Mathf.Cos(fov / 2f) * (h / 2f) / Mathf.Sin(fov / 2f);
            return Mathf.Atan((w / 2f) / dist) * 2f;
        }
    }


}