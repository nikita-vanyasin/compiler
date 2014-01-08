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
		//номер элемента в логе -> позиция в коде
		private Dictionary<int, int> m_errorPositions = new Dictionary<int, int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void BuildSource()
        {
            logListBox.Items.Clear();
			m_errorPositions.Clear();

            var text = SourceBox.Text + "\n";
            var outStream = new FileStream("result.s", FileMode.OpenOrCreate);

            var compiler = new Compiler();
            if (!compiler.Compile(text, outStream))
            {
                var errorsContainer = compiler.GetErrorsContainer();
				foreach (var ev in errorsContainer)
				{
					int id = logListBox.Items.Add(TextUtils.WriteCompilerError(SourceBox.Text, ev));
					m_errorPositions.Add(id, ev.Position);
				}
            }
            else
            {
                logListBox.Items.Add("Compiled successfully!");
            }            
            outStream.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				SourceBox.Clear();
				SourceBox.Text = File.ReadAllText(openFileDlg.FileName);
				this.Text = string.Format("Compiler - {0}", openFileDlg.FileName);
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

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			saveFileDialog.FileName = openFileDlg.FileName;
			saveFileDialog.InitialDirectory = openFileDlg.InitialDirectory;

			if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				File.WriteAllText(saveFileDialog.FileName, SourceBox.Text);
				this.Text = string.Format("Compiler - {0}", saveFileDialog.FileName);
			}
		}

		private void buildToolStripMenuItem_Click(object sender, EventArgs e)
		{
			BuildSource();
		}

		private void logListBox_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (logListBox.SelectedIndex != -1)
			{
				if (m_errorPositions.ContainsKey(logListBox.SelectedIndex))
				{
					SourceBox.SelectionStart = m_errorPositions[logListBox.SelectedIndex];
					SourceBox.SelectionLength = 1;
				}
			}
		}
    }
}
