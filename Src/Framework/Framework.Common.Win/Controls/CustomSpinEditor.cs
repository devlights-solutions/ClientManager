using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace Framework.Common.Win.Controls
{
    public class CustomSpinEditor : RadSpinEditor
    {
        public decimal? NullableValue
        {
            get
            {
                return (this.SpinElement as CustomSpinEditorElement).NullableValue;
            }
            set
            {
                (this.SpinElement as CustomSpinEditorElement).NullableValue = value;
            }
        }

        public CustomSpinEditor()
        {
            this.AutoSize = true;
            this.TabStop = false;
            base.SetStyle(ControlStyles.Selectable, true);
        }

        protected override void CreateChildItems(RadElement parent)
        {
            Type baseType = typeof(RadSpinEditor);
            CustomSpinEditorElement element = new CustomSpinEditorElement();
            element.RightToLeft = this.RightToLeft == System.Windows.Forms.RightToLeft.Yes;
            this.RootElement.Children.Add(element);

            this.LostFocus += (sender, args) =>
                              {
                                  element.SetValueAfterLostFocus();
                              };

            element.ValueChanging += spinElement_ValueChanging;
            element.ValueChanged += spinElement_ValueChanged;
            element.TextChanging += spinElement_TextChanging;

            element.KeyDown += OnSpinElementKeyDown;
            element.KeyPress += OnSpinElementKeyPress;
            element.KeyUp += OnSpinElementKeyUp;

            baseType.GetField("spinElement", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(this, element);
        }

        private Dictionary<string, MethodInfo> cache = new Dictionary<string, MethodInfo>();
        private void InvokeBaseMethod(string name, params object[] parameters)
        {
            if (!cache.ContainsKey(name))
            {
                cache[name] = typeof(RadSpinEditor).GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic);
            }

            cache[name].Invoke(this, parameters);
        }

        private void OnSpinElementKeyUp(object sender, KeyEventArgs e)
        {
            this.InvokeBaseMethod("OnSpinElementKeyUp", sender, e);
        }

        private void OnSpinElementKeyPress(object sender, KeyPressEventArgs e)
        {
            this.InvokeBaseMethod("OnSpinElementKeyPress", sender, e);
        }

        private void OnSpinElementKeyDown(object sender, KeyEventArgs e)
        {
            this.InvokeBaseMethod("OnSpinElementKeyDown", sender, e);
        }

        private void spinElement_TextChanging(object sender, TextChangingEventArgs e)
        {
            this.InvokeBaseMethod("spinElement_TextChanging", sender, e);
        }

        private void spinElement_ValueChanged(object sender, EventArgs e)
        {
            this.InvokeBaseMethod("spinElement_ValueChanged", sender, e);
        }

        private void spinElement_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            this.InvokeBaseMethod("spinElement_ValueChanging", sender, e);
        }

        protected override Size DefaultSize
        {
            get
            {
                return GetDpiScaledSize(new Size(100, 20));
            }
        }
    }

    public class CustomSpinEditorElement : RadSpinElement
    {
        private decimal? nullableValue;

        public decimal? NullableValue
        {
            get
            {
                return this.nullableValue;
            }
            set
            {
                this.nullableValue = value;
                if (value.HasValue)
                {
                    this.internalValue = value.Value;
                }
                else
                {
                    this.internalValue = 0m;
                }

                this.Validate();
                this.OnNullableValueChanged();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.NullableValue = this.GetValueFromText();
                e.Handled = true;
                return;
            }

            base.OnKeyDown(e);
        }
        
        public override bool Validate()
        {
            if (!this.NullableValue.HasValue)
            {
                this.TextBoxItem.Text = string.Empty;
                return true;
            }

            this.TextBoxItem.Text = this.GetTextFromNumber(this.NullableValue.HasValue ? this.NullableValue.Value : 0m, this.Hexadecimal,
                this.ThousandsSeparator, this.DecimalPlaces);

            return true;
        }

        private string GetTextFromNumber(decimal num, bool hex, bool thousands, int decimalPlaces)
        {
            if (hex)
            {
                return string.Format("{0:X}", (long)num);
            }

            return num.ToString((thousands ? "N" : "F") + decimalPlaces.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
        }

        public override void PerformStep(decimal step)
        {
            decimal value = this.GetValueFromText();

            try
            {
                decimal incValue = value + step;
                value = incValue;
            }
            catch (OverflowException)
            {
            }

            this.NullableValue = this.Constrain(value);
            this.Validate();
        }

        internal void SetValueAfterLostFocus()
        {
            decimal value = this.GetValueFromText();
            this.NullableValue = this.Constrain(value);
            this.Validate();
        }

       protected override Type ThemeEffectiveType
        {
            get
            {
                return typeof(RadSpinElement);
            }
        }

        public event EventHandler NullableValueChanged;

        protected virtual void OnNullableValueChanged()
        {
            if (this.NullableValueChanged != null)
            {
                this.NullableValueChanged(this, EventArgs.Empty);
            }
        }
    }
}
