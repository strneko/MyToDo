namespace MyToDo.API.APIResponses
{
    public class APIResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
