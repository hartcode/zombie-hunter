using System;
using System.Threading;

namespace AssemblyCSharp
{
	public class ThreadedJob
	{
		
			public bool IsDone = false;
			private Thread thread = null;

		public virtual void Start()
		{
			thread = new Thread (new ThreadStart(Run));
				thread.Start ();
		}

		public virtual void Abort(){
			if (thread != null) {
				thread.Abort ();
			}
		}

		protected virtual void ThreadFunction() {}
		protected virtual void OnFinished() {
		}

		public virtual bool Update()
		{
			bool retval = false;
			if (IsDone) {
				OnFinished ();

				retval = true;
			}
			return retval;
		}

		private void Run()
		{
			ThreadFunction ();
			IsDone = true;
		}
	}
}

