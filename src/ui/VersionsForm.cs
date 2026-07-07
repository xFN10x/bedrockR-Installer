using System.Reflection;
using System.Windows.Forms;
using Eto.Drawing;
using Eto.Forms;
using bedrockRInstall.util;
using Eto;
using GLib;
using Button = Eto.Forms.Button;
using FileUtils = bedrockRInstall.util.FileUtils;
using HorizontalAlignment = Eto.Forms.HorizontalAlignment;
using Label = Eto.Forms.Label;
using Orientation = Eto.Forms.Orientation;
using Padding = Eto.Drawing.Padding;
using Process = System.Diagnostics.Process;

namespace bedrockRInstall.ui
{
    public class VersionsForm : Dialog
    {
        private static int _rowi = 0;

        private static Size WinSize = new(480, 240);

        public static TableRow CreateVersionRow(JsonVersionInfo info)
        {
            var mainPanel = new StackLayout()
            {
                BackgroundColor = _rowi % 2 == 1 ? Colors.DarkGray : Installer.Bg,
                Spacing = 10,
                Width = WinSize.Width - 20,
                Padding = new Padding(10),
                Orientation = Orientation.Vertical,
                HorizontalContentAlignment = HorizontalAlignment.Stretch
            };
            var topPanel = new TableLayout();
            var buttonsPanel = new StackLayout()
            {
                Orientation = Orientation.Horizontal,
                Spacing = 10
            };
            mainPanel.Items.Add(topPanel);
            mainPanel.Items.Add(
                new Label
                {
                    Text = info.Description, Font = Installer.MainFont, TextColor = Colors.White, Style = "bedrockR"
                });
            mainPanel.Items.Add(buttonsPanel);

            topPanel.Rows.Add(
                new Label
                {
                    Text = info.Version,
                    Font = Installer.MainFontHeader,
                    TextColor = Colors.White,
                    Style = "bedrockR"
                });

            topPanel.Rows.Add(
                new Label
                {
                    TextAlignment = TextAlignment.Right,

                    Text = info.Bundled ? "✓ Bundled" : "✗ Not Bundled",
                    Font = Installer.MainFont,
                    TextColor = Colors.White,
                    Style = "bedrockR"
                });

            buttonsPanel.Items.Add(new Button
                { Text = "Install", Font = Installer.MainFont, Style = "bedrockR" });
            buttonsPanel.Items.Add(new Button((sender, args) =>
                {
                    if (EtoEnvironment.Platform.IsWindows)
                        Process.Start("explorer",
                            "\"https://bedrockr.xplate.dev/download?release=" + info.TagName + "\"");
                })
                { Text = "View download page", Font = Installer.MainFont, Style = "bedrockR" });
            if (info.Bundled)
                buttonsPanel.Items.Add(new Button
                    { Text = "Run", Font = Installer.MainFont, Style = "bedrockR" });


            _rowi++;
            return mainPanel;
        }

        public VersionsForm()
        {
            Title = "bedrockR Versions";
            Resizable = false;
            ClientSize = WinSize;
            BackgroundColor = Installer.Bg;

            var scroll = new Scrollable
            {
                ExpandContentHeight = false,
                ExpandContentWidth = false
            };
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