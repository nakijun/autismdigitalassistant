using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using dotnetCHARTING.WinForms;
using ADAAnalyzer.ADAWorkSystemDataSetTableAdapters;

namespace ADAAnalyzer
{
    public partial class MainForm : Form
    {
        private ADAWorkSystemDataSet _dataSet;
        private DataRow[] _userRows;

        public MainForm()
        {
            InitializeComponent();
            _dataSet = new ADAWorkSystemDataSet();

            monthCalendarStart.SelectionStart = DateTime.Now.AddMonths(-2).Date;
            monthCalendarStart.SelectionEnd = monthCalendarStart.SelectionStart;

            monthCalendarEnd.SelectionStart = DateTime.Now.Date;
            monthCalendarEnd.SelectionEnd = monthCalendarEnd.SelectionStart;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CreateChart(ref this.chart1);
            LoadDataSet();
        }

        private void FillUserList()
        {
            listBoxUsers.Items.Clear();

            _userRows = _dataSet.User.Select("", "Name ASC");

            foreach (ADAWorkSystemDataSet.UserRow userRow in _userRows)
            {
                listBoxUsers.Items.Add(userRow.Name);
            }

            if (_userRows.Length > 0)
            {
                listBoxUsers.SelectedIndex = 0;
            }
        }

        private void LoadDataSet()
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                _dataSet.EnforceConstraints = false;

                UserTableAdapter UserAdapter = new UserTableAdapter();
                UserAdapter.Fill(_dataSet.User);

                ScheduleTableAdapter scheduleAdapter = new ScheduleTableAdapter();
                scheduleAdapter.Fill(_dataSet.Schedule);

                ActivityTableAdapter activityAdapter = new ActivityTableAdapter();
                activityAdapter.Fill(_dataSet.Activity);

                _dataSet.EnforceConstraints = true;

                FillUserList();
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }

