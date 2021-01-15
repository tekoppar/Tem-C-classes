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
    public partial class Vector3Control : UserControl {
        private Vector3 vector = new Vector3(0, 0, 0);

        public event EventHandler OnValueChangedNoArgs;
        protected virtual void ValueChangedVec3NoArgs(EventArgs e) {
            EventHandler handler = OnValueChangedNoArgs;
            if (handler != null) {
                handler(this, e);
            }
        }
        public event EventHandler<ValuechangedVec3EventArgs> OnValueChanged;
        protected virtual void ValueChangedVec3(ValuechangedVec3EventArgs e) {
            EventHandler<ValuechangedVec3EventArgs> handler = OnValueChanged;
            if (handler != null) {
                handler(this, e);
            }
        }
        public Vector3Control() {
            InitializeComponent();
        }

        private void value_ValueChanged(object sender, EventArgs e) {
            NumericUpDown numeric = sender as NumericUpDown;

            switch (numeric.Name) {
                case "valueX": this.vector.X = (float)numeric.Value; break;
                case "valueY": this.vector.Y = (float)numeric.Value; break;
                case "valueZ": this.vector.Z = (float)numeric.Value; break;
            }

            ValuechangedVec3EventArgs args = new ValuechangedVec3EventArgs();
            args.vector = this.vector;
            ValueChangedVec3(args);
            ValueChangedVec3NoArgs(EventArgs.Empty);
        }

        public Vector3 GetValue() {
            return this.vector;
        }

        public void SetValue(Vector3 value) {
            this.vector = value;

            if (this.vector.X > (float)decimal.MinValue && this.vector.X < (float)decimal.MaxValue)
                valueX.Value = (decimal)this.vector.X;
            else
                valueX.Value = -1;

            if (this.vector.Y > (float)decimal.MinValue && this.vector.Y < (float)decimal.MaxValue)
                valueY.Value = (decimal)this.vector.Y;
            else
                valueY.Value = -1;

            if (this.vector.Z > (float)decimal.MinValue && this.vector.Z < (float)decimal.MaxValue)
                valueZ.Value = (decimal)this.vector.Z;
            else
                valueZ.Value = -1;
        }
    }

    public class ValuechangedVec3EventArgs : EventArgs {
        public Vector3 vector { get; set; }
    }
}
