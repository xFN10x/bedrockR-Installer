using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace bedrockRInstall.ui
{
    public class VersionsForm : Eto.Forms.Dialog
    {

        public static TableRow createVersionRow(JsonVersionInfo info)
        {
            // [version name] | [desc] | [bundled checkmark]
            var nameCell = new TableCell(new Label() { Text = info.Version });
            var descCell = new TableCell(new Label() { Text = info.Version });
            var bundledCell = new TableCell(new Label() { Text = info.Version });

            return new TableRow(nameCell, descCell, bundledCell);
        }
        public VersionsForm()
        {
            Title = "bedrockR Versions";
            Resizable = false;
            Size = new Eto.Drawing.Size(480, 240);

            var table = new TableLayout();

            table.Rows.Add(createVersionRow(new JsonVersionInfo() { Version = "test", NumericVersion = 0}));

            Content = table;
        }
    }
}
