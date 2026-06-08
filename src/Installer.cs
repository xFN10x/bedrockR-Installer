using bedrockRInstall.ui;
using Eto;
using Eto.Drawing;
using Eto.Forms;
using Eto.Wpf.Forms.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using Application = Eto.Forms.Application;
using Button = Eto.Forms.Button;
using Color = Eto.Drawing.Color;
using Orientation = Eto.Forms.Orientation;
using Padding = Eto.Drawing.Padding;
using Point = Eto.Drawing.Point;

namespace bedrockRInstall
{
    public class Installer : Eto.Forms.Form
    {

        public static Color BG = Color.FromRgb(0x202020);
        public static Color ControlBG = Color.FromRgb(0x343434);
        public static HttpClient client = new();
        public static Font mainFont = Font.FromStream(FileUtils.GetFile("font.otf"), 10);
        public static Font mainFontHeader = Font.FromStream(FileUtils.GetFile("font.otf"), 14);
        public HttpRequestMessage onlineReq = new(HttpMethod.Get, "http://clients3.google.com/generate_204");

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
                con.BackgroundColor = ControlBG;
                var cont = con.ControlObject as EtoButton;
                if (cont == null) return;
                cont.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xff, 0x48, 0x48, 0x48));
                var selectedBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xff, 0x4C, 0xff, 0x00));

                var selectedTrigger = new Trigger() { Property = EtoButton.IsFocusedProperty, Value = true };
                selectedTrigger.Setters.Add(new Setter(EtoButton.BorderBrushProperty, selectedBrush));

                var style = new System.Windows.Style(typeof(EtoButton));
                style.Setters.Add(new Setter(EtoButton.TemplateProperty, new ControlTemplate(typeof(EtoButton)) { } ));
                style.Setters.Add(new Setter(EtoButton.ForegroundProperty, new SolidColorBrush(System.Windows.Media.Colors.White)));
                style.Triggers.Add(selectedTrigger);
                cont.Style = style;
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
                Image = new Bitmap(FileUtils.GetFile("logo.png")).WithSize(500, 130)
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