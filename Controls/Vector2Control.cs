using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tem.TemClass;

namespace Tem.TemUI
{
    public partial class Vector2Control : UserControl {
        private Vector2 vector = new Vector2(0, 0);

        public event EventHandler OnValueChangedNoArgs;
        protected virtual void ValueChangedVec2NoArgs(EventArgs e) {
            EventHandler handler = OnValueChangedNoArgs;
            if (handler != null) {
                handler(this, e);
            }
        }
        public event EventHandler<ValuechangedVec2EventArgs> OnValueChanged;
        protected virtual void ValueChangedVec2(ValuechangedVec2EventArgs e) {
            EventHandler<ValuechangedVec2EventArgs> handler = OnValueChanged;
            if (handler != null) {
                handler(this, e);
            }
        }
        public Vector2Control() {
            InitializeComponent();
        }

        private void value_ValueChanged(object sender, EventArgs e) {
            NumericUpDown numeric = sender as NumericUpDown;

            switch (numeric.Name) {
                case "valueX": this.vector.X = (float)numeric.Value; break;
                case "valueY": this.vector.Y = (float)numeric.Value; break;
            }

            ValuechangedVec2EventArgs args = new ValuechangedVec2EventArgs();
            args.vector = this.vector;
            ValueChangedVec2(args);
            ValueChangedVec2NoArgs(EventArgs.Empty);
        }

        public Vector2 GetValue() {
            return this.vector;
        }

        public void SetValue(Vector2 value) {
            this.vector = value;

            if (this.vector.X > (float)decimal.MinValue && this.vector.X < (float)decimal.MaxValue)
                valueX.Value = (decimal)this.vector.X;
            else
                valueX.Value = -1;

            if (this.vector.Y > (float)decimal.MinValue && this.vector.Y < (float)decimal.MaxValue)
                valueY.Value = (decimal)this.vector.Y;
            else
                valueY.Value = -1;
        }
    }

    public class ValuechangedVec2EventArgs : EventArgs {
        public Vector2 vector { get; set; }
    }
}
