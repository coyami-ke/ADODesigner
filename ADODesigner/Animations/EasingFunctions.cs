using ADODesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable 1591
namespace ADODesigner.Animations
{
    public static class EasingFunctions
    {
        public static float ApplyFunction(Ease ease, float t)
        {
            if (ease == Ease.Linear) return t;
            if (ease == Ease.IsSine) return InSine(t);
            if (ease == Ease.OutSine) return OutSine(t);
            if (ease == Ease.InOutSine) return InOutSine(t);
            if (ease == Ease.InQuad) return InQuad(t);
            if (ease == Ease.OutQuad) return OutQuad(t);
            if (ease == Ease.InCubic) return InCubic(t);
            if (ease == Ease.OutCubic) return OutCubic(t);
            if (ease == Ease.InOutCubic) return InOutCubic(t);
            if (ease == Ease.OutQuart) return OutQuart(t);
            if (ease == Ease.InQuart) return InQuart(t);
            if (ease == Ease.InOutQuart) return InQuart(t);
            if (ease == Ease.OutQuint) return OutQuart(t);
            if (ease == Ease.InQuint) return InQuart(t);
            if (ease == Ease.InOutQuint) return InQuart(t);
            if (ease == Ease.InExpo) return InExpo(t);
            if (ease == Ease.OutExpo) return OutExpo(t);
            if (ease == Ease.InOutExpo) return InOutExpo(t);
            if (ease == Ease.InCirc) return InCirc(t);
            if (ease == Ease.OutCirc) return OutCirc(t);
            if (ease == Ease.OutBack) return OutBack(t);
            if (ease == Ease.InBack) return InBack(t);
            if (ease == Ease.InOutBack) return InOutBack(t);
            if (ease == Ease.InElastic) return InElastic(t);
            if (ease == Ease.OutElastic) return OutElastic(t);
            if (ease == Ease.OutBounce) return OutBounce(t);
            if (ease == Ease.InBounce) return InBounce(t);
            if (ease == Ease.InOutBounce) return InOutBounce(t);
            return 1;
        }

        public static Vector2 ApplyFunctionVector2(Ease ease, Vector2 t)
        {
            Vector2 result = new Vector2();
            result.X = ApplyFunction(ease, t.X);
            result.Y = ApplyFunction(ease, t.Y);
            return result;
        }

        public static float InSine(float t)
        {
            return 1 - MathF.Cos((t * MathF.PI) / 2);
        }

        public static float OutSine(float t)
        {
            return MathF.Sin((t * MathF.PI) / 2);
        }

        public static float InOutSine(float t)
        {
            return -(MathF.Cos(MathF.PI * t) - 1) / 2;
        }
        public static float InQuad(float t)
        {
            return t * t;
        }
        public static float OutQuad(float t)
        {
            return 1 - (1 - t) * (1 - t);
        }
        public static float InOutQuad(float t)
        {
            if (t < 0.5)
                return 2 * t * t;
            else
                return 1 - 2 * (1 - t) * (1 - t);
        }
        public static float InCubic(float t)
        {
            return t * t * t;
        }

        public static float OutCubic(float t)
        {
            float t1 = t - 1;
            return t1 * t1 * t1 + 1;
        }

