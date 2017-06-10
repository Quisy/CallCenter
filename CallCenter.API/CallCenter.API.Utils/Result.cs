using System.Collections.Generic;
using System.Linq;

namespace CallCenter.API.Utils
{
    public class Result
    {
        public virtual bool IsSuccess { get; set; } = false;
        public virtual bool IsError { get; set; } = false;
        public virtual bool IsWarning { get; set; } = false;
        public virtual List<string> Messages { get; set; }

        public static List<string> CreateMessagesList(params string[] messages)
        {
            return messages.ToList();
        }
    }

    public class Result<T> : Result
    {
        private T _value;

        public T Value
        {
            get { return _value; }
            set
            {
                _value = value;
                IsSuccess = true;
            }
        }

        public static Result<T> Error(List<string> messages = null)
        {
            var result = new Result<T>
            {
                Value = default(T),
                IsSuccess = false,
                IsError = true,
                Messages = messages
            };

            return result;
        }

        public static Result<T> Error(string message = null)
        {
            var result = new Result<T>
            {
                Value = default(T),
                IsSuccess = false,
                IsError = true,
                Messages = new List<string> { message }
            };

            return result;
        }

        public static Result<T> ErrorWhenNoData(T data, List<string> messages = null)
        {
            var result = new Result<T>();

            if (data == null)
            {
                result.IsError = true;
                result.Messages = messages;

            }
            else
            {
                result.IsSuccess = true;
                result.Value = data;
            }
            return result;
        }

        public static Result<T> Warning(T data, string message = null)
        {
            var result = new Result<T>
            {
                Value = data,
                IsWarning = true,
                Messages = new List<string> { message }
            };

            return result;
        }

        public static Result<T> Warning(T data, List<string> messages = null)
        {
            var result = new Result<T>
            {
                Value = data,
                IsWarning = true,
                Messages = messages
            };

            return result;
        }

        public static Result<T> WarningWhenNoData(T data, List<string> messages = null)
        {
            var result = new Result<T>();

            if (data == null)
            {
                result.IsWarning = true;
                result.Messages = messages;

            }
            else
            {
                result.IsSuccess = true;
                result.Value = data;
            }
            return result;
        }

        public static Result<T> WarningWhenNoData(T data, string message)
        {
            var result = new Result<T>();

            if (data == null)
            {
                result.IsWarning = true;
                result.Messages = new List<string> { message };
            }
            else
            {
                result.IsSuccess = true;
                result.Value = data;
            }
            return result;
        }
    }
}
