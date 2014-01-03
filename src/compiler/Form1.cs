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
        public Form1()
        {
            InitializeComponent();
        }

        private void ScanFile(string filePath)
        {
            logListBox.Items.Clear();

            var text = File.ReadAllText(filePath);
            var outStream = new FileStream("result.1", FileMode.OpenOrCreate);

            var compiler = new Compiler();
            if (!compiler.Compile(text, outStream))
            {
                var errorsContainer = compiler.GetErrorsContainer();
                logListBox.Items.AddRange(errorsContainer.GetAll());
            }
            else
            {
                logListBox.Items.Add("Compiled successfully!");
            }
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
