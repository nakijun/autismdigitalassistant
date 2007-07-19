using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using XrossOne.Drawing;
using AdaTimerPpc.Properties;
using OpenNETCF.WindowsCE.Notification;
using System.IO;
using System.Reflection;
using UtilitiesPpc;
using System.Diagnostics;
using System.Threading;
using OpenNETCF.Media;

namespace AdaTimerPpc
{
    public partial class MainForm : AdaBaseForm
    {
        private Microsoft.WindowsCE.Forms.Notification notificationAlarm;

        private const string REGISTRY_RUNNING = "Running";
        private const string REGISTRY_END_TIME = "EndTime";
        private const string REGISTRY_SET_MINUTES = "SetMinutes";
        private const string REGISTRY_PRE_ALARM_ENABLED = "PreAlarmEnabled";
        private const string REGISTRY_ALARM_PERIOD = "AlarmPeriod";
        private const string REGISTRY_TIMER_NOTIFICATION_HANDLE = "Handle";

        private const string REGISTRY_IS_ALARM_SET = "AlarmSet";
        private const string REGISTRY_ALARM_DATE_TIME = "AlarmDateTime";
        private const string REGISTRY_ALARM_NOTIFICATION_HANDLE = "AlarmHandle";

        private DateTime endTime;
        private int setMinutes;
        private int alarmPeriod;
        private bool isPreAlarmEnabled;
        private bool isRunning;
        private int timerNotificationHandle;
        private bool isAlarmSet;
        private DateTime alarmDateTime;
        private int alarmNotificationHandle;

        private RedarwHelper redrawHelper;

        private string menuItemTextStart;

        private string menuItemTextStop;

        private TimeSpan timeRemaining;

        private SoundPlayer soundPlayer;

        private string application;

        private string title;

        public MainForm()
        {
            InitializeComponent();

            this.menuItemTextStart = this.menuItemStartStop.Text;
            this.menuItemTextStop = Resources.Stop;

            this.redrawHelper = new RedarwHelper(this.panelTimer.ClientRectangle);

            this.alarmPeriod = 5;
            this.isPreAlarmEnabled = true;

            this.setMinutes = 6;// 45;

            this.application = Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
            string strAppDir = Path.GetDirectoryName(application);

            this.soundPlayer = new SoundPlayer(strAppDir + "\\Timer.wav");
            this.title = this.Text;
            this.alarmDateTime = DateTime.Now;
        }

        void notificationAlarm_ResponseSubmitted(object sender, Microsoft.WindowsCE.Forms.ResponseSubmittedEventArgs e)
        {

        }

