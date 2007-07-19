using System;
using System.Collections.Generic;
using System.Text;

namespace UtilitiesPpc
{
    public class ThreadExecuteTask : IDisposable
    {
        //States we can be in.
        public enum ProcessingState
        {
            //----------------
            //Initial state
            //----------------
            //Not doing anything interesting yet
            notYetStarted,

            //----------------
            //Working states
            //----------------
            //We are waiting for the background thread to start
            waitingToStartAsync,

            //Code is running in the background thread
            running,

            //Requesting that the calculation be aborted
            requestAbort,

            //----------------
            //Final states
            //----------------

            //Final State: We have successfully completed background
            //execution
            done,

            //Final State: We have aborted the background execution
            //before finishing
            aborted
        }

        private ProcessingState m_processingState;

        private System.Threading.Thread workerThread;

        public delegate void ExecuteMeOnAnotherThread(ThreadExecuteTask checkForAborts);
        private ExecuteMeOnAnotherThread m_CallFunction;
        private object m_useForStateMachineLock;


        public ThreadExecuteTask(ExecuteMeOnAnotherThread functionToCall)
        {
            //Create an object we can use for a lock for the
            //state-machine transition function
            m_useForStateMachineLock = new Object();

            //Mark our execution as ready to start
            m_processingState = ProcessingState.notYetStarted;

            //Store the function we are supposed to call on the new
            //thread
            m_CallFunction = functionToCall;

            //------------------------------------------------------
            //Create a new thread and have it start executing on:
            // this.ThreadStartPoint()
            //------------------------------------------------------
            System.Threading.ThreadStart threadStart;
            threadStart =
              new System.Threading.ThreadStart(ThreadStartPoint);

            workerThread = new System.Threading.Thread(threadStart);

            //Mark our execution as ready to start (for determinism,
            //it is important to do this before we start the thread!)
            setProcessingState(ProcessingState.waitingToStartAsync);

            //Tell the OS to start our new thread async.
            workerThread.Start();
            //Return control to the caller on this thread
        }

        //-------------------------------------------------------
        //This function is the entry point that is called on the
        //new thread
        //-------------------------------------------------------
        private void ThreadStartPoint()
        {
            //Set the processing state to indicate we are running on
            //a new thread!
            setProcessingState(ProcessingState.running);

            //Run the user's code, and pass in a pointer to our class
            //so that code can occasionally call to see if an abort has
            //been requested
            m_CallFunction(this);

            //If we didn't abort, change the execution state to indicate
            //success
            if (m_processingState != ProcessingState.aborted)
            {
                //Mark our execution as done
                setProcessingState(ProcessingState.done);
            }

            //Exit the thread...
        }

        //--------------------------------------------------
        //The state machine.
        //--------------------------------------------------
        public void setProcessingState(ProcessingState nextState)
        {
            //We should only allow one thread of execution to try
            //to modify the state at any given time.
            lock (m_useForStateMachineLock)
            {
                //If we are entering the state we are already in,
                //do nothing.
                if (m_processingState == nextState)
                {
                    return;
                }

                //--------------------------------------------------
                //Some very simple protective code to make sure
                //we can't enter another state if we have either
                //Successfully finished, or Successfully aborted
                //--------------------------------------------------

                if ((m_processingState == ProcessingState.aborted)
                  || (m_processingState == ProcessingState.done))
                {
                    return;
                }

                //Make sure the state transition is valid
                switch (nextState)
                {
                    case ProcessingState.notYetStarted:
                        throw new Exception
                          ("Cannot enter 'notYetStarted' state");

                    case ProcessingState.waitingToStartAsync:
                        if (m_processingState != ProcessingState.notYetStarted)
                        { throw new Exception("Invalid state transition"); }
                        break;

                    case ProcessingState.running:
                        if (m_processingState !=
                           ProcessingState.waitingToStartAsync)
                        { throw new Exception("Invalid state transition"); }

                        break;

                    case ProcessingState.done:
                        //We can complete work only if we have been running.
                        //It is also possible that the user requested an
                        //abort, but we finished the work before aborting
                        if ((m_processingState != ProcessingState.running) &&
                           (m_processingState != ProcessingState.requestAbort)
                          )
                        { throw new Exception("Invalid state transition"); }

                        break;

                    case ProcessingState.aborted:
                        if (m_processingState != ProcessingState.requestAbort)
                        { throw new Exception("Invalid state transition"); }

                        break;
                }

                //Allow the state change
                m_processingState = nextState;
            }
        }

        public ProcessingState State
        {
            get
            {
                ProcessingState currentState;
                //Prevents simultaneous read/write of state
                lock (m_useForStateMachineLock)
                {
                    currentState = m_processingState;
                }
                return currentState;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (this.workerThread != null && this.m_processingState == ProcessingState.running)
            {
                this.workerThread.Abort();
                this.workerThread.Join();
            }
        }

        #endregion
    } // End class
}
