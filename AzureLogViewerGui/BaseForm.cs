using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureLogViewerGui
{
    public class BaseForm : Form
    {
        private Dictionary<Control, bool> controlState = new Dictionary<Control, bool>();
        private int busyCount = 0;
        private bool isBusy = false;

        public BaseForm()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        }

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                if (value)
                    busyCount++;
                else
                    busyCount--;

                if (isBusy && busyCount == 0)
                    DisableBusy();
                else if (!isBusy && busyCount == 1)
                    EnableBusy();
            }
        }

        private void EnableBusy()
        {
            isBusy = true;
            controlState.Clear();
            foreach (Control c in this.Controls)
                SaveControlStateAndDisable(c);
            Cursor.Current = Cursors.WaitCursor;
        }

        private void DisableBusy()
        {
            isBusy = false;
            foreach (Control c in this.Controls)
                RestoreControlState(c);
            Cursor.Current = Cursors.Default;
        }

        #region ControlState

        private void SaveControlStateAndDisable(Control control)
        {
            controlState.Add(control, control.Enabled);
            //foreach (Control c in control.Controls)
            //{
            //    SaveControlStateAndDisable(c);
            //}
            control.Enabled = false;
        }
        
        private void RestoreControlState(Control control)
        {
            if (controlState.ContainsKey(control))
            {
                control.Enabled = controlState[control];
            }
            //foreach (Control c in control.Controls)
            //{
            //    RestoreControlState(c);
            //}
        }

        #endregion

    }
}
