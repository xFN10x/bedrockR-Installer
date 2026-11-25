using bedrockRInstall.util;
using Eto.Drawing;
using Eto.Forms;
using Gdk;
using Color = Eto.Drawing.Color;

namespace bedrockRInstall.ui;

public class InstallingForm : Dialog
{
    public static void Install(JsonVersionInfo? verInfo)
    {
        if (verInfo == null) return;
        if (verInfo.InstallerFormat > FileUtils.installerVer)
        {
            MessageBox.Show("This bedrockR version is incompatible with this version of the installer.",
                MessageBoxButtons.OK, MessageBoxType.Error, MessageBoxDefaultButton.OK);
        }

        new InstallingForm(verInfo, Directory.CreateTempSubdirectory("brinstall")).ShowModal();
    }

    public readonly JsonVersionInfo Installing;
    protected volatile Label stepText = new()
    {
        Text = "Starting install...",
        Style = "bedrockR",
    };

    protected volatile ProgressBar progress = new()
    {
        Indeterminate = true,
        Width = 400,
        Style = "bedrockR",
        BackgroundColor = Color.FromRgb(0x4a4a4a)
    };

    public InstallingForm(JsonVersionInfo installing, DirectoryInfo tempDir)
    {
        Installing = installing;
        Title = "Installing " + Installing.Version + "...";
        Resizable = false;
        ClientSize = new Eto.Drawing.Size(400, 200);
        BackgroundColor = Installer.Bg;

        StackLayout lay = new()
        {
            Orientation = Orientation.Vertical,
            HorizontalContentAlignment = HorizontalAlignment.Center,
            Spacing = 10
        };

        var logoImg = new ImageView
        {
            Image = new Eto.Drawing.Bitmap(FileUtils.GetFile("BrandingFullWShadow.png")).WithSize(500, 130),
            Width = 300,
            Style = "bedrockR",
        };

        

        var log = new TextArea()
        {
            Enabled = false,
            Width = 390,
            Text = "test\ntest",
            TextColor = Colors.White,
            BackgroundColor = Color.FromRgb(0x4a4a4a)
        };

        lay.Items.Add(logoImg);
        lay.Items.Add(stepText);
        lay.Items.Add(progress);
        lay.Items.Add(log);

        Content = lay;

        Thread worker = new Thread(Install);
        
        Installer.Center(this);
    }

    public void Install()
    {
        
    }
}