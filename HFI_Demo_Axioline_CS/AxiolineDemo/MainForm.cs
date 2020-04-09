#region Copyright
///////////////////////////////////////////////////////////////////////////////
//
//  Copyright PHOENIX CONTACT Software GmbH
//
///////////////////////////////////////////////////////////////////////////////
#endregion

namespace HFI_Demo_Axioline_CS
{
    using System;
    using System.Windows.Forms;

    using PhoenixContact.HFI.Axioline;
    using PhoenixContact.HFI.Axioline.AxlDevices;
    using PhoenixContact.PxC_Library.Util;

    public partial class MainForm : Form
    {
        #region *** Global Exception Handling *******************************************

        /// <summary>
        /// Different error types.
        /// </summary>
        private enum ErrorType
        {
            Unknown = 0,
            Application = 1,        // possible Application errors from 1 to 29999
            Domain = 30000          // possible Domain errors from 30000 to int.MaxValue
        }

        /// <summary>
        /// Handle the application exception.
        /// </summary>
        /// <param name="sender">The object with the exception.</param>
        /// <param name="e">The exception.</param>
        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            this.ShowError(ErrorType.Application, e.Exception);
        }

        /// <summary>
        /// Handle the application domain exception.
        /// </summary>
        /// <param name="sender">The object with the exception.</param>
        /// <param name="e">The exception.</param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            this.ShowError(ErrorType.Domain, (Exception)e.ExceptionObject);
        }

        /// <summary>
        /// Show the error message.
        /// </summary>
        /// <param name="type">The error type.</param>
        /// <param name="e">The exception.</param>
        private void ShowError(ErrorType type, Exception e)
        {
            // Fehlermeldung beim öffnen der Datei
            MessageBox.Show(
                "Error Source = " + type.ToString() + Environment.NewLine + Environment.NewLine
                + EnvironmentInfo.GetAllInformation(e),
                Application.ProductName);

            // Exception behavior
            Application.Exit();
        }

        #endregion *** Global Exception Handling *********************************************

        private readonly AxlBkApplication myApplication;
        
        public MainForm()
        {
            // This two events catch all unhandled exceptions.
            Application.ThreadException += this.Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += this.CurrentDomain_UnhandledException;

            this.InitializeComponent();

            // Create a new instance from the application class.
            this.myApplication = new AxlBkApplication();
          
            // Register an event if the buscoupler changes.
            this.axlControllerCtrl1.AfterSelectController += this.AxlControllerCtrl1_AfterSelectController; 

            // Add the buscoupler to the control.
            this.axlControllerCtrl1.AddObject(this.myApplication.Buscoupler);
        }

        private void AxlControllerCtrl1_AfterSelectController(object sender, object controller)
        {
            this.AddDevicesToControl(controller as IAxlController);         
        }

        /// <summary>
        /// Add the devices from the buscoupler to the control.
        /// </summary>
        /// <param name="controller">The selected controller.</param>
        private void AddDevicesToControl(IAxlController controller)
        {
            if (controller != null)
            {
                // Delete old devices
                this.axlDeviceCtrl1.ClearAllObjects();
                
                // Add the controller devices to the device control.
                foreach (AxlDevice i in controller.DeviceList)
                {
                    this.axlDeviceCtrl1.AddObject(i);
                }         
            }
        }
              
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.myApplication != null)
            {
                this.myApplication.Dispose();
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            // Update the controls for the Axioline buscoupler and the Axioline devices.
            this.axlControllerCtrl1.UpdateData();
            this.axlDeviceCtrl1.UpdateData();
        }

        private void axlControllerCtrl1_Load(object sender, EventArgs e)
        {

        }

        private void axlDeviceCtrl1_Load(object sender, EventArgs e)
        {

        }
    }
}
