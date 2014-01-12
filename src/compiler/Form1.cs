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
using System.Runtime.InteropServices;
using System.Threading;


namespace compiler
{
    public partial class Form1 : Form
    {
		private const string outputFileName = "result.s";
		//номер элемента в логе -> позиция в коде
		private Dictionary<int, SourcePosition> m_errorPositions = new Dictionary<int, SourcePosition>();

        private AboutBox1 a = new AboutBox1();

        public Form1()
        {
            InitializeComponent();
        }

        private bool BuildSource()
        {
			bool result = false;
            Log("_______________________________________________________________");
            Log("");

            var outStream = new FileStream(outputFileName, FileMode.Create);

            Log("Starting build...");
            var compiler = new Compiler();

            var text = (string)this.Invoke(new Func<string>(() => SourceBox.Text));
            result = compiler.Compile(text, outStream);

            var errorsContainer = compiler.GetErrorsContainer();
            foreach (var ev in errorsContainer)
            {
                text = (string)this.Invoke(new Func<string>(() => SourceBox.Text));
                int id = Log(TextUtils.WriteCompilerError(text, ev));
                m_errorPositions.Add(id, ev.Position);
            }

            if (result)
            {
                Log("Compiled successfully!");
                Log("");
            }
            outStream.Close();
			return result;
        }

        private int Log(object o)
        {
            var r = 0;
            this.Invoke(new MethodInvoker(delegate()
            {
                r = logListBox.Items.Add(o);

                if (logListBox.Items.Count > 0)
                {
                    logListBox.SelectedIndex = Math.Max(0, logListBox.Items.Count - 1);
                }
            }));
            return r;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				SourceBox.Clear();
				SourceBox.Text = File.ReadAllText(openFileDlg.FileName);
				this.Text = string.Format("Py# Compiler - {0}", openFileDlg.FileName);
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
			SourceBox.SelectAll();
			SourceBox.SelectionColor = Color.Black;

			BaseScanner scanner = new BaseScanner();
			scanner.SetText(string.Format("{0}\n", SourceBox.Text));
			Token token;
			SourcePosition pos = scanner.GetSourcePosition();

			while ((token = scanner.GetNextToken()).Type != TokenType.EOF)
			{
				Color color = token.Type == TokenType.ID
					? TokenColor.ColorForID(token.Attribute)
					: TokenColor.GetColor(token);


                if (token.Type == TokenType.STRING_LITERAL)
                {
                    pos.TokenLength += 2;
                }

				if (color != Color.Black)
				{
					SourceBox.Select(pos.Position, pos.TokenLength);
					SourceBox.SelectionColor = color;
				}

			
				pos = scanner.GetSourcePosition();
			}

			SourceBox.SelectAll();
			SourceBox.SelectionBackColor = SourceBox.BackColor;
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
				this.Text = string.Format("Py# Compiler - {0}", saveFileDialog.FileName);
			}
		}

		private void buildToolStripMenuItem_Click(object sender, EventArgs e)
		{
            var t = new Thread(() => BuildSource());
            t.IsBackground = true;
            t.Start();
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
					SourceBox.Focus();
				}
			}
		}

		private void runToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var t = new Thread(() =>
				{
					if (BuildSource())
						Process.Start("run_module.bat", outputFileName);
				}
			);
            t.IsBackground = true;
            t.Start();
		}



		private void SourceBox_TextChanged(object sender, EventArgs e)
		{
			int eventMask = StopRedrawingBox();

			int oldpos = SourceBox.SelectionStart;
			int offset = ReplaceTabs();
			
			recolorSyntax();

			SourceBox.SelectionStart = oldpos + offset;
			SourceBox.SelectionLength = 0;

			StartRedrawingBox(eventMask);
		}

		private int ReplaceTabs()
		{
			const char target = '\t';
			const string replacement = "    ";

			int result = SourceBox.Text.Count(s => s == target);
			SourceBox.Text = SourceBox.Text.Replace(target.ToString(), replacement);
			result *= replacement.Length - 1/*target.length*/;

			return result;
		}

		private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
		{
			m_errorPositions.Clear();
			logListBox.Items.Clear();
		}

		private void logListBox_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				logMenuStrip.Show(this, new Point(e.X + logListBox.Left, e.Y + logListBox.Top));
			}
		}

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            a.Show();
        }
    }
}
