using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using AtomicCF;

namespace FliteNet
{
    public partial class MainForm : Form
    {
        private FliteTTS m_flietTTS;
        private const int SINGLE_SAMPLE_INDEX = 1; //Choose any valid index
        private const int TOTAL_SAMPLE_INDEX = 2; //Choose any valid index
        private float m_duration = 0;

        public MainForm()
        {
            InitializeComponent();

            m_flietTTS = new FliteTTS();
        }

        //----------------------------------------
        //Example of a better experience:
        //   + A visual indication is given when work is started
        //     (wait cursor appears)
        //   + A visual indication is given when work is ended
        //     (wait cursor disappears)
        //   - User interface is unresponsive during work
        //   + End user knows when task is completed and UI is responsive
        //     again
        //----------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current =
              System.Windows.Forms.Cursors.WaitCursor;

            PerformanceSampling.StartSample(SINGLE_SAMPLE_INDEX, "Execution Time");

            if (m_flietTTS.SayIt(textBox1.Text))
            {
                PerformanceSampling.StopSample(SINGLE_SAMPLE_INDEX);
                MessageBox.Show(PerformanceSampling.GetSampleDurationText(SINGLE_SAMPLE_INDEX) + "\n"
                    + "Conversion Time: " + 
                    (long) (PerformanceSampling.GetSampleDuration(SINGLE_SAMPLE_INDEX) - m_duration * 1000) + " ms");
            }

            System.Windows.Forms.Cursor.Current =
               System.Windows.Forms.Cursors.Default;
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            //Give user text that explains what is going on
            label2.Text = "Wait! Doing work!";
            //Force the UI to update the text
            //(otherwise it will wait until it processes the repaint
            //message, this may be after we exit this function)
            label2.Update();

            //Show wait cursor
            Cursor.Current = Cursors.WaitCursor;

            //Optional Incremental status update
            progressBar1.Value = 0;

            const int TOTAL_TEST_COUNT = 50;
            const string fileName = @"\Storage Card\TTS_Net.wav";
            
            bool ok = true;

            //Start sampling
            PerformanceSampling.StartSample(TOTAL_SAMPLE_INDEX,
                  "Total");

            long average = 0;

            for (int i = 0; i < TOTAL_TEST_COUNT; i++)
            {
                PerformanceSampling.StartSample(SINGLE_SAMPLE_INDEX, "Single");

                if (!m_flietTTS.TextToSpeech(textBox1.Text, fileName, out m_duration))
                {
                    ok = false;
                    break;
                }

                PerformanceSampling.StopSample(SINGLE_SAMPLE_INDEX);
                average += PerformanceSampling.GetSampleDuration(SINGLE_SAMPLE_INDEX);

                //Optional Incremental status update
                progressBar1.Value = i + 1;
            }

            //Stop sampling
            PerformanceSampling.StopSample(TOTAL_SAMPLE_INDEX);

            //Give text indication that we are done
            //(Update whenever UI normally updates)
            if (ok)
            {
                label2.Text = PerformanceSampling.GetSampleDurationText(TOTAL_SAMPLE_INDEX) + " " +
                    PerformanceSampling.GetSampleDurationText(SINGLE_SAMPLE_INDEX) +
                    "\nAverage: " + average / TOTAL_TEST_COUNT + " ms Duration: " + m_duration + " s" +
                    "\nProgress Bar Update: " + 
                    (PerformanceSampling.GetSampleDuration(TOTAL_SAMPLE_INDEX) - average) / TOTAL_TEST_COUNT + " ms";
            }
            else
            {
                label2.Text = "Fail!";
            }

            //Get rid of the wait cursor
            Cursor.Current = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                {
                    this.textBox1.Text = sr.ReadToEnd();
                }
            }
        }
    }
}