        void notificationAlarm_BalloonChanged(object sender, Microsoft.WindowsCE.Forms.BalloonChangedEventArgs e)
        {
            if (e.Visible)
            {
                this.soundPlayer.PlayLooping();

                if (this.isRunning)
                {
                    this.SetupNotifications();
                    this.SaveSetting();
                }
            }
            else
            {
                this.soundPlayer.Stop();
                this.notificationAlarm.Dispose();

                if (!this.isRunning)
                {
                    this.CleanupNotifications();
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.LoadSetting();
            this.SetupNumericUpDownMinutes();

            if (this.isRunning)
            {
                this.ToggleTimer();
            }
        }

        private void MainForm_Closing(object sender, CancelEventArgs e)
        {
            this.SaveSetting();

            if (this.timer.Enabled)
            {
                this.timer.Enabled = false;
            }

            if (this.soundPlayer != null)
            {
                this.soundPlayer.Stop();
                this.soundPlayer.Dispose();
            }
        }

        private void LoadSetting()
        {
            object o = this.Setting.LocalSetting.GetValue(REGISTRY_ALARM_PERIOD);
            if (o != null)
            {
                this.alarmPeriod = Convert.ToInt32(o);
            }

            o = this.Setting.LocalSetting.GetValue(REGISTRY_END_TIME);
            if (o != null)
            {
                this.endTime = Convert.ToDateTime(o);
            }

            o = this.Setting.LocalSetting.GetValue(REGISTRY_PRE_ALARM_ENABLED);
            if (o != null)
            {
                this.isPreAlarmEnabled = Convert.ToBoolean(o);
            }

            o = this.Setting.LocalSetting.GetValue(REGISTRY_RUNNING);
            if (o != null)
            {
                this.isRunning = Convert.ToBoolean(o);
            }

            o = this.Setting.LocalSetting.GetValue(REGISTRY_SET_MINUTES);
            if (o != null)
            {
                this.setMinutes = Convert.ToInt32(o);
            }

            o = this.Setting.LocalSetting.GetValue(REGISTRY_TIMER_NOTIFICATION_HANDLE);
            if (o != null)
            {
                this.timerNotificationHandle = Convert.ToInt32(o);
            }

            o = this.Setting.LocalSetting.GetValue(REGISTRY_IS_ALARM_SET);
            if (o != null)
            {
                this.isAlarmSet = Convert.ToBoolean(o);
            }

            o = this.Setting.LocalSetting.GetValue(REGISTRY_ALARM_DATE_TIME);
            if (o != null)
            {
                this.alarmDateTime = Convert.ToDateTime(o);
            }

            o = this.Setting.LocalSetting.GetValue(REGISTRY_ALARM_NOTIFICATION_HANDLE);
            if (o != null)
            {
                this.alarmNotificationHandle = Convert.ToInt32(o);
            }

        }

        private void SaveSetting()
        {
            this.Setting.LocalSetting.SetValue(REGISTRY_ALARM_PERIOD, this.alarmPeriod);
            this.Setting.LocalSetting.SetValue(REGISTRY_END_TIME, this.endTime);
            this.Setting.LocalSetting.SetValue(REGISTRY_PRE_ALARM_ENABLED, this.isPreAlarmEnabled);
            this.Setting.LocalSetting.SetValue(REGISTRY_RUNNING, this.isRunning);
            this.Setting.LocalSetting.SetValue(REGISTRY_SET_MINUTES, this.setMinutes);
            this.Setting.LocalSetting.SetValue(REGISTRY_TIMER_NOTIFICATION_HANDLE, this.timerNotificationHandle);
            this.Setting.LocalSetting.SetValue(REGISTRY_IS_ALARM_SET, this.isAlarmSet);
            this.Setting.LocalSetting.SetValue(REGISTRY_ALARM_DATE_TIME, this.alarmDateTime);
            this.Setting.LocalSetting.SetValue(REGISTRY_ALARM_NOTIFICATION_HANDLE, this.alarmNotificationHandle);
        }

        private void SetupNumericUpDownMinutes()
        {
            int min = (this.isPreAlarmEnabled ? this.alarmPeriod + 1 : 1);

            if (this.setMinutes < min)
            {
                this.setMinutes = min;
            }

            this.numericUpDownMinutes.Value = this.setMinutes;
            this.numericUpDownMinutes.Minimum = min;
        }

        private void SetupNotifications()
        {
            DateTime time = new DateTime();
            DateTime timeAlarm = this.endTime.AddMinutes(-this.alarmPeriod);
            UserNotification notification = new UserNotification();

            if (this.isPreAlarmEnabled && timeAlarm.CompareTo(DateTime.Now) > 0)
            {
                time = timeAlarm;
                notification.Text = string.Format(global::AdaTimerPpc.Properties.Resources.TimeAlarm, this.alarmPeriod);
            }
            else if (this.endTime.CompareTo(DateTime.Now) > 0)
            {
                time = this.endTime;
                notification.Text = global::AdaTimerPpc.Properties.Resources.TimeUp;
            }

            if (notification.Text.Length > 0)
            {
                notification.Action = NotificationAction.Dialog;
                notification.Title = this.title;

                UserNotificationTrigger trigger = new UserNotificationTrigger();
                trigger.Application = this.application;
                trigger.Arguments = "-TIMER";
                trigger.Type = NotificationType.Time;
                trigger.StartTime = time;

                this.timerNotificationHandle = Notify.SetUserNotification(trigger, notification);
                Notify.RunAppAtTime(application, time);
            }
        }

        private void CleanupNotifications()
        {
            this.notificationAlarm = null;
            Notify.ClearUserNotification(this.timerNotificationHandle);
            Notify.RunAppAtTime(this.application, DateTime.Now);
        }

        private void menuItemStartStop_Click(object sender, EventArgs e)
        {
            this.ToggleTimer();

            if (this.isRunning)
            {
                DateTime now = DateTime.Now;
                this.endTime = now + new TimeSpan(0, this.setMinutes, 0);

                this.SetupNotifications();
            }
            else
            {
                this.CleanupNotifications();
            }

            this.SaveSetting();
        }

        private void ToggleTimer()
        {
            if (timer.Enabled)
            {
                this.timer.Enabled = false;
                this.menuItemStartStop.Text = this.menuItemTextStart;
                this.numericUpDownMinutes.Enabled = true;
                this.numericUpDownMinutes.Focus();

                this.redrawHelper.StopTimer();
            }
            else
            {
                this.Redraw();

                this.redrawHelper.StartTimer(this.setMinutes);

                this.timer.Enabled = true;
                this.menuItemStartStop.Text = this.menuItemTextStop;
                this.numericUpDownMinutes.Enabled = false;
            }

            this.isRunning = this.timer.Enabled;
            this.menuItemMenu.Enabled = !this.isRunning;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.Redraw();

            if (this.timeRemaining.TotalSeconds <= 0)
            {
                this.ToggleTimer();

                this.ShowNotification(global::AdaTimerPpc.Properties.Resources.TimeUp);
            }
            else if (this.isPreAlarmEnabled && this.timeRemaining.Minutes < this.alarmPeriod && this.notificationAlarm == null)
            {
                this.ShowNotification(global::AdaTimerPpc.Properties.Resources.TimeAlarm);
            }
        }

        private void ShowNotification(string alarmString)
        {
            Notify.ClearUserNotification(this.timerNotificationHandle);

            FormEngine.BringWindowToTop(this);

            if (this.notificationAlarm != null)
            {
                this.notificationAlarm.Dispose();
            }

            this.notificationAlarm = new Microsoft.WindowsCE.Forms.Notification();

            this.notificationAlarm.Caption = this.Text;
            this.notificationAlarm.Critical = false;

            this.notificationAlarm.InitialDuration = 60;
            this.notificationAlarm.BalloonChanged += new Microsoft.WindowsCE.Forms.BalloonChangedEventHandler(notificationAlarm_BalloonChanged);
            this.notificationAlarm.ResponseSubmitted += new Microsoft.WindowsCE.Forms.ResponseSubmittedEventHandler(notificationAlarm_ResponseSubmitted);

            int minute = this.timeRemaining.Minutes;
            if (this.timeRemaining.Seconds > 0)
            {
                minute++;
            }

            this.notificationAlarm.Text = string.Format(alarmString, minute);
            this.notificationAlarm.Critical = true;

            //this.notificationAlarm.Icon = global::AdaTimerPpc.Properties.Resources.AlarmIcon;
            this.notificationAlarm.Visible = true;
        }

        private void Redraw()
        {
            int secondsRemaining = 0;
            int minutesRemaining = 0;

            if (this.timer.Enabled)
            {
                timeRemaining = this.endTime - DateTime.Now;

                if (this.timeRemaining.TotalSeconds <= 0)
                {
                    minutesRemaining = 0;
                    secondsRemaining = 0;

                    timeRemaining = new TimeSpan();
                }
                else
                {
                    minutesRemaining = timeRemaining.Minutes;
                    secondsRemaining = timeRemaining.Seconds;
                }
            }
            else
            {
                minutesRemaining = this.setMinutes;
                timeRemaining = new TimeSpan(0, this.setMinutes, 0);
            }

            if (secondsRemaining > 0)
            {
                minutesRemaining++;
            }

            int elapsedMinutes = this.redrawHelper.ElapsedMinutes(minutesRemaining);

            if (elapsedMinutes > 0)
            {
                Rectangle rect = this.redrawHelper.prepareGx(minutesRemaining);

                if (elapsedMinutes > 1)
                {
                    this.panelTimer.Invalidate();
                }
                else
                {
                    this.panelTimer.Invalidate(rect);
                }

                this.panelTimer.Update();
            }

            this.labelCountDown.Text = timeRemaining.ToString();
            this.labelCountDown.Update();
        }

        private void panelTimer_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            this.redrawHelper.Flush(g, e.ClipRectangle);
        }

        private void numericUpDownMinutes_ValueChanged(object sender, EventArgs e)
        {
            this.setMinutes = Decimal.ToInt32(this.numericUpDownMinutes.Value);

            this.Redraw();
        }

        private void panelTimer_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.timer.Enabled && this.redrawHelper.PointInClock(e.X, e.Y, ref this.setMinutes))
            {
                SetupNumericUpDownMinutes();
            }
        }

