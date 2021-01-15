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

namespace Tem.TemUI {
    public partial class BoundsControl : UserControl {
        private Bounds bounds = new Bounds(new Vector3(0, 0, 0), new Vector3(0, 0, 0));
        private bool PlayerChangedValues = true;

        public event EventHandler OnValueChangedNoArgs;
        protected virtual void ValueChangedBoundsNoArgs(EventArgs e) {
            EventHandler handler = OnValueChangedNoArgs;
            if (handler != null) {
                handler(this, e);
            }
        }
        public event EventHandler<ValuechangedBoundsEventArgs> OnValueChanged;
        protected virtual void ValueChangedBounds(ValuechangedBoundsEventArgs e) {
            EventHandler<ValuechangedBoundsEventArgs> handler = OnValueChanged;
            if (handler != null) {
                handler(this, e);
            }
        }
        public BoundsControl() {
            InitializeComponent();
        }

        public Bounds GetValue() {
            return this.bounds;
        }

        public void SetValue(Vector3 center, Vector3 extents) {
            this.bounds.center = center;
            this.bounds.extents = extents;
            PlayerChangedValues = false;
            this.vector3Center.SetValue(center);
            this.vector3Extents.SetValue(extents);
            PlayerChangedValues = true;
        }

        private void vector3Control_OnValueChanged(object sender, ValuechangedVec3EventArgs e) {
            if (PlayerChangedValues == true) {
                Vector3Control control = sender as Vector3Control;
                if (control.Name == "vector3Center")
                    this.bounds.center = e.vector;
                else if (control.Name == "vector3Extents")
                    this.bounds.extents = e.vector;

                ValuechangedBoundsEventArgs args = new ValuechangedBoundsEventArgs();
                args.bounds = this.bounds;
                ValueChangedBounds(args);
                ValueChangedBoundsNoArgs(EventArgs.Empty);
            }
        }
    }

    public class ValuechangedBoundsEventArgs : EventArgs {
        public Bounds bounds { get; set; }
    }

    public class Bounds {
        public Vector3 center { get; set; } = new Vector3(0, 0, 0);
        public Vector3 extents { get; set; } = new Vector3(0, 0, 0);

        public Bounds() {
            this.center = new Vector3(0, 0, 0);
            this.extents = new Vector3(0, 0, 0);
        }
        public Bounds (Vector3 center, Vector3 extents) {
            this.center = center;
            this.extents = extents;
        }
        public Bounds(string s) {
            string[] vectors = s.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            if (vectors.Count() >= 1)
                this.center = new Vector3(vectors[0]);
            else
                this.center = new Vector3(0);

            if (vectors.Count() >= 2)
                this.extents = new Vector3(vectors[1]);
            else
                this.center = new Vector3(0);
        }

        public override string ToString() {
            return this.center.X.ToString(CultureInfo.CreateSpecificCulture("en-US")) + ", " + this.center.Y.ToString(CultureInfo.CreateSpecificCulture("en-US")) + ", " + this.center.Z.ToString(CultureInfo.CreateSpecificCulture("en-US")) + ";" + this.extents.X.ToString(CultureInfo.CreateSpecificCulture("en-US")) + ", " + this.extents.Y.ToString(CultureInfo.CreateSpecificCulture("en-US")) + ", " + this.extents.Z.ToString(CultureInfo.CreateSpecificCulture("en-US"));
        }
    }
}
