using System;


namespace FDL.Program
{
    public class InvalidQueryException : Exception {
        public InvalidQueryException()
        {

        }
        public InvalidQueryException(string info) : base(info)
        {

        }
    }
}
