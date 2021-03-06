// Authors:
//   Alan McGovern <alan.mcgovern@gmail.com>
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

using DHTNet.BEncode;
using DHTNet.Messages.Queries;
using DHTNet.Nodes;

namespace DHTNet.Messages.Responses
{
    internal abstract class ResponseBase : DhtMessage
    {
        private static readonly BEncodedString _returnValuesKey = "r";
        private static readonly BEncodedString _responseType = "r";

        protected ResponseBase(NodeId id, BEncodedValue transactionId)
            : base(_responseType)
        {
            Properties.Add(_returnValuesKey, new BEncodedDictionary());
            ReturnValues.Add(IdKey, id.BencodedString());
            TransactionId = transactionId;
        }

        protected ResponseBase(BEncodedDictionary d, QueryBase m)
            : base(d)
        {
            Query = m;
        }

        internal override NodeId Id => new NodeId(((BEncodedString)ReturnValues[IdKey]).TextBytes);

        public BEncodedDictionary ReturnValues => (BEncodedDictionary)Properties[_returnValuesKey];

        public QueryBase Query { get; }
    }
}