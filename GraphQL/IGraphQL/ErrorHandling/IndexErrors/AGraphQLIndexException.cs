﻿using System;
using sones.Library.ErrorHandling;

namespace sones.GraphQL.ErrorHandling
{
    public abstract class AGraphQLIndexException : AGraphQLException
    {
        public override ushort ErrorCode
        {
            get { throw new NotImplementedException(); }
        }
    }
}