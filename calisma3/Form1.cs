using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using Microsoft.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Tulpep.NotificationWindow;

namespace calisma3
{
    public partial class Form1 : Form
    {

        public string DeviceName = "USB\\VID_0930&PID_6545\\000FEA0E7429EB51F3F50013";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                PopupNotifier popup = new PopupNotifier();
                popup.TitleText = "USB Portları devre dışı";
                popup.ContentText = "USB Portları devre dışı";
                popup.Popup();// show  

                string path = "SYSTEM\\CurrentControlSet\\services\\USBSTOR\\";
                RegistryKey RK = Registry.LocalMachine.OpenSubKey(path, true);

                RK.SetValue("Start", "4", RegistryValueKind.DWord);
               // status.Text = "Status : All Usb Ports Locked !";
            }
            catch (Exception ex)
            {

                MessageBox.Show(" Should be Run as Adminstrator", "Stop");

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {

            PopupNotifier popup = new PopupNotifier();
            popup.TitleText = "USB Portları aktif";
            popup.ContentText = "USB Portları aktif";
            popup.Popup();// show  
            //enable USB storage...
            Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\USBSTOR", "Start", 3, Microsoft.Win32.RegistryValueKind.DWord);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ManagementScope myScope = new ManagementScope("root\\CIMV2");
            SelectQuery q = new SelectQuery(@"select * from Win32_USBHub");
            ManagementObjectSearcher s = new ManagementObjectSearcher(myScope, q);

            foreach (ManagementObject device in s.Get())
            {
              //  checkedListBox1.Items.Add(device.ToString());
                checkedListBox1.Items.Add(device.GetPropertyValue("deviceID"));
            }
        }
    }
}
