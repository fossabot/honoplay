using System.Net;



namespace Honoplay.Common.Extensions
{
    public static class EnumExtensions
    {
        public static int ToInt(this HttpStatusCode httpStatusCode)
        {
            return (int)httpStatusCode;
        }
    }
}