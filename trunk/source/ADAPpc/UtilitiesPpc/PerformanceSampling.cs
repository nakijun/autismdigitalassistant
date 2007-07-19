using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace UtilitiesPpc
{
    public class Timer
    {
        [DllImport("CoreDll.dll")]
        public static extern int QueryPerformanceFrequency(ref Int64 lpFrequency);

        [DllImport("CoreDll.dll")]
        public static extern int QueryPerformanceCounter(ref Int64 lpPerformanceCount);

        static private Int64 m_frequency;
        private Int64 m_start;

        // Static constructor to initialize frequency.
        static Timer()
        {
            if (QueryPerformanceFrequency(ref m_frequency) == 0)
            {
                throw new ApplicationException();
            }
            // Convert to ms.
            m_frequency /= 1000;
        }

        public void Start()
        {
            if (QueryPerformanceCounter(ref m_start) == 0)
            {
                throw new ApplicationException();
            }
        }

        public Int64 Stop()
        {
            Int64 stop = 0;
            if (QueryPerformanceCounter(ref stop) == 0)
            {
                throw new ApplicationException();
            }
            return (stop - m_start) / m_frequency;
        }
    }

    public class PerformanceSampling
    {
        //Arbitrary number, but 8 seems like enough samplers for
        //most uses.
        const int NUMBER_SAMPLERS = 8;

        static string[] m_perfSamplesNames =
                            new string[NUMBER_SAMPLERS];

        static long[] m_perfSamplesDuration =
                            new long[NUMBER_SAMPLERS];

        static UtilitiesPpc.Timer[] m_perfTimers =
                            new UtilitiesPpc.Timer[NUMBER_SAMPLERS];

        static PerformanceSampling()
        {
            for (int i = 0; i < NUMBER_SAMPLERS; i++)
            {
                m_perfTimers[i] = new UtilitiesPpc.Timer();
            }
        }

        //Take a start tick count for a sample
        public static void StartSample(int sampleIndex,
                                         string sampleName)
        {
            m_perfSamplesNames[sampleIndex] = sampleName;
            m_perfTimers[sampleIndex].Start();
        }

        //Take a start tick count for a sample
        public static void StopSample(int sampleIndex)
        {
            m_perfSamplesDuration[sampleIndex] = m_perfTimers[sampleIndex].Stop();
        }

        //Return the length of a sample we have taken
        //(length in milliseconds)
        public static long GetSampleDuration(int sampleIndex)
        {
            return m_perfSamplesDuration[sampleIndex];
        }

        //Returns the number of seconds that have elapsed
        //during the sample period
        public static string GetSampleDurationText(int sampleIndex)
        {
            return m_perfSamplesNames[sampleIndex] + ": " +
              System.Convert.ToString(
                m_perfSamplesDuration[sampleIndex] + " ms");
        }
    }
}
