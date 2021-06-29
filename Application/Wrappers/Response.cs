using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Wrappers
{
    public class Response<T>
    {
        public List<string> Errors { get; set; }
        public bool Succeeded =>  Errors.Count == 0;

        public T Data { get; set; }
        public Response()
        {
            Errors = new List<string>();
        }
        public Response(T data) 
        {
            Errors = new List<string>();
            Data = data;
        }
        public void AddError(string error)
        {
            Errors.Add(error);
        }
       
    }
}
