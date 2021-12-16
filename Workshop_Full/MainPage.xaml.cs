using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Gpio;
using System.Reflection;
using System.Globalization;
using Windows.UI.Popups;
using System.Xml;
using System.Xml.Serialization;
using System.Data;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Workshop
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool status = true;
        //private const int Comp_PIN = 13;
        //private const int Aspi_PIN = 21;
        //private const int Scie_PIN_ON = 20;
        //private const int Ponceuse_PIN_ON = 16;
        //private const int Rabot_PIN_ON = 26;
        //private const int Drill_PIN_ON = 19;
        //private GpioPin Comp_gpio;
        //private GpioPin Aspi_gpio;
        //private GpioPin Scie_gpio_ON;
        //private GpioPin Ponceuse_gpio_ON;
        //private GpioPin Rabot_gpio_ON;
        //private GpioPin Drill_gpio_ON;

        private XmlFile MyXml;

        public MainPage()
        {
            this.InitializeComponent();
            //GPIO_Init();
            string version = Get_Version();
            Aspi_Version.Text = "Version : " + version;
            Comp_Version.Text = "Version : " + version;
            MyXml = new XmlFile("XmlSettings.xml");
            MyXml.Xml_Load();
            Tb_V_Motor_Toupie.PlaceholderText = MyXml.Get_Value("lvc", "Toupie", "V_Motor");
            Tb_Freq_Toupie.PlaceholderText = MyXml.Get_Value("lvc", "Toupie", "F_Motor");
            Tb_D_Motor_Toupie.PlaceholderText = MyXml.Get_Value("lvc", "Toupie", "D_Motor");
            Tb_D_Tree_Toupie.PlaceholderText = MyXml.Get_Value("lvc", "Toupie", "D_Tree");
            Tb_V_Motor_Rabot.PlaceholderText = MyXml.Get_Value("lvc", "Rabot", "V_Motor");
            Tb_Freq_Rabot.PlaceholderText = MyXml.Get_Value("lvc", "Rabot", "F_Motor");
            Tb_D_Motor_Rabot.PlaceholderText = MyXml.Get_Value("lvc", "Rabot", "D_Motor");
            Tb_D_Tree_Rabot.PlaceholderText = MyXml.Get_Value("lvc", "Rabot", "D_Tree");
            Get_Pression();
        }

        public string Get_Version()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return version;
        }

        //private void GPIO_Init()
        //{
        //    GpioController gpio = GpioController.GetDefault();
        //    Comp_gpio = gpio.OpenPin(Comp_PIN);
        //    Comp_gpio.SetDriveMode(GpioPinDriveMode.Output);
        //    Aspi_gpio = gpio.OpenPin(Aspi_PIN);
        //    Aspi_gpio.SetDriveMode(GpioPinDriveMode.Output);
        //    Scie_gpio_ON = gpio.OpenPin(Scie_PIN_ON);
        //    Scie_gpio_ON.SetDriveMode(GpioPinDriveMode.Output);
        //    Ponceuse_gpio_ON = gpio.OpenPin(Ponceuse_PIN_ON);
        //    Ponceuse_gpio_ON.SetDriveMode(GpioPinDriveMode.Output);
        //    Rabot_gpio_ON = gpio.OpenPin(Rabot_PIN_ON);
        //    Rabot_gpio_ON.SetDriveMode(GpioPinDriveMode.Output);
        //    Drill_gpio_ON = gpio.OpenPin(Drill_PIN_ON);
        //    Drill_gpio_ON.SetDriveMode(GpioPinDriveMode.Output);
        //}

        private void Aspi_SW_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
                if (toggleSwitch.IsOn)
                {
                    Aspi_Man_Status();

                    //Aspi_gpio.Write(GpioPinValue.Low);
                }
                else
                {
                    Aspi_Auto_Status();

                    //Aspi_gpio.Write(GpioPinValue.High);
                }
            }
        }

        private void Comp_SW_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
                if (toggleSwitch.IsOn)
                {
                    //Comp_gpio.Write(GpioPinValue.Low);
                }
                else
                {
                    //Comp_gpio.Write(GpioPinValue.High);
                }
            }
        }

        private void Scie_SW_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
                if (toggleSwitch.IsOn)
                {
                    //Scie_gpio_ON.Write(GpioPinValue.Low);
                    Drill_SW.IsOn = false;
                    Ponceuse_SW.IsOn = false;
                    Rabot_SW.IsOn = false;

                }
                else
                {
                    //Scie_gpio_ON.Write(GpioPinValue.High);
                }
            }
        }

        private void Rabot_SW_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
                if (toggleSwitch.IsOn)
                {
                    //Rabot_gpio_ON.Write(GpioPinValue.Low);
                    Drill_SW.IsOn = false;
                    Ponceuse_SW.IsOn = false;
                    Scie_SW.IsOn = false;
                }
                else
                {
                    //Rabot_gpio_ON.Write(GpioPinValue.High);
                }
            }
        }

        private void Ponceuse_SW_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
                if (toggleSwitch.IsOn)
                {
                    //Ponceuse_gpio_ON.Write(GpioPinValue.Low);
                    Drill_SW.IsOn = false;
                    Scie_SW.IsOn = false;
                    Rabot_SW.IsOn = false;
                }
                else
                {
                    //Ponceuse_gpio_ON.Write(GpioPinValue.High);
                }
            }
        }

        private void Drill_SW_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
                if (toggleSwitch.IsOn)
                {
                    //Drill_gpio_ON.Write(GpioPinValue.Low);
                    Scie_SW.IsOn = false;
                    Ponceuse_SW.IsOn = false;
                    Rabot_SW.IsOn = false;
                }
                else
                {
                    //Drill_gpio_ON.Write(GpioPinValue.High);
                }
            }
        }

        private void Aspi_Man_Status()
        {
            Drill_SW.IsEnabled = true;
            Drill_SW.IsOn = false;
            Ponceuse_SW.IsEnabled = true;
            Ponceuse_SW.IsOn = false;
            Rabot_SW.IsEnabled = true;
            Rabot_SW.IsOn = false;
            Scie_SW.IsEnabled = true;
            Scie_SW.IsOn = false;
        }

        private void Aspi_Auto_Status()
        {
            Drill_SW.IsEnabled = false;
            Drill_SW.IsOn = false;
            Ponceuse_SW.IsEnabled = false;
            Ponceuse_SW.IsOn = false;
            Rabot_SW.IsEnabled = false;
            Rabot_SW.IsOn = false;
            Scie_SW.IsEnabled = false;
            Scie_SW.IsOn = false;
        }

        private void Aspi_Auto_Man_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (status)
            {
                status = false;
                Aspi_Auto_Man_BTN.Content = "MANU";
                Aspi_Auto_Man_BTN.Background = new SolidColorBrush(Colors.Red);
                Aspi_Man_Status();
            }
            else
            {
                status = true;
                Aspi_Auto_Man_BTN.Content = "AUTO";
                Aspi_Auto_Man_BTN.Background = new SolidColorBrush(Colors.Green);
                Aspi_Auto_Status();
            }
        }

        private void Aspi_All_OFF_BTN_Click(object sender, RoutedEventArgs e)
        {
            Drill_SW.IsOn = false;
            Ponceuse_SW.IsOn = false;
            Scie_SW.IsOn = false;
            Rabot_SW.IsOn = false;
            Aspi_SW.IsOn = false;
        }

        private void Comp_Man_Status()
        {

        }

        private void Comp_Auto_Status()
        {

        }

        private void Comp_Auto_Man_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (status)
            {
                status = false;
                Comp_Auto_Man_BTN.Content = "MANU";
                Comp_Auto_Man_BTN.Background = new SolidColorBrush(Colors.Red);
                Comp_Man_Status();
            }
            else
            {
                status = true;
                Comp_Auto_Man_BTN.Content = "AUTO";
                Comp_Auto_Man_BTN.Background = new SolidColorBrush(Colors.Green);
                Comp_Auto_Status();
            }
        }

        private void Comp_All_OFF_BTN_Click(object sender, RoutedEventArgs e)
        {
            Comp_SW.IsOn = false;
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private double Set_Pression()
        {
            double pression = 0.5;
            return pression;
        }

        private void Get_Pression()
        {
            Pression.Text = "Pression : " + Set_Pression().ToString() + " bars";
        }

        private void Valid_Deport_1_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Bt_Valid_Settings_Toupie_Click(object sender, RoutedEventArgs e)
        {
            string V_Motor = Tb_V_Motor_Toupie.Text;
            string Freq = Tb_Freq_Toupie.Text;
            string D_Motor = Tb_D_Motor_Toupie.Text;
            string D_Tree = Tb_D_Tree_Toupie.Text;
            MyXml.Set_Value("lvc", "Toupie", "V_Motor", V_Motor);
            MyXml.Set_Value("lvc", "Toupie", "F_Motor", Freq);
            MyXml.Set_Value("lvc", "Toupie", "D_Motor", D_Motor);
            MyXml.Set_Value("lvc", "Toupie", "D_Tree", D_Tree);
            MyXml.Xml_Save();
        }

        private void Bt_Valid_Settings_Rabot_Click(object sender, RoutedEventArgs e)
        {
            string V_Motor = Tb_V_Motor_Rabot.Text;
            string Freq = Tb_Freq_Rabot.Text;
            string D_Motor = Tb_D_Motor_Rabot.Text;
            string D_Tree = Tb_D_Tree_Rabot.Text;
            MyXml.Set_Value("lvc", "Rabot", "V_Motor", V_Motor);
            MyXml.Set_Value("lvc", "Rabot", "F_Motor", Freq);
            MyXml.Set_Value("lvc", "Rabot", "D_Motor", D_Motor);
            MyXml.Set_Value("lvc", "Rabot", "D_Tree", D_Tree);
            MyXml.Xml_Save();
        }

        private void Validation_Toupie_Click(object sender, RoutedEventArgs e)
        {
            if (Tb_D_Outils.Text != "" & Tb_V_Outils.Text != "")
            {
                Spinning toupie = new Spinning(int.Parse(Tb_V_Outils.Text), int.Parse(Tb_D_Outils.Text));
                Tb_Fr.Text = toupie.Calcul_Fr().ToString("N2", CultureInfo.InvariantCulture) + " Hz";
            }
            else
            {
                MessageDialog warning = new MessageDialog("Vous avez oublié de remplir les champs diamètre de l'outil et vitesse de l'outil", "Erreur");
                warning.Commands.Add(new UICommand("Ok"));
                _ = warning.ShowAsync();
            }
        }
    }
}