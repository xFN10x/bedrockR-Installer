using Eto.Drawing;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace bedrockRInstall.ui
{
    public class VersionsForm : Eto.Forms.Dialog
    {

        private static int rowi = 0;
        public static TableRow CreateVersionRow(JsonVersionInfo info)
        {
            var panel = new PixelLayout() { BackgroundColor = rowi % 2 == 1 ? Colors.Gray : Installer.BG};

            panel.Add(new Label() { Text = info.Version, Font = Installer.mainFontHeader, TextColor = Colors.White, Style = "bedrockR"}, 15, 15);
            panel.Add(new Label() { Text = info.Description, Font = Installer.mainFont, TextColor = Colors.White, Style = "bedrockR"}, 15, 50);

            rowi++;
            return panel;
        }
        public VersionsForm()
        {
            Title = "bedrockR Versions";
            Resizable = false;
            ClientSize = new Eto.Drawing.Size(480, 240);
            BackgroundColor = Installer.BG;

            var scroll = new Scrollable() { ExpandContentHeight = false};
            var table = new TableLayout();
            scroll.Content = table;


            table.Rows.Add(CreateVersionRow(FileUtils.GetBundledVersionInfo()));
            table.Rows.Add(CreateVersionRow(new JsonVersionInfo() { Version = "test", NumericVersion = 0, Bundled = false}));
            table.Rows.Add(CreateVersionRow(new JsonVersionInfo() { Version = "test", NumericVersion = 0, Bundled = false}));
            table.Rows.Add(CreateVersionRow(new JsonVersionInfo() { Version = "test", NumericVersion = 0, Bundled = false}));
            table.Rows.Add(CreateVersionRow(new JsonVersionInfo() { Version = "test", NumericVersion = 0, Bundled = false}));
            table.Rows.Add(CreateVersionRow(new JsonVersionInfo() { Version = "test", NumericVersion = 0, Bundled = false}));

            Content = scroll;
            Installer.Center(this);
        }
    }
}