        public static float InOutCubic(float t)
        {
            if (t < 0.5f)
            {
                return 4 * t * t * t;
            }
            else
            {
                float t1 = 2 * t - 2;
                return 0.5f * t1 * t1 * t1 + 1;
            }
        }
        public static float InQuart(float t)
        {
            return t * t * t * t;
        }
        public static float OutQuart(float t)
        {
            return 1 - MathF.Pow(1 - t, 4);
        }
        public static float InOutQuart(float t)
        {
            return t < 0.5 ? 8 * t * t * t * t : 1 - MathF.Pow(-2 * t + 2, 4) / 2;
        }
        public static float InQuint(float t)
        {
            return t * t * t * t * t;
        }
        public static float OutQuint(float t)
        {
            return 1 - MathF.Pow(1 - t, 5);
        }
        public static float InOutQuint(float t)
        {
            return t < 0.5 ? 16 * t * t * t * t : 1 - MathF.Pow(-2 * t + 2, 5) / 2;
        }
        public static float InExpo(float t)
        {
            return t == 0 ? 0 : MathF.Pow(2, 10 * t - 10);
        }
        public static float OutExpo(float t)
        {
            return t == 1 ? 1 : 1 - MathF.Pow(2, -10 * t);
        }
        public static float InOutExpo(float t)
        {
            if (t <= 0.0) return 0;
            if (t >= 1.0) return 1;
            return t < 0.5F ? 0.5F * MathF.Pow(2, 10 * (t - 1)) : 0.5F * (-MathF.Pow(2, -10 * (t - 1)) + 2);
        }
        public static float OutCirc(float t)
        {
            if (t <= 0.0) return 0;
            if (t >= 1.0) return 1;
            return MathF.Sqrt(1 - (t - 1) * (t - 1));
        }
        public static float InCirc(float t)
        {
            if (t <= 0) return 0;
            if (t >= 1) return 1;
            return 1 - MathF.Sqrt(1 - t * t);
        }
        public static float InOutCirc(float t)
        {
            if (t <= 0) return 0;
            if (t >= 1) return 1;
            return t < 0.5
                ? (float)0.5 * (1 - MathF.Sqrt(1 - 4 * t * t))
                : (float)0.5 * (MathF.Sqrt(1 - (t * 2 - 2) * (t * 2 - 2)) + 1);
        }
        public static float InBack(float t)
        {
            const float c1 = 1.70158F;
            return t * t * ((c1 + 1) * t - c1);
        }
        public static float OutBack(float t)
        {
            const float c1 = 1.70158F;
            return 1 - ((1 - t) * (1 - t) * ((c1 + 1) * (1 - t) - c1));
        }
        public static float InOutBack(float t)
        {
            const float c1 = 1.70158F * 1.525F;
            return t < 0.5F
                ? 2F * t * t * ((c1 + 1) * 2 * t - c1) * 0.5F
                : 1F - ((2 - 2 * t) * (2 - 2 * t) * ((c1 + 1) * (2 - 2 * t) - c1) * 0.5F);
        }
        public static float InElastic(float t)
        {
            const float c4 = 2 * MathF.PI / 3;
            return t == 0 ? 0 : t == 1 ? 1 : -MathF.Pow(2, 10 * t - 10) * MathF.Sin((t * 10 - 10.75F) * c4);
        }
        public static float OutElastic(float t)
        {
            const float c4 = (2 * MathF.PI) / 3;
            return t == 0 ? 0 : t == 1 ? 1 : MathF.Pow(2, -10 * t) * MathF.Sin((t * 10 - 0.75F) * c4) + 1;
        }
        public static float InOutElastic(float t)
        {
            const float c5 = (2 * MathF.PI) / 4.5F;
            return t == 0 ? 0 : t == 1 ? 1 :
                t < 0.5F ? -(MathF.Pow(2, 20 * t - 10) * MathF.Sin((20 * t - 11.125F) * c5)) * 0.5F :
                (MathF.Pow(2, -20 * t + 10) * MathF.Sin((20 * t - 11.125F) * c5)) * 0.5F + 1;
        }
        public static float InBounce(float t)
        {
            return 1 - OutBounce(1 - t);
        }
        public static float OutBounce(float t)
        {
            if (t < 1.0 / 2.75)
            {
                return 7.5625F * t * t;
            }
            else if (t < 2.0 / 2.75)
            {
                t -= 1.5F / 2.75F;
                return 7.5625F * t * t + 0.75F;
            }
            else if (t < 2.5 / 2.75)
            {
                t -= 2.25F / 2.75F;
                return 7.5625F * t * t + 0.9375F;
            }
            else
            {
                t -= 2.625F / 2.75F;
                return 7.5625F * t * t + 0.984375F;
            }
        }
        public static float InOutBounce(float t)
        {
            return t < 0.5F ? 0.5F * InBounce(t * 2) : 0.5F * OutBounce(t * 2 - 1) + 0.5F;
        }
    }
}
