namespace Ecom_Api.Helper
{
    public class ResponseAPI
    {
        public ResponseAPI(int statusCod,string message=null)
        {
            StatusCod = statusCod;
            Message = message??GetMessgeFromStatusCod(statusCod);
        }

        private string GetMessgeFromStatusCod(int statusCod)
        {
            return statusCod switch
            {
                200 => "OK",
                201 => "Created",
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                500 => "Internal Server Error",
                _=> "Unknown Status"
            };
        }   

        public int StatusCod { get; set; }
        public string? Message { get; set; }
    }
}