        private void menuItemSetting_Click(object sender, EventArgs e)
        {
            SettingForm f = new SettingForm();

            f.IsPreAlarmEnabled = this.isPreAlarmEnabled;
            f.AlarmPeriod = this.alarmPeriod;

            if (f.ShowDialog() == DialogResult.OK)
            {
                this.isPreAlarmEnabled = f.IsPreAlarmEnabled;
                this.alarmPeriod = f.AlarmPeriod;

                this.SetupNumericUpDownMinutes();

                this.SaveSetting();
            }
        }

        private void menuItemAlarm_Click(object sender, EventArgs e)
        {
            AlarmForm f = new AlarmForm();

            if (this.alarmDateTime.CompareTo(DateTime.Now) <= 0)
            {
                this.isAlarmSet = false;
                this.alarmDateTime = DateTime.Now.AddMinutes(10);
            }

            f.IsAlarmSet = this.isAlarmSet;
            f.AlarmDateTime = this.alarmDateTime;

            if (f.ShowDialog() == DialogResult.OK)
            {
                this.isAlarmSet = f.IsAlarmSet;
                this.alarmDateTime = f.AlarmDateTime;

                Notify.ClearUserNotification(this.alarmNotificationHandle);

                if (this.isAlarmSet)
                {
                    UserNotification notification = new UserNotification();

                    notification.Text = string.Format(global::AdaTimerPpc.Properties.Resources.AlarmMessage, this.title);
                    notification.Action = NotificationAction.Dialog;
                    notification.Title = this.title;

                    UserNotificationTrigger trigger = new UserNotificationTrigger();
                    trigger.Application = this.application;
                    trigger.Arguments = "-ALARM";
                    trigger.Type = NotificationType.Time;
                    trigger.StartTime = this.alarmDateTime;

                    this.alarmNotificationHandle = Notify.SetUserNotification(trigger, notification);
                }

                this.SaveSetting();
            }
        }

