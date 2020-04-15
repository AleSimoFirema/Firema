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
    using System.Collections.Generic;
    using PhoenixContact.HFI.Axioline;
    using PhoenixContact.HFI.Axioline.AxlDevices;

    /// <summary>
    /// Application class for the AXL F BK ETH.
    /// </summary>
    public class AxlBkApplication : IDisposable
    {
        private UInt32 pdCounter;
        private AxlControllerF_BK axlFBk;

        // TODO create the Axioline device objects
        // The slot number describes the position of the participant in the bus configuration.
        // The slot numbers must be consecutive and begin with 1.
        private AxlDeviceDigitalInput di32 = new AxlDeviceDigitalInput(DigitalInputType.DI32, 1, "Di32");

        private AxlDeviceDigitalOutput do32 = new AxlDeviceDigitalOutput(DigitalOutputType.DO32, 2, false, "Do32");

        /// <summary>
        /// Initializes a new instance of the <see cref="AxlBkApplication"/> class. 
        /// Default constructor for the class.
        /// </summary>
        public AxlBkApplication()
        {
            this.ExceptionList = new Queue<Exception>();

            this.axlFBk = new AxlControllerF_BK("AXL F BK ETH");

            // TODO Set the ip address
            this.axlFBk.Connection = "172.16.159.11";

            this.axlFBk.OnConnect += this.Controller_OnConnect;
            this.axlFBk.OnUpdateProcessData += this.AxlFBk_OnUpdateProcessData;
            this.axlFBk.OnException += this.AxlFBk_OnException;

            // TODO add the Axioline device objects to the buscoupler, the order doesn't matter
            this.axlFBk.AddAxlDevice(this.di32);
            this.axlFBk.AddAxlDevice(this.do32);
            this.axlFBk.CreateAxlDeviceConfig();
        }

        /// <summary>
        /// Gets the error messages from the buscoupler.
        /// </summary>
        public Queue<Exception> ExceptionList { get; private set; }

        /// <summary>
        /// Gets the Buscoupler object.
        /// </summary>
        public IAxlController Buscoupler
        {
            get
            {
                return this.axlFBk;
            }
        }

        #region *** Controller Events ***************************************************

        private void Controller_OnConnect(object sender)
        {
            // TODO enter your code to be executed when connecting the controller
        }

        private void AxlFBk_OnUpdateProcessData(object sender)
        {
            // TODO insert your process data handling (application) here.
            // This event is called once for each process data update cycle.
            // Please don't access to slow instances, for example: Windows Forms, Databases, ...

            // Write a test couter to the outputs from the DO 32 device.
            this.pdCounter++;
            this.do32.OutputValue = this.pdCounter;

            // For bit access on the variable activate the "bitAccess" property in the device class konstructor.
            // do32[0] = !di32[0];
        }

        private void AxlFBk_OnException(Exception exceptionData)
        {
            // Save each error message.
            this.ExceptionList.Enqueue(exceptionData);

            // TODO your error handling can be inserted here.
        }

        #endregion *** Controller Events ***************************************************

        #region *** IDisposable Member **************************************************

        private bool disposed;

        /// <summary>
        /// Implement IDisposable.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                    if (this.axlFBk != null)
                    {
                        this.axlFBk.Disable();
                        this.axlFBk.Dispose();
                    }
                }
                
                // Free your own state (unmanaged objects).
                // Set large fields to null.
                this.disposed = true;
            }
        }

        // Use C# destructor syntax for finalization code.
        ~AxlBkApplication()
        {
            // Simply call Dispose(false).
            this.Dispose(false);
        }

        #endregion *** IDisposable Member **************************************************
    }
}