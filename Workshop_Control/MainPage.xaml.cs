using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.Devices.Gpio;
using Windows.UI;
using System.Reflection;


namespace Workshop_Control
{
    public sealed partial class MainPage : Page
    {
        private bool status = true;
        private const int Comp_PIN = 13;
        private const int Aspi_PIN = 21;
        private const int Scie_PIN_ON = 20;
        private const int Ponceuse_PIN_ON = 16;
        private const int Rabot_PIN_ON = 26;
        private const int Drill_PIN_ON = 19;
        private GpioPin Comp_gpio;
        private GpioPin Aspi_gpio;
        private GpioPin Scie_gpio_ON;
        private GpioPin Ponceuse_gpio_ON;
        private GpioPin Rabot_gpio_ON;
        private GpioPin Drill_gpio_ON;

        public MainPage()
        {
            InitializeComponent();
            GPIO_Init();
            Title.Text = "Workshop Control";
            string version = Get_Version();
            Version.Text = "Version : " + version;
        }

        public string Get_Version()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return version;
        }

        private void GPIO_Init()
        {
            GpioController gpio = GpioController.GetDefault();
            Comp_gpio = gpio.OpenPin(Comp_PIN);
            Comp_gpio.SetDriveMode(GpioPinDriveMode.Output);
            Aspi_gpio = gpio.OpenPin(Aspi_PIN);
            Aspi_gpio.SetDriveMode(GpioPinDriveMode.Output);
            Scie_gpio_ON = gpio.OpenPin(Scie_PIN_ON);
            Scie_gpio_ON.SetDriveMode(GpioPinDriveMode.Output);
            Ponceuse_gpio_ON = gpio.OpenPin(Ponceuse_PIN_ON);
            Ponceuse_gpio_ON.SetDriveMode(GpioPinDriveMode.Output);
            Rabot_gpio_ON = gpio.OpenPin(Rabot_PIN_ON);
            Rabot_gpio_ON.SetDriveMode(GpioPinDriveMode.Output);
            Drill_gpio_ON = gpio.OpenPin(Drill_PIN_ON);
            Drill_gpio_ON.SetDriveMode(GpioPinDriveMode.Output);
        }

        private void Aspi_SW_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
                if (toggleSwitch.IsOn)
                {
                    Man_Status();

                    Aspi_gpio.Write(GpioPinValue.Low);
                }
                else
                {
                    Auto_Status();

                    Aspi_gpio.Write(GpioPinValue.High);
                }
            }
        }

        private void Comp_SW_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
                if (toggleSwitch.IsOn)
                {
                    Comp_gpio.Write(GpioPinValue.Low);
                }
                else
                {
                    Comp_gpio.Write(GpioPinValue.High);
                }
            }
        }

        private void Scie_SW_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
                if (toggleSwitch.IsOn)
                {
                    Scie_gpio_ON.Write(GpioPinValue.Low);
                    Drill_SW.IsOn = false;
                    Ponceuse_SW.IsOn = false;
                    Rabot_SW.IsOn = false;

                }
                else
                {
                    Scie_gpio_ON.Write(GpioPinValue.High);
                }
            }
        }

        private void Rabot_SW_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
                if (toggleSwitch.IsOn)
                {
                    Rabot_gpio_ON.Write(GpioPinValue.Low);
                    Drill_SW.IsOn = false;
                    Ponceuse_SW.IsOn = false;
                    Scie_SW.IsOn = false;
                }
                else
                {
                    Rabot_gpio_ON.Write(GpioPinValue.High);
                }
            }
        }

        private void Ponceuse_SW_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
                if (toggleSwitch.IsOn)
                {
                    Ponceuse_gpio_ON.Write(GpioPinValue.Low);
                    Drill_SW.IsOn = false;
                    Scie_SW.IsOn = false;
                    Rabot_SW.IsOn = false;
                }
                else
                {
                    Ponceuse_gpio_ON.Write(GpioPinValue.High);
                }
            }
        }

        private void Drill_SW_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
                if (toggleSwitch.IsOn)
                {
                    Drill_gpio_ON.Write(GpioPinValue.Low);
                    Scie_SW.IsOn = false;
                    Ponceuse_SW.IsOn = false;
                    Rabot_SW.IsOn = false;
                }
                else
                {
                    Drill_gpio_ON.Write(GpioPinValue.High);
                }
            }
        }

        private void Man_Status()
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

        private void Auto_Status()
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

        private void Auto_Man_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (status)
            {
                status = false;
                Auto_Man_BTN.Content = "MANU";
                Auto_Man_BTN.Background = new SolidColorBrush(Colors.Red);
                Man_Status();
            }
            else
            {
                status = true;
                Auto_Man_BTN.Content = "AUTO";
                Auto_Man_BTN.Background = new SolidColorBrush(Colors.Green);
                Auto_Status();
            }
        }

        private void All_OFF_BTN_Click(object sender, RoutedEventArgs e)
        {
            Drill_SW.IsOn = false;
            Ponceuse_SW.IsOn = false;
            Scie_SW.IsOn = false;
            Rabot_SW.IsOn = false;
            Comp_SW.IsOn = false;
            Aspi_SW.IsOn = false;
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