        internal class RedarwHelper
        {
            private ThreadExecuteTask threadExecute;

            private const int BORDER_WIDTH = 10;
            private const int LONG_LINE_LENGTH = 20;
            private const int SHORT_LINE_LENGTH = 10;

            private BrushX brushX;
            private PenX penX;
            private PenX linePenX;

            private Rectangle clientRectangle;
            private Rectangle arcRect;
            private Rectangle boarderRect;

            private bool timerEnabled;

            private GraphicsX gx;
            private int currentMinute;
            private Rectangle currentMinuteRedrawRect;

            private GraphicsX gxNextMinute;
            private int nextMinute;
            private Rectangle nextMinuteRedrawRect;

            internal RedarwHelper(Rectangle clientRectangle)
            {
                this.currentMinute = -1;
                this.nextMinute = -1;

                this.clientRectangle = clientRectangle;
                this.arcRect = clientRectangle;

                this.brushX = new SolidBrushX(Color.Red);

                this.penX = new PenX(Color.Gray, BORDER_WIDTH);
                this.linePenX = new PenX(Color.Black);

                this.gx = new GraphicsX(arcRect.Width, arcRect.Height);
                this.gxNextMinute = new GraphicsX(arcRect.Width, arcRect.Height);

                this.arcRect.Width--;
                this.arcRect.Height--;
                this.arcRect.Inflate(-25, -25);

                int delta = BORDER_WIDTH / 2;
                boarderRect = new Rectangle(arcRect.Left + delta, arcRect.Top + delta, arcRect.Width - BORDER_WIDTH, arcRect.Height - BORDER_WIDTH);
                this.boarderRect = this.arcRect;

                this.currentMinuteRedrawRect = clientRectangle;
            }

            internal int ElapsedMinutes(int minutesRemaining)
            {
                return Math.Abs(this.currentMinute - minutesRemaining);
            }

            internal Rectangle prepareGx(int minutes)
            {
                Rectangle newRedrawRect;

                if (this.currentMinute == minutes)
                {
                    return new Rectangle();
                }
                else if (this.nextMinute == minutes)
                {
                    GraphicsX g = this.gx;
                    this.gx = this.gxNextMinute;
                    this.gxNextMinute = g;

                    newRedrawRect = this.nextMinuteRedrawRect;
                }
                else
                {
                    newRedrawRect = this.prepareGxSync(ref this.gx, minutes);
                }

                Rectangle rect;

                if (!this.timerEnabled)
                {
                    rect = this.clientRectangle;
                }
                else
                {
                    if (minutes > 0)
                    {
                        this.prepareNextGx(minutes - 1);
                    }

                    if (this.currentMinuteRedrawRect.IsEmpty)
                    {
                        rect = newRedrawRect;
                    }
                    else
                    {
                        rect = Rectangle.Union(newRedrawRect, this.currentMinuteRedrawRect);
                    }
                }

                this.currentMinute = minutes;
                this.currentMinuteRedrawRect = newRedrawRect;

                return rect;
            }

