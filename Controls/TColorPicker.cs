﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tem.TemClass;

namespace Tem.TemUI
{
    [JsonConverter(typeof(SystemTextJsonSamples.JsonConverterTColorPicker))]
    public partial class TColorPicker : UserControl {
        [Description("Displays the color values as text next to the color picker"), Category("Appearance")]
        public bool ShowColorValues {
            get {
                return this.ShowColorValue;
            }
            set {
                this.ShowColorValue = value;
                this.lblColorValues.Visible = this.ShowColorValue;
                this.lblColorValues.Text = this.Color.Value.R.ToString() + ", " + this.Color.Value.G.ToString() + ", " + this.Color.Value.B.ToString() + ", " + this.Color.Value.A.ToString();
            }
        }
        [Description("Sets the dimensions of the color picker"), Category("Appearance")]
        public Point PickerSize {
            get {
                return this.ColorPickerSize;
            }
            set {
                this.ColorPickerSize = value;
                if (this.colorPicker != null)
                {
                    this.colorPicker.Width = this.ColorPickerSize.X;
                    this.colorPicker.Height = this.ColorPickerSize.Y;
                }
            }
        }

        public event EventHandler<PickedColorEventArgs> OnPickedColor;
        protected virtual void PickedColor(PickedColorEventArgs e) {
            EventHandler<PickedColorEventArgs> handler = OnPickedColor;
            if (handler != null) {
                handler(this, e);
            }
        }

        static private ColorWheel ColorWheel;
        static private Color? CopyColor = null;
        public Color? Color { get; set; } = System.Drawing.Color.FromArgb(255, 255, 255, 255);
        private int Alpha = 255;
        private bool HasBeenInitialized = false;
        private bool ShowColorValue { get; set; } = true;
        private Point ColorPickerSize { get; set; } = new Point(64, 64);

        public static TColorPicker ColorPicker(bool initialize = false) {
            return new TColorPicker(initialize);
        }

        public static TColorPicker ColorPickrColors(float r = 1.0f, float g = 1.0f, float b = 1.0f, float a = 1.0f) {
            return new TColorPicker(r, g, b, a);
        }

        public TColorPicker(bool initialize = false) {
            if (initialize)
                Initialize();
        }
        private void Initialize() {
            if (this.HasBeenInitialized == false) {
                this.HasBeenInitialized = true;
                InitializeComponent();
            }
        }
        public TColorPicker(float r = 1.0f, float g = 1.0f, float b = 1.0f, float a = 1.0f) {
            int cA = Math.Min(0, Math.Max(255, (int)(a * 255.0f)));
            int cR = Math.Min(0, Math.Max(255, (int)(r * 255.0f)));
            int cG = Math.Min(0, Math.Max(255, (int)(g * 255.0f)));
            int cB = Math.Min(0, Math.Max(255, (int)(b * 255.0f)));

            this.Color = System.Drawing.Color.FromArgb(cA, cR, cG, cB);
            if (this.ShowColorValue == true) {
                this.lblColorValues.Visible = true;
            }
        }

        private void panel1_DoubleClick(object sender, EventArgs e) {
            if (this.Color.HasValue == true)
                TColorPicker.ColorWheel = new ColorWheel(this.Color.Value);
            else
                TColorPicker.ColorWheel = new ColorWheel();

            if (TColorPicker.ColorWheel.ShowDialog() == DialogResult.OK) {
                this.Color = TColorPicker.ColorWheel.ReturnColor;
                this.Alpha = this.Color.Value.A;
                this.colorPicker.Image = this.DrawPreviewColorImage(this.Color.Value, this.Alpha);
                colorPicker.SizeMode = PictureBoxSizeMode.StretchImage;
                this.lblColorValues.Text = this.Color.Value.R.ToString() + ", " + this.Color.Value.G.ToString() + ", " + this.Color.Value.B.ToString() + ", " + this.Color.Value.A.ToString();

                PickedColorEventArgs args = new PickedColorEventArgs();
                args.Color = this.Color.Value;
                PickedColor(args);
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                TColorPicker.CopyColor = this.Color;
            } else if (e.Button == MouseButtons.Right) {
                this.Color = TColorPicker.CopyColor;
                this.Alpha = this.Color.Value.A;
                this.colorPicker.Image = this.DrawPreviewColorImage(this.Color.Value, this.Alpha);
                colorPicker.SizeMode = PictureBoxSizeMode.StretchImage;
                this.lblColorValues.Text = this.Color.Value.R.ToString() + ", " + this.Color.Value.G.ToString() + ", " + this.Color.Value.B.ToString() + ", " + this.Color.Value.A.ToString();

                PickedColorEventArgs args = new PickedColorEventArgs();
                args.Color = this.Color.Value;
                PickedColor(args);
            }
        }

