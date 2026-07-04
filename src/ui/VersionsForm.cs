using Eto.Drawing;
using Eto.Forms;
using bedrockRInstall.util;

namespace bedrockRInstall.ui
{
    public class VersionsForm : Dialog
    {
        private static int _rowi = 0;

        public static TableRow CreateVersionRow(JsonVersionInfo info)
        {
            var panel = new PixelLayout { BackgroundColor = _rowi % 2 == 1 ? Colors.DarkGray : Installer.Bg };

            panel.Add(
                new Label
                {
                    Text = info.Version, Font = Installer.MainFontHeader, TextColor = Colors.White, Style = "bedrockR"
                }, 15, 15);
            panel.Add(
                new Label
                {
                    Text = info.Description, Font = Installer.MainFont, TextColor = Colors.White, Style = "bedrockR"
                }, 15, 50);
            panel.Add(
                new Label
                {
                    TextAlignment = TextAlignment.Right, Text = info.Bundled ? "✓ Bundled" : "X Not Bundled",
                    Font = Installer.MainFont, TextColor = Colors.White, Style = "bedrockR"
                }, 360, 15);
            StackLayout buttonsLay = new()
            {
                Orientation = Orientation.Horizontal,
                VerticalContentAlignment = VerticalAlignment.Bottom,
                Padding = new Padding(10,3),
                Spacing = 10
            };
            buttonsLay.Items.Add(new Button
                { Text = "Install", Font = Installer.MainFont, Style = "bedrockR" });
            buttonsLay.Items.Add(new Button
                { Text = "View download page", Font = Installer.MainFont, Style = "bedrockR" });
            if (info.Bundled)
                buttonsLay.Items.Add(new Button
                    { Text = "Run", Font = Installer.MainFont, Style = "bedrockR" });

            panel.Add(buttonsLay, 0, 80);

            _rowi++;
            return panel;
        }

        public VersionsForm()
        {
            Title = "bedrockR Versions";
            Resizable = false;
            ClientSize = new Size(480, 240);
            BackgroundColor = Installer.Bg;

            var scroll = new Scrollable { ExpandContentHeight = false };
            var table = new TableLayout();
            scroll.Content = table;


            var bundled = FileUtils.GetBundledVersionInfo();
            if (bundled != null)
                table.Rows.Add(CreateVersionRow(bundled));
            var vers = FileUtils.GetVersions();
            if (vers != null)
                foreach (JsonVersionInfo jsonVersionInfo in vers)
                {
                    if (!jsonVersionInfo.Bundled)
                        table.Rows.Add(CreateVersionRow(jsonVersionInfo));
                }

            Content = scroll;
            Installer.Center(this);
        }
    }
}