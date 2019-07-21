namespace Harbour.Utils
{
    /// <summary>
    /// Json返回值类
    /// </summary>
    public class JsonData<T>
    {
        /// <summary>
        /// JsonData
        /// </summary>
        public JsonData() { }
        /// <summary>
        /// JsonData
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Msg"></param>
        /// <param name="Data"></param>
        public JsonData(string Code, string Msg, T Data)
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
