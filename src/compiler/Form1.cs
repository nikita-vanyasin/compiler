using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace compiler
{
    public partial class Form1 : Form
    {
        Scanner scanner;

        public Form1()
        {
            InitializeComponent();
            scanner = new Scanner();
        }

        private void ScanFile(string filePath)
        {
            this.Text = "Dwarves - " + filePath;
            logListBox.Items.Clear();
            scanner.SetText(File.ReadAllText(filePath));

            Token t;
            do
            {
                t = scanner.GetNextToken();
                txtResultsBox.Text += t + "\n";
            } while (Token.IsCorrectToken(t));
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = getTestFilesFolderPath();
            ofd.Multiselect = false;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            ofd.AddExtension = true;

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                ScanFile(ofd.FileName);
            }
        }

        private string getTestFilesFolderPath()
        {
            return getCurrFolder() + @"\test";
        }

        private string getCurrFolder()
        {
            return Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        }
    }
}
