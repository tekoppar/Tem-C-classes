using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;

namespace Tem.TemClass
{ 
    public class HSV
    {
        public float h;
        public float s;
        public float b;

        public HSV(Color color)
        {
            this.h = color.GetHue();
            this.s = color.GetSaturation();
            this.b = getBrightness(color);
        }

        public HSV(float h, float s, float b)
        {
            this.h = h;
            this.s = s;
            this.b = b;
        }

        public HSV()
        {
            this.h = 0.0f;
            this.s = 0.0f;
            this.b = 0.0f;
        }

        static float getBrightness(Color c) { return (c.R * 0.299f + c.G * 0.587f + c.B * 0.114f) / 256f; }

        public static HSV ColorToHSV(Color color)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));
            HSV hsv = new HSV();
            hsv.h = color.GetHue();
            hsv.s = (float)((max == 0) ? 0 : 1d - (1d * min / max));
            hsv.b = (float)(max / 255d);

            return hsv;
        }

        public static Color ColorFromHSV(HSV hsv)
        {
            int hi = Convert.ToInt32(Math.Floor(hsv.h / 60)) % 6;
            double f = hsv.h / 60 - Math.Floor(hsv.h / 60);

            hsv.b = hsv.b * 255;
            int v = Convert.ToInt32(hsv.b);
            int p = Convert.ToInt32(hsv.b * (1 - hsv.s));
            int q = Convert.ToInt32(hsv.b * (1 - f * hsv.s));
            int t = Convert.ToInt32(hsv.b * (1 - (1 - f) * hsv.s));

            hsv.b = hsv.b / 255;
            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
    }

    public struct TColor
    {
        public float r { get; set; }
        public float g { get; set; }
        public float b { get; set; }
        public float a { get; set; }
        public TColor(float r, float g, float b, float a)
        {
            if (r <= 1.0f && g <= 1.0f && b <= 1.0f && a <= 1.0f)
            {
                this.r = r * 255.0f;
                this.g = g * 255.0f;
                this.b = b * 255.0f;
                this.a = a * 255.0f;
            }
            else
            {
                this.r = r;
                this.g = g;
                this.b = b;
                this.a = a;
            }
        }
        public TColor(Color color)
        {
            this.r = color.R;
            this.g = color.G;
            this.b = color.B;
            this.a = color.A;
        }

        public TColor(string r, string g, string b, string a)
        {
            this.r = float.Parse(r, CultureInfo.CreateSpecificCulture("en-US"));
            this.g = float.Parse(g, CultureInfo.CreateSpecificCulture("en-US"));
            this.b = float.Parse(b, CultureInfo.CreateSpecificCulture("en-US"));
            this.a = float.Parse(a, CultureInfo.CreateSpecificCulture("en-US"));
        }
        public TColor(string value)
        {
            string[] values = value.Split(',');
            this.r = float.Parse(values[0], CultureInfo.CreateSpecificCulture("en-US"));
            this.g = float.Parse(values[1], CultureInfo.CreateSpecificCulture("en-US"));
            this.b = float.Parse(values[2], CultureInfo.CreateSpecificCulture("en-US"));

            if (values.Length >= 4)
                this.a = float.Parse(values[3], CultureInfo.CreateSpecificCulture("en-US"));
            else
                this.a = 1.0f;
        }
        public Color ToColor()
        {
            return Color.FromArgb((int)this.a, (int)this.r, (int)this.g, (int)this.b);
        }
        public string ToJsonString()
        {
            string json = "{\"r\":" + this.r.ToString() + ",\"g\":" + this.g.ToString() + ",\"b\":" + this.b.ToString() + ",\"a\":" + this.a.ToString() + "}";
            return json;
        }
        public override string ToString()
        {
            return this.r.ToString(CultureInfo.CreateSpecificCulture("en-US")) + ", " + this.g.ToString(CultureInfo.CreateSpecificCulture("en-US")) + ", " + this.b.ToString(CultureInfo.CreateSpecificCulture("en-US")) + ", " + this.a.ToString(CultureInfo.CreateSpecificCulture("en-US"));
        }
    }
}