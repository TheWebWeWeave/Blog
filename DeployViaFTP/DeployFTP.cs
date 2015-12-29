using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using Tool = DeployViaFTP.PageObects.ToolbarClasses;
using DeployViaFTP.PageObects.PowerShell_ISEClasses;


namespace DeployViaFTP
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class DeployFTP
    {
        public DeployFTP()
        {
        }

        [TestMethod]
        public void DeployUsingPowerShellAndFTP()
        {
            Tool.Toolbar tool = new Tool.Toolbar();
            PowerShell_ISE ps = new PowerShell_ISE();

            tool.StartPowerShellISE_as_Admin();
            ps.OpenFTP_Blog_To_Azure_script();
            ps.RunPowerShellScript();

            // If this was testing code this would be a very bad things.
            // but we are not we just need this to leave the PowerShell window alone
            // for about 5 minutes...then it can cleanup...
            System.Threading.Thread.Sleep(300000);
            ps.ClosePowerShell_ISE();
        }



        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;
    }
}
