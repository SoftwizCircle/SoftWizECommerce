using SoftWizBusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftWizECommerce.AuthService
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, string audience, UserDTO user);
    }
}
