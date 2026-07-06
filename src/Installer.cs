using System.Text.Json;
using bedrockRInstall.ui;
using bedrockRInstall.util;
using Eto;
using Eto.Forms;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Application = Eto.Forms.Application;
using Button = Eto.Forms.Button;
using Color = Eto.Drawing.Color;
using Font = Eto.Drawing.Font;
using Orientation = Eto.Forms.Orientation;
using Padding = Eto.Drawing.Padding;
using Point = Eto.Drawing.Point;

namespace bedrockRInstall
{
    public class Installer : Form
    {
        public static Color Bg = Color.FromRgb(0x202020);
        public static Color ControlBg = Color.FromRgb(0x343434);
        public static Color Accent = Color.FromRgb(0x4CFF00);

        public static HttpClient Client = new();
        public static Font MainFont = Font.FromStream(FileUtils.GetFile("font.otf"), 10);
        public static Font MainFontHeader = Font.FromStream(FileUtils.GetFile("font.otf"), 20);
        public HttpRequestMessage OnlineReq = new(HttpMethod.Get, "http://clients3.google.com/generate_204");

        public static System.Windows.Media.Color EtoColorToWpf(Color c)
        {
            return System.Windows.Media.Color.FromArgb((byte)(c.A * 255), (byte)(c.R * 255), (byte)(c.G * 255),
                (byte)(c.B * 255));
        }

        public static void Center(Eto.Forms.Window window)
        {
            window.UpdateLayout();
            var screenSize = window.Screen.WorkingArea;
            float screenW = screenSize.Size.Width;
            float screenH = screenSize.Size.Height;
            float windowW = window.ClientSize.Width;
            float windowH = window.ClientSize.Height;

            float posX = (screenW / 2f) - (windowW / 2f);
            float posY = (screenH / 2f) - (windowH / 2f);

            window.Location = new Point((int)posX, (int)posY);
        }

        [STAThread]
        public static void Main(string[] args)
        {
            SetupStyle();
            new Application(Platform.Detect).Run(new Installer());
        }