        private Image DrawPreviewColorImage(Color color, int alpha) {
            int imageSize = 32;
            var finalImage = new Bitmap(imageSize, imageSize, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(finalImage);
            Pen opaquePen = new Pen(System.Drawing.Color.FromArgb(alpha, color.R, color.G, color.B), imageSize);
            Rectangle rect = new Rectangle(0, 0, imageSize, imageSize);
            g.DrawRectangle(opaquePen, rect);
            return finalImage;
        }

        public void SetColor(Color color, bool init = false) {
            if (init == true)
                Initialize();

            this.Color = color;
            this.Alpha = this.Color.Value.A;

            if (this.HasBeenInitialized) {
                this.colorPicker.Image = this.DrawPreviewColorImage(color, color.A);
                colorPicker.SizeMode = PictureBoxSizeMode.StretchImage;
                this.lblColorValues.Text = this.Color.Value.R.ToString() + ", " + this.Color.Value.G.ToString() + ", " + this.Color.Value.B.ToString() + ", " + this.Color.Value.A.ToString();
            }
            /*PickedColorEventArgs args = new PickedColorEventArgs();
            args.Color = this.Color.Value;
            PickedColor(args);*/
        }
    }

    public class PickedColorEventArgs : EventArgs {
        public Color Color { get; set; }
    }
}

namespace SystemTextJsonSamples {
    internal sealed class JsonConverterTColorPicker : JsonConverter<Tem.TemUI.TColorPicker> {
        public override Tem.TemUI.TColorPicker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            Tem.TemUI.TColorPicker colorPicker = Tem.TemUI.TColorPicker.ColorPicker();
            string colorValues;
            using (var jsonDoc = JsonDocument.ParseValue(ref reader)) {
                colorValues = jsonDoc.RootElement.GetRawText();
            }
            int startOfR = colorValues.IndexOf("r\":");
            int endOfR = colorValues.IndexOf(",", startOfR);
            int startOfG = colorValues.IndexOf("g\":");
            int endOfG = colorValues.IndexOf(",", startOfG);
            int startOfB = colorValues.IndexOf("b\":");
            int endOfB = colorValues.IndexOf(",", startOfB);
            int startOfA = colorValues.IndexOf("a\":");
            int endOfA = colorValues.IndexOf("}", startOfA);
            TColor color = new TColor(float.Parse(colorValues.Substring(startOfR + 3, endOfR - (startOfR + 3)), CultureInfo.InvariantCulture.NumberFormat), float.Parse(colorValues.Substring(startOfG + 3, endOfG - (startOfG + 3)), CultureInfo.InvariantCulture.NumberFormat), float.Parse(colorValues.Substring(startOfB + 3, endOfB - (startOfB + 3)), CultureInfo.InvariantCulture.NumberFormat), float.Parse(colorValues.Substring(startOfA + 3, endOfA - (startOfA + 3)), CultureInfo.InvariantCulture.NumberFormat));
            colorPicker.SetColor(color.ToColor());
            return colorPicker;
        }

        public override void Write(Utf8JsonWriter writer, Tem.TemUI.TColorPicker value, JsonSerializerOptions options) {
            TColor color = new TColor(value.Color.HasValue == true ? value.Color.Value : Color.FromArgb(255, 255, 255, 255));
            //writer.WriteStringValue(color.ToJsonString());
            using (JsonDocument document = JsonDocument.Parse(color.ToJsonString())) {
                document.RootElement.WriteTo(writer);
            }
        }
    }
}
