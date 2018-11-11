namespace ADondeIr.Common.Model
{
    using System.Collections.Generic;

    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<Error> Errors { get; set; }
    }
}
