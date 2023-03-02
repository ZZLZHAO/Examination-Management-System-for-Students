using SqlSugar;

namespace Result
{
    public static class ApiResultHelper
    {
        public static ApiResult Success(dynamic data)
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                Msg = "success!",
            };
        }
        public static ApiResult Error(string msg)
        {
            return new ApiResult
            {
                Code = 500,
                Data = null,
                Msg = msg,
            };
        }
    }
}