        public static void SetupStyle()
        {
            Eto.Style.Add<TextControl>("bedrockR", con =>
            {
                con.TextColor = Eto.Drawing.Colors.White;
                con.Font = MainFont;
            });

            Eto.Style.Add<Button>("bedrockR", con =>
            {
                var cont = con.ControlObject as System.Windows.Controls.Button;
                if (cont == null) return;
                var bgBrush = new SolidColorBrush(EtoColorToWpf(ControlBg));
                var normalBorderBrush = new SolidColorBrush(EtoColorToWpf(Color.FromRgb(0x3E3E3E)));
                var selectedBorderBrush = new SolidColorBrush(EtoColorToWpf(Accent));

                var focausedTrigger = new Trigger
                    { Property = UIElement.IsFocusedProperty, Value = true };
                focausedTrigger.Setters.Add(new Setter(System.Windows.Controls.Control.BorderThicknessProperty,
                    new Thickness(1)));
                focausedTrigger.Setters.Add(new Setter(System.Windows.Controls.Control.BorderBrushProperty,
                    selectedBorderBrush));

                var mouseOverTrigger = new Trigger
                    { Property = UIElement.IsMouseOverProperty, Value = true };
                mouseOverTrigger.Setters.Add(new Setter(System.Windows.Controls.Control.BorderThicknessProperty,
                    new Thickness(2)));
                mouseOverTrigger.Setters.Add(new Setter(System.Windows.Controls.Control.BorderBrushProperty,
                    selectedBorderBrush));

                var style = new System.Windows.Style(typeof(System.Windows.Controls.Button));

                var borderFactory = new FrameworkElementFactory(typeof(Border));

                borderFactory.SetValue(
                    Border.BackgroundProperty,
                    new TemplateBindingExtension(Border.BackgroundProperty));

                borderFactory.SetValue(
                    Border.BorderBrushProperty,
                    new TemplateBindingExtension(Border.BorderBrushProperty));

                borderFactory.SetValue(
                    Border.BorderThicknessProperty,
                    new TemplateBindingExtension(Border.BorderThicknessProperty));

                borderFactory.SetValue(
                    Border.PaddingProperty,
                    new TemplateBindingExtension(Border.PaddingProperty));


                var contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));

                contentPresenter.SetValue(
                    Border.PaddingProperty,
                    new TemplateBindingExtension(Border.PaddingProperty));

                contentPresenter.SetValue(
                    FrameworkElement.HorizontalAlignmentProperty,
                    System.Windows.HorizontalAlignment.Center);

                contentPresenter.SetValue(
                    FrameworkElement.VerticalAlignmentProperty,
                    System.Windows.VerticalAlignment.Center);

                contentPresenter.SetValue(
                    FrameworkElement.MarginProperty,
                    new TemplateBindingExtension(FrameworkElement.MarginProperty));


                borderFactory.AppendChild(contentPresenter);

                var template = new ControlTemplate(typeof(System.Windows.Controls.Button))
                {
                    VisualTree = borderFactory
                };

                style.Setters.Add(new Setter(
                    System.Windows.Controls.Control.TemplateProperty,
                    template));

                style.Setters.Add(new Setter(System.Windows.Controls.Control.ForegroundProperty,
                    new SolidColorBrush(Colors.White)));
                style.Setters.Add(new Setter(System.Windows.Controls.Control.BackgroundProperty,
                    new SolidColorBrush(EtoColorToWpf(ControlBg))));
                style.Setters.Add(new Setter(System.Windows.Controls.Control.BorderBrushProperty, normalBorderBrush));
                style.Setters.Add(new Setter(FrameworkElement.MarginProperty, new Thickness(2)));
                style.Setters.Add(new Setter(Border.PaddingProperty, new Thickness(5)));

                style.Triggers.Add(focausedTrigger);
                style.Triggers.Add(mouseOverTrigger);

                cont.Style = style;

                //cont.MouseEnter += (s, e) =>
                //{
                //    Console.WriteLine("grsvbed");
                //    cont.BorderBrush = selectedBorderBrush;
                //    cont.BorderThickness = new Thickness(1);
                //};

                //cont.MouseLeave += (s, e) =>
                //{
                //    Console.WriteLine("grsvbed");
                //    cont.BorderBrush = normalBorderBrush;
                //};

                cont.PreviewMouseDown += (s, e) =>
                {
                    //Console.WriteLine("down");
                    cont.SetValue(System.Windows.Controls.Control.BackgroundProperty, normalBorderBrush);
                };

                cont.PreviewMouseUp += (s, e) =>
                {
                    cont.SetValue(System.Windows.Controls.Control.BackgroundProperty, bgBrush);
                };
            });
        }

        public Installer()
        {
            Title = "bedrockR Installer";
            Resizable = false;
            ClientSize = new Eto.Drawing.Size(500, 200);
            BackgroundColor = Bg;

            var logoImg = new ImageView
            {
                Image = new Eto.Drawing.Bitmap(FileUtils.GetFile("logo.png")).WithSize(500, 130)
            };

            var main = new StackLayout
            {
                Orientation = Orientation.Vertical, HorizontalContentAlignment = Eto.Forms.HorizontalAlignment.Center
            };
            var top = new StackLayout { Orientation = Orientation.Horizontal };
            var bottom = new StackLayout { Orientation = Orientation.Horizontal };

            var bundledInfo = FileUtils.GetBundledVersionInfo();

            var installBundledButton = new Button { Style = "bedrockR" };
            if (bundledInfo != null)
                installBundledButton.Text = "Install Bundled (" + bundledInfo.Version + ")";
            else
                installBundledButton.Enabled = false;

            var installOnlineButton = new Button((o, e) => { new VersionsForm().ShowModal(this); })
                { Text = "Install Online", Style = "bedrockR", Enabled = false };
            //var versionsButton = new Button((o,e) => { new VersionsForm().ShowModal(this); }) { Text = "View Versions", Style = "bedrockR", Enabled = false };

            bool online = false;
            try
            {
                var onlineRes = Client.Send(OnlineReq);
                online = onlineRes.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                // ignored
            }

            installOnlineButton.Enabled = online;

            main.Items.Add(top);
            main.Items.Add(bottom);

            top.Items.Add(logoImg);

            bottom.Padding = new Padding(10, 40);
            bottom.Spacing = 10;
            bottom.Items.Add(installBundledButton);
            bottom.Items.Add(installOnlineButton);

            Content = main;
            Center(this);
        }
    }
}