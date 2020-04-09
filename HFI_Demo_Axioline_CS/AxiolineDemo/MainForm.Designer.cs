namespace HFI_Demo_Axioline_CS
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.axlDeviceCtrl1 = new PhoenixContact.HFI.Axioline.WinFormComponents.AxlDeviceCtrl();
            this.axlControllerCtrl1 = new PhoenixContact.HFI.Axioline.WinFormComponents.AxlControllerCtrl();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // axlDeviceCtrl1
            // 
            this.axlDeviceCtrl1.ControlText = "AxlDeviceCtrl";
            this.axlDeviceCtrl1.EditActivate = false;
            this.axlDeviceCtrl1.Location = new System.Drawing.Point(12, 333);
            this.axlDeviceCtrl1.Name = "axlDeviceCtrl1";
            this.axlDeviceCtrl1.Size = new System.Drawing.Size(755, 371);
            this.axlDeviceCtrl1.TabIndex = 0;
            this.axlDeviceCtrl1.Load += new System.EventHandler(this.axlDeviceCtrl1_Load);
            // 
            // axlControllerCtrl1
            // 
            this.axlControllerCtrl1.ControlText = "IAxlControllerCtrl";
            this.axlControllerCtrl1.Location = new System.Drawing.Point(12, 12);
            this.axlControllerCtrl1.Name = "axlControllerCtrl1";
            this.axlControllerCtrl1.Size = new System.Drawing.Size(755, 315);
            this.axlControllerCtrl1.TabIndex = 1;
            this.axlControllerCtrl1.Load += new System.EventHandler(this.axlControllerCtrl1_Load);
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 713);
            this.Controls.Add(this.axlControllerCtrl1);
            this.Controls.Add(this.axlDeviceCtrl1);
            this.Name = "MainForm";
            this.Text = "HFI Demo Axioline C#";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private PhoenixContact.HFI.Axioline.WinFormComponents.AxlDeviceCtrl axlDeviceCtrl1;
        private PhoenixContact.HFI.Axioline.WinFormComponents.AxlControllerCtrl axlControllerCtrl1;
        private System.Windows.Forms.Timer updateTimer;
    }
}