            private Rectangle prepareGxSync(ref GraphicsX gx, int minutes)
            {
                gx.Clear(Color.White);

                int degree = minutes * 6;

                if (degree == 360)
                {
                    gx.FillEllipse(this.brushX, arcRect);
                }
                else
                {
                    gx.FillPie(this.brushX, arcRect, -90, -degree);
                }

                this.DrawTicks(ref gx, minutes);
                gx.DrawEllipse(this.penX, this.boarderRect);

                return this.CalculateRedrawRect(minutes);
            }

            private void DrawTicks(ref GraphicsX gx, int minutes)
            {
                for (minutes = 0; minutes <= 60; minutes++)
                {
                    int degree = -90 - minutes * 6;
                    double d = degree * Math.PI / 180.0;

                    int r = this.arcRect.Width / 2;
                    int x = this.arcRect.X + r;
                    int y = this.arcRect.Y + r;

                    int width = (int)Math.Round(r * Math.Cos(d));
                    int height = (int)Math.Round(r * Math.Sin(d));

                    int x1 = x + width;
                    int y1 = y + height;

                    if (minutes % 5 == 0)
                    {
                        r -= LONG_LINE_LENGTH;
                    }
                    else
                    {
                        r -= SHORT_LINE_LENGTH;
                    }

                    width = (int)Math.Round(r * Math.Cos(d));
                    height = (int)Math.Round(r * Math.Sin(d));

                    int x2 = x + width;
                    int y2 = y + height;

                    gx.DrawLine(linePenX, x1, y1, x2, y2);
                }
            }

            private Rectangle CalculateRedrawRect(int minutes)
            {
                int degree = -90 - minutes * 6;
                double d = degree * Math.PI / 180.0;

                int r = this.arcRect.Width / 2;
                int width = (int)Math.Round(r * Math.Cos(d));
                int height = (int)Math.Round(r * Math.Sin(d));

                int x = this.arcRect.X + r;
                int y = this.arcRect.Y + r;

                int x1 = x + width;
                int y1 = y + height;

                if (x > x1)
                {
                    x = x1;
                }

                if (y > y1)
                {
                    y = y1;
                }

                return new Rectangle(x, y, Math.Abs(width) + 1, Math.Abs(height) + 1);
            }

            internal void StartTimer(int setMinutes)
            {
                this.timerEnabled = true;
                this.currentMinuteRedrawRect.Width = 0;
                this.currentMinuteRedrawRect.Height = 0;

                this.prepareNextGx(setMinutes - 1);
            }

            private void prepareNextGx(int minutes)
            {
                this.nextMinute = minutes;

                if (this.threadExecute != null)
                {
                    this.threadExecute.Dispose();
                }

                //Package the class' method entry point up in a delegate
                ThreadExecuteTask.ExecuteMeOnAnotherThread delegateCallCode;
                delegateCallCode = new ThreadExecuteTask.ExecuteMeOnAnotherThread(this.prepareGxAsync);

                //Tell the thread to get going!
                this.threadExecute = new ThreadExecuteTask(delegateCallCode);
            }

            void prepareGxAsync(ThreadExecuteTask threadExecute)
            {
                try
                {
                    this.nextMinuteRedrawRect = this.prepareGxSync(ref this.gxNextMinute, this.nextMinute);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }

            internal void StopTimer()
            {
                this.timerEnabled = false;
            }


            internal bool PointInClock(int x, int y, ref int setMinutes)
            {
                int r = this.arcRect.Width / 2;

                x = x - this.arcRect.X - r;
                y = y - this.arcRect.Y - r;

                double d = Math.Atan2(y, x);

                int width = (int)Math.Round(r * Math.Cos(d));
                int height = (int)Math.Round(r * Math.Sin(d));

                if (Math.Abs(x) <= Math.Abs(width) && Math.Abs(y) <= Math.Abs(height))
                {
                    double degree = Math.Round(180 * d / Math.PI) + 90;
                    setMinutes = (int)Math.Abs(Math.Round(degree / 6));

                    if (x > 0 || y > 0)
                    {
                        setMinutes = 60 - setMinutes;
                    }

                    return true;
                }

                return false;
            }

            internal void Flush(Graphics g, Rectangle rectangle)
            {
                this.gx.Flush(g, rectangle);
            }
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}