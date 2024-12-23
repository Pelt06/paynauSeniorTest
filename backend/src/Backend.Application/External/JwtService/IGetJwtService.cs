using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.External.JwtService
{
    public interface IGetJwtService
    {
        string GenerateToken();
    }
}
