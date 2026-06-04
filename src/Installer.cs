using bedrockRInstall.ui;
using Eto;
using Eto.Drawing;
using Eto.Forms;
using System.Windows.Documents;
using Application = Eto.Forms.Application;

namespace bedrockRInstall
{
    public class Installer : Eto.Forms.Form
    {

        public static Color BG = Color.FromRgb(0x202020);
        public static HttpClient client = new();
        public static Font mainFont = Font.FromStream(FileUtils.GetFile("font.otf"), 12);
        public static Font mainFontHeader = Font.FromStream(FileUtils.GetFile("font.otf"), 16);
        public HttpRequestMessage onlineReq = new(HttpMethod.Get, "http://clients3.google.com/generate_204");

        public static void Center(Window window)
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
            new Application(Platform.Detect).Run(new Installer());
        }

        public static void SetupStyle()
        {

            Eto.Style.Add<Eto.Forms.Button>("bedrockR", con =>
            {
                var cont = con.ControlObject as System.Windows.Forms.Button;
                cont.BackColor = System.Drawing.Color.FromArgb((int)(BG.A * 255), (int)(BG.R * 255), (int)(BG.G * 255), (int)(BG.B * 255));
             });

            Eto.Style.Add<TextControl>("bedrockR", con =>
            {
                con.TextColor = Colors.White;
                con.Font = mainFont;
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

            var installOnlineButton = new Button() { Text = "Install Online", Style = "bedrockR", Enabled = false };
            var versionsButton = new Button((o,e) => { new VersionsForm().ShowModal(this); }) { Text = "View Versions", Style = "bedrockR", Enabled = false };

            bool online = false;
            try
            {
                var onlineRes = client.Send(onlineReq);
                online = onlineRes.IsSuccessStatusCode;
            } catch (Exception) { }
            installOnlineButton.Enabled = online;
            versionsButton.Enabled = online;

            main.Items.Add(top);
            main.Items.Add(bottom);

            top.Items.Add(logoImg);

            bottom.Padding = new Padding(10, 40);
            bottom.Spacing = 10;
            bottom.Items.Add(installBundledButton);
            bottom.Items.Add(installOnlineButton);
            bottom.Items.Add(versionsButton);

            Content = main;
            Center(this);
        }
    }
}