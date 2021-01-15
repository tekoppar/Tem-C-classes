using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using Tem.TemClass;

namespace Tem.TemUI
{
    public partial class Vector4Control : UserControl {
        private TColor vector = new TColor(0, 0, 0, 0);
        public event EventHandler OnValueChangedNoArgs;
        protected virtual void ValueChangedVec4NoArgs(EventArgs e) {
            EventHandler handler = OnValueChangedNoArgs;
            if (handler != null) {
                handler(this, e);
            }
        }
        public event EventHandler<ValuechangedVec4EventArgs> OnValueChanged;
        protected virtual void ValueChangedVec4(ValuechangedVec4EventArgs e) {
            EventHandler<ValuechangedVec4EventArgs> handler = OnValueChanged;
            if (handler != null) {
                handler(this, e);
            }
        }
        public Vector4Control() {
            InitializeComponent();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        }

        private void value_ValueChanged(object sender, EventArgs e) {
            NumericUpDown numeric = sender as NumericUpDown;

            switch (numeric.Name) {
                case "valueX": this.vector.r = (float)numeric.Value; break;
                case "valueY": this.vector.g = (float)numeric.Value; break;
                case "valueZ": this.vector.b = (float)numeric.Value; break;
                case "valueA": this.vector.a = (float)numeric.Value; break;
            }

            ValuechangedVec4EventArgs args = new ValuechangedVec4EventArgs();
            args.vector = this.vector;
            ValueChangedVec4(args);
            ValueChangedVec4NoArgs(EventArgs.Empty);
        }

        public TColor GetValue() {
            return this.vector;
        }

        public void SetValue(TColor value) {
            this.vector = value;

            if (this.vector.a > (float)decimal.MinValue && this.vector.a < (float)decimal.MaxValue)
                valueA.Value = (decimal)this.vector.a;
            else
                valueA.Value = -1;

            if (this.vector.r > (float)decimal.MinValue && this.vector.r < (float)decimal.MaxValue)
                valueX.Value = (decimal)this.vector.r;
            else
                valueX.Value = -1;

            if (this.vector.g > (float)decimal.MinValue && this.vector.g < (float)decimal.MaxValue)
                valueY.Value = (decimal)this.vector.g;
            else
                valueY.Value = -1;

            if (this.vector.b > (float)decimal.MinValue && this.vector.b < (float)decimal.MaxValue)
                valueZ.Value = (decimal)this.vector.b;
            else
                valueZ.Value = -1;
        }

        private void Vector4Control_Paint(object sender, PaintEventArgs e) {
            this.Width = 200;
        }
    }

    public class ValuechangedVec4EventArgs : EventArgs {
        public TColor vector { get; set; }
    }
}
