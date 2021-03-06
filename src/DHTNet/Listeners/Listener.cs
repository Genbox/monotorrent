// Authors:
//   Alan McGovern alan.mcgovern@gmail.com
//
// Copyright (C) 2008 Alan McGovern
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DHTNet.Enums;

namespace DHTNet.Listeners
{
    public abstract class Listener
    {
        private ListenerStatus _status = ListenerStatus.NotListening;

        protected Listener(IPEndPoint endpoint)
        {
            Endpoint = endpoint;
        }

        public event Action<ListenerStatus> StatusChanged;
        public abstract event Action<byte[], IPEndPoint> MessageReceived;

        public IPEndPoint Endpoint { get; private set; }

        public ListenerStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;

                if (StatusChanged != null)
                    Task.Factory.StartNew(() => StatusChanged(_status), CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
            }
        }

        public abstract void Send(Stream stream, IPEndPoint endpoint);

        public abstract void Start();

        public abstract void Stop();
    }
}