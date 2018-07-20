namespace Harbour.Utils
{
    /// <summary>
    /// Json返回值类
    /// </summary>
    public class JsonResponse<T>
    {
        /// <summary>
        /// 实例化JsonResponse
        /// </summary>
        public JsonResponse() { }
        /// <summary>
        /// 实例化JsonResponse
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Msg"></param>
        /// <param name="Data"></param>
        public JsonResponse(string Code, string Msg, T Data)
        {
            this.Code = Code;
            this.Msg = Msg;
            this.Data = Data;
        }

        /// <summary>
        /// 返回码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 返回内容
        /// </summary>
        public T Data { get; set; }
    }
}
