using bedrockRInstall.ui;
using Eto;
using Eto.Drawing;
using Eto.Forms;
using Eto.Wpf.Forms.Controls;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media;
using Application = Eto.Forms.Application;
using Binding = System.Windows.Data.Binding;
using Button = Eto.Forms.Button;
using Color = Eto.Drawing.Color;
using Font = Eto.Drawing.Font;
using HorizontalAlignment = System.Windows.Forms.HorizontalAlignment;
using Orientation = Eto.Forms.Orientation;
using Padding = Eto.Drawing.Padding;
using Point = Eto.Drawing.Point;
using VerticalAlignment = System.Windows.VerticalAlignment;

namespace bedrockRInstall
{
    public class Installer : Eto.Forms.Form
    {

        public static Color BG = Color.FromRgb(0x202020);
        public static Color ControlBG = Color.FromRgb(0x343434);
        public static Color Accent = Color.FromRgb(0x4CFF00);

        public static HttpClient client = new();
        public static Font mainFont = Font.FromStream(FileUtils.GetFile("font.otf"), 10);
        public static Font mainFontHeader = Font.FromStream(FileUtils.GetFile("font.otf"), 14);
        public HttpRequestMessage onlineReq = new(HttpMethod.Get, "http://clients3.google.com/generate_204");

        public static System.Windows.Media.Color EtoColorToWpf(Color c)
        {
            return System.Windows.Media.Color.FromArgb((byte)(c.A * 255), (byte)(c.R * 255), (byte)(c.G * 255), (byte)(c.B * 255));
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
                con.Font = mainFont;
            });

            Eto.Style.Add<Eto.Forms.Button>("bedrockR", con =>
            {
                var cont = con.ControlObject as System.Windows.Controls.Button;
                if (cont == null) return;
                var bgBrush = new SolidColorBrush(EtoColorToWpf(ControlBG));
                var normalBorderBrush = new SolidColorBrush(EtoColorToWpf(Color.FromRgb(0x3E3E3E)));
                var selectedBorderBrush = new SolidColorBrush(EtoColorToWpf(Accent));

                var focausedTrigger = new Trigger() { Property = System.Windows.Controls.Button.IsFocusedProperty, Value = true };
                focausedTrigger.Setters.Add(new Setter(System.Windows.Controls.Button.BorderThicknessProperty, new Thickness(1)));
                focausedTrigger.Setters.Add(new Setter(System.Windows.Controls.Button.BorderBrushProperty, selectedBorderBrush));

                var mouseOverTrigger = new Trigger() { Property = System.Windows.Controls.Button.IsMouseOverProperty, Value = true };
                mouseOverTrigger.Setters.Add(new Setter(System.Windows.Controls.Button.BorderThicknessProperty, new Thickness(2)));
                mouseOverTrigger.Setters.Add(new Setter(System.Windows.Controls.Button.BorderBrushProperty, selectedBorderBrush));

                var style = new System.Windows.Style(typeof(System.Windows.Controls.Button));

                var borderFactory = new FrameworkElementFactory(typeof(System.Windows.Controls.Border));

                borderFactory.SetValue(
                    System.Windows.Controls.Border.BackgroundProperty,
                    new TemplateBindingExtension(System.Windows.Controls.Border.BackgroundProperty));

                borderFactory.SetValue(
                    System.Windows.Controls.Border.BorderBrushProperty,
                    new TemplateBindingExtension(System.Windows.Controls.Border.BorderBrushProperty));

                borderFactory.SetValue(
                    System.Windows.Controls.Border.BorderThicknessProperty,
                    new TemplateBindingExtension(System.Windows.Controls.Border.BorderThicknessProperty));


                var contentPresenter = new FrameworkElementFactory(typeof(System.Windows.Controls.ContentPresenter));

                contentPresenter.SetValue(
                    System.Windows.Controls.Border.PaddingProperty,
                    new TemplateBindingExtension(System.Windows.Controls.Border.PaddingProperty));

                contentPresenter.SetValue(
                    System.Windows.Controls.Border.HorizontalAlignmentProperty,
                    System.Windows.HorizontalAlignment.Center);

                contentPresenter.SetValue(
                System.Windows.Controls.Border.VerticalAlignmentProperty,
                System.Windows.VerticalAlignment.Center);

                contentPresenter.SetValue(
                System.Windows.Controls.Border.MarginProperty,
                new TemplateBindingExtension(System.Windows.Controls.Border.MarginProperty));


                borderFactory.AppendChild(contentPresenter);

                var template = new ControlTemplate(typeof(System.Windows.Controls.Button));
                template.VisualTree = borderFactory;

                style.Setters.Add(new Setter(
                    System.Windows.Controls.Button.TemplateProperty,
                    template));

                style.Setters.Add(new Setter(System.Windows.Controls.Button.ForegroundProperty, new SolidColorBrush(System.Windows.Media.Colors.White)));
                style.Setters.Add(new Setter(System.Windows.Controls.Button.BackgroundProperty, new SolidColorBrush(EtoColorToWpf(ControlBG))));
                style.Setters.Add(new Setter(System.Windows.Controls.Button.BorderBrushProperty, normalBorderBrush));
                style.Setters.Add(new Setter(System.Windows.Controls.Button.MarginProperty, new Thickness(2)));

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
                    cont.SetValue(System.Windows.Controls.Button.BackgroundProperty, normalBorderBrush);
                };

                cont.PreviewMouseUp += (s, e) =>
                {
                    cont.SetValue(System.Windows.Controls.Button.BackgroundProperty, bgBrush);
                };
            });

        }
        public Installer()
        {
            Title = "bedrockR Installer";
            Resizable = false;
            ClientSize = new Eto.Drawing.Size(500, 200);
            BackgroundColor = BG;
            
            var logoImg = new ImageView()
            {
                Image = new Eto.Drawing.Bitmap(FileUtils.GetFile("logo.png")).WithSize(500, 130)
            };

            var main = new StackLayout() { Orientation = Orientation.Vertical, HorizontalContentAlignment = Eto.Forms.HorizontalAlignment.Center};
            var top = new StackLayout() { Orientation = Orientation.Horizontal };
            var bottom = new StackLayout() { Orientation = Orientation.Horizontal };

            var bundledInfo = FileUtils.GetBundledVersionInfo();

            var installBundledButton = new Button() {Style = "bedrockR" };
            if (bundledInfo != null)
                installBundledButton.Text = "Install Bundled (" + bundledInfo.Version + ")";
            else
                installBundledButton.Enabled = false;

            var installOnlineButton = new Button((o, e) => { new VersionsForm().ShowModal(this); }) { Text = "Install Online", Style = "bedrockR", Enabled = false };
            //var versionsButton = new Button((o,e) => { new VersionsForm().ShowModal(this); }) { Text = "View Versions", Style = "bedrockR", Enabled = false };

            bool online = false;
            try
            {
                var onlineRes = client.Send(onlineReq);
                online = onlineRes.IsSuccessStatusCode;
            } catch (Exception) { }
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