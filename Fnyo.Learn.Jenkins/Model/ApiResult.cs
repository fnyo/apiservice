namespace Fnyo.Learn.Jenkins.Model
{
    public class ApiResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public object Result { get; set; }

        public int Total { get; set; }


        public void Ok(object result = null)
        {
            Success = true;
            Result = result;    
        }
    }
}
