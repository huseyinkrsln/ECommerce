using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Exceptions
{
    // EXception hazır metodlar sunar 
    public class UserCreateFailedException : Exception
    {
        // default mesaj yazdırdım
        public UserCreateFailedException() : base("Kullanıcı oluşturulurken beklenmeyen hata oluştu")
        {
        }

        public UserCreateFailedException(string? message) : base(message)
        {
        }

        public UserCreateFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
