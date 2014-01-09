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
using System.Diagnostics;
using compiler.utils;
using System.Runtime.InteropServices;


namespace compiler
{
    public partial class Form1 : Form
    {
		private const string outputFileName = "result.s";
		//номер элемента в логе -> позиция в коде
		private Dictionary<int, SourcePosition> m_errorPositions = new Dictionary<int, SourcePosition>();

        public Form1()
        {
            InitializeComponent();
        }

        private bool BuildSource()
        {
			bool result = false;
            logListBox.Items.Add("_______________________________________________________________");
            logListBox.Items.Add("");
			m_errorPositions.Clear();

            var text = SourceBox.Text + "\n";
            var outStream = new FileStream(outputFileName, FileMode.Create);

            logListBox.Items.Add("Starting build...");
            var compiler = new Compiler();

            result = compiler.Compile(text, outStream);

            var errorsContainer = compiler.GetErrorsContainer();
            foreach (var ev in errorsContainer)
            {
                int id = logListBox.Items.Add(TextUtils.WriteCompilerError(SourceBox.Text, ev));
                m_errorPositions.Add(id, ev.Position);
            }

            if (result)
            {
                logListBox.Items.Add("Compiled successfully!");
                logListBox.Items.Add("");
            }

            if (logListBox.Items.Count > 0)
            {

                logListBox.SelectedIndex = logListBox.Items.Count - 1;
            }
            outStream.Close();
			return result;
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

		private void recolorSyntax()
		{
			//reset colors
			int oldpos = SourceBox.SelectionStart;

			int eventMask = StopRedrawingBox();

			SourceBox.SelectAll();
			SourceBox.SelectionColor = Color.Black;

			BaseScanner scanner = new BaseScanner();
			scanner.SetText(string.Format("{0}\n", SourceBox.Text));
			Token token;
			SourcePosition pos = scanner.GetSourcePosition();

			while ((token = scanner.GetNextToken()).Type != TokenType.EOF)
			{
				Color color = TokenColor.GetColor(token);
				if (color != Color.Black)
				{
					SourceBox.Select(pos.Position, pos.TokenLength);
					SourceBox.SelectionColor = color;
				}
			
				pos = scanner.GetSourcePosition();
			}

			SourceBox.SelectAll();
			SourceBox.SelectionBackColor = SourceBox.BackColor;

			SourceBox.SelectionStart = oldpos;
			SourceBox.SelectionLength = 0;
			StartRedrawingBox(eventMask);
		}

		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

		private int StopRedrawingBox()
		{	
			SendMessage(SourceBox.Handle, 0x0B/*wm_setredraw*/, 0, 0);
			return SendMessage(SourceBox.Handle, 0x400+59/*EM_GETEVENTMASK*/, 0, 0);
		}

		private void StartRedrawingBox(int eventMask)
		{
			SendMessage(SourceBox.Handle, 0x400+69/*EM_SETEVENTMASK*/, 0, eventMask);
			SendMessage(SourceBox.Handle, 0x0B/*WM_SETREDRAW*/, 1, 0);
			SourceBox.Invalidate();
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
                    var sp = m_errorPositions[logListBox.SelectedIndex];
					SourceBox.SelectionStart = sp.Position;
					SourceBox.SelectionLength = sp.TokenLength;
				}
			}
		}

		private void runToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if(BuildSource())
				Process.Start("run_module.bat", outputFileName);
		}



		private void SourceBox_TextChanged(object sender, EventArgs e)
		{
			recolorSyntax();
		}
    }
}
