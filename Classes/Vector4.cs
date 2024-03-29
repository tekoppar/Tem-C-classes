﻿using System.Runtime.InteropServices;
using System.Drawing;
using System.Globalization;
using System;

namespace Tem.TemClass
{
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 1)]
    public struct Vector4 {
        [FieldOffset(0)]
        public float X;
        [FieldOffset(4)]
        public float Y;
        [FieldOffset(8)]
        public float W;
        [FieldOffset(12)]
        public float H;

        /*public Vector4(string cordinates) {
            string[] cords = cordinates.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            if (cords.Length == 4) {
                float temp = 0;
                float.TryParse(cords[0], out temp);
                this.X = temp;
                float.TryParse(cords[1], out temp);
                this.Y = temp;
                float.TryParse(cords[2], out temp);
                this.W = temp;
                float.TryParse(cords[3], out temp);
                this.H = temp;
            } else {
                this.X = 0;
                this.Y = 0;
                this.W = 0;
                this.H = 0;
            }
        }*/
        public Vector4(Vector2 pos, float w, float h, bool center) {
            if (center) {
                this.X = pos.X - (w / 2);
                this.Y = pos.Y + (h / 2);
            } else {
                this.X = pos.X;
                this.Y = pos.Y;
            }

            this.W = w;
            this.H = h;
        }
        public Vector4(Vector3 pos, float w, float h, bool center) {
            if (center) {
                this.X = pos.X - (w / 2);
                this.Y = pos.Y + (h / 2);
            } else {
                this.X = pos.X;
                this.Y = pos.Y;
            }

            this.W = w;
            this.H = h;
        }
        public Vector4(string s) {
            string[] values = s.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (values.Length >= 3) {
                this.X = float.Parse(values[0], CultureInfo.CreateSpecificCulture("en-US"));
                this.Y = float.Parse(values[1], CultureInfo.CreateSpecificCulture("en-US"));
                this.W = float.Parse(values[2], CultureInfo.CreateSpecificCulture("en-US"));
                this.H = float.Parse(values[3], CultureInfo.CreateSpecificCulture("en-US"));
            } else {
                this.X = this.Y = this.W = this.H = -1.0f;
            }
        }
        public Vector2 GetCenter() {
            return new Vector2() { X = X + (W / 2), Y = Y - (H / 2) };
        }
        public bool Intersects(Vector4 other) {
            return X + W >= other.X && other.X + other.W >= X && Y - H <= other.Y && other.Y - other.H <= Y;
        }
        public override string ToString() {
            return $"{X:0.00}, {Y:0.00}, {W:0.00}, {H:0.00}";
        }
    }
}