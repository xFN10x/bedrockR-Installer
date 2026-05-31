using Eto.Forms;
using Eto.Drawing;
using Eto;
using Application = Eto.Forms.Application;
using bedrockRInstall.ui;

namespace bedrockRInstall
{
    public class Installer : Eto.Forms.Form
    {

        public static Color BG = Color.FromRgb(0x202020);
        public static HttpClient client = new();
        public HttpRequestMessage onlineReq = new(HttpMethod.Get, "http://clients3.google.com/generate_204");

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

            var onlineRes = client.Send(onlineReq);
            installOnlineButton.Enabled = onlineRes.IsSuccessStatusCode;
            versionsButton.Enabled = onlineRes.IsSuccessStatusCode;

            main.Items.Add(top);
            main.Items.Add(bottom);

            top.Items.Add(logoImg);

            bottom.Padding = new Padding(10, 40);
            bottom.Spacing = 10;
            bottom.Items.Add(installBundledButton);
            bottom.Items.Add(installOnlineButton);
            bottom.Items.Add(versionsButton);

            Content = main;

        }
    }
}