            Cursor = Cursors.Default;
        }

        private void ReportError(Exception ex)
        {
            string statusMessage = ex.Message.ToString() + "\r\n" + ex.StackTrace;

            // ...post the caller's message to the status bar.
            MessageBox.Show(statusMessage, this.Text,
               MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void CreateChart(ref Chart chart1)
        {
            chart1.Type = ChartType.Combo;//Horizontal;
            chart1.Width = 600;
            chart1.Height = 350;
            chart1.TempDirectory = "temp";
            chart1.Debug = true;


            // This sample will demonstrate how to use calculated time axes.

            chart1.DefaultLegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Bottom;

            chart1.YAxis.Label.Text = "Minutes";

            // *DYNAMIC DATA NOTE* 
            // This sample uses random data to populate the chart. To populate 
            // a chart with database data see the following resources:
            // - Classic samples folder
            // - Help File > Data Tutorials
            // - Sample: features/DataEngine.aspx
            //SeriesCollection mySC = getRandomData(0);

            // Set some properties for the main x axis.
            chart1.XAxis.TimeInterval = dotnetCHARTING.WinForms.TimeInterval.Days;
            chart1.XAxis.Label.Text = "Days";

            // Calculate an axis from the x axis and add it with 'weeks' intervals.
            Axis calculatedXAxis = chart1.XAxis.Calculate("Weeks", dotnetCHARTING.WinForms.TimeInterval.Weeks);
            calculatedXAxis.Orientation = dotnetCHARTING.WinForms.Orientation.Top;
            chart1.AxisCollection.Add(calculatedXAxis);

            // Calculate an axis from the x axis and add it with an advanced time interval (3 Days) intervals.
            //TimeIntervalAdvanced tia = TimeIntervalAdvanced.Days;
            //tia.Multiplier = 3;
            //Axis calculatedXAxis = chart1.XAxis.Calculate("TimeIntervalAdvanced: Every 3 Days", tia);
            //calculatedXAxis.Orientation = dotnetCHARTING.WinForms.Orientation.Top;
            //chart1.AxisCollection.Add(calculatedXAxis);


            //// Add the random data.
            //chart1.SeriesCollection.Add(mySC);

            //mySC = getRandomData(1);
            //chart1.SeriesCollection.Add(mySC);

            //mySC = getRandomData(2);
            //chart1.SeriesCollection.Add(mySC);

            //mySC = getRandomData(3);
            //chart1.SeriesCollection.Add(mySC);
        }

        private void listBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxWorkSystems.Items.Clear();

            if (listBoxUsers.SelectedIndex >= 0)
            {
                ADAWorkSystemDataSet.UserRow userRow = _userRows[listBoxUsers.SelectedIndex] as ADAWorkSystemDataSet.UserRow;

                ADAWorkSystemDataSet.ScheduleRow[] scheduleRows = userRow.GetScheduleRows();

                foreach (ADAWorkSystemDataSet.ScheduleRow scheduleRow in scheduleRows)
                {
                    if (!listBoxWorkSystems.Items.Contains(scheduleRow.Name))
                    {
                        listBoxWorkSystems.Items.Add(scheduleRow.Name);
                    }
                }

                if (scheduleRows.Length > 0)
                {
                    listBoxWorkSystems.SelectedIndex = 0;
                }
                else
                {
                    RefreshChart();
                }
            }
        }

        private void listBoxWorkSystems_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshChart();
        }

        SeriesCollection getSeriesCollection(DataRow[] scheduleRows)
        {
            SeriesCollection SC = new SeriesCollection();

            ADAWorkSystemDataSet.ScheduleRow firstScheduleRow = scheduleRows[0] as ADAWorkSystemDataSet.ScheduleRow;
            ADAWorkSystemDataSet.ActivityRow[] firstScheduleActivityRows = firstScheduleRow.GetActivityRows();

            foreach (ADAWorkSystemDataSet.ActivityRow firstScheduleActivityRow in firstScheduleActivityRows)
            {
                Series s = new Series();
                s.Type = SeriesType.Line;
                s.Line.Width = 4;
                s.Name = firstScheduleActivityRow.Name;

                foreach (ADAWorkSystemDataSet.ScheduleRow scheduleRow in scheduleRows)
                {
                    ADAWorkSystemDataSet.ActivityRow[] activityRows = scheduleRow.GetActivityRows();

                    foreach (ADAWorkSystemDataSet.ActivityRow activityRow in activityRows)
                    {
                        if (activityRow.Name == s.Name)
                        {
                            Element e = new Element();

                            e.XDateTime = activityRow.ExecutionEnd.Date;

                            TimeSpan span = activityRow.ExecutionEnd.Subtract(activityRow.ExecutionStart);
                            e.YValue = span.TotalMinutes;

                            int i = s.Elements.Count - 1;

                            while (i >= 0)
                            {
                                if (s.Elements[i].XDateTime.CompareTo(e.XDateTime) <= 0)
                                {
                                    s.Elements.Insert(i + 1, e);
                                    break;
                                }
                                else
                                {
                                    i--;
                                }
                            }

                            if (i < 0)
                            {
                                s.Elements.Insert(0, e);
                            }

                            break;
                        }
                    }
                }

                //s.DefaultElement.Color = Color.Red;
                SC.Add(s);
            }

            return SC;
        }

        private void RefreshChart()
        {
            Cursor = Cursors.WaitCursor;

            chart1.SeriesCollection.Clear();

            if (listBoxWorkSystems.SelectedIndex >= 0)
            {
                ADAWorkSystemDataSet.UserRow userRow = _userRows[listBoxUsers.SelectedIndex] as ADAWorkSystemDataSet.UserRow;

                string filter = "UserId=" + userRow.UserId + @" AND Name='" + listBoxWorkSystems.SelectedItem + @"'";
                DataRow[] scheduleRows = _dataSet.Schedule.Select(filter);

                chart1.SeriesCollection.Add(getSeriesCollection(scheduleRows));
            }

            chart1.XAxis.ScaleRange = new ScaleRange(monthCalendarStart.SelectionStart,
                monthCalendarEnd.SelectionStart);

            chart1.Invalidate();
            chart1.Update();

            Cursor = Cursors.Default;
        }

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            RefreshChart();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDataSet();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}