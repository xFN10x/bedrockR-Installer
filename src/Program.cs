using System;
using Eto.Forms;
using Eto.Drawing;
using Eto;

public class Installer : Form
{
    [STAThread]
    public static void Main(string[] args)
    {
        new Application(Platform.Detect).Run(new Installer());
    }

    public Installer()
    {
        Title = "Eto Installer";
        ClientSize = new Size(400, 300);
    }
}
