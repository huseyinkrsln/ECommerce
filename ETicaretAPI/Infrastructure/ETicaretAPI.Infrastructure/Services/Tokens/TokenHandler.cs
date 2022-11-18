using ETicaretAPI.Application.Abstractions.Tokens;
using ETicaretAPI.Application.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Tokens
{
    /*ADIM3 JWT
     * 
     * */
    public class TokenHandler : ITokenHandler
    {
        IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateAccessToken(int minute)
        {
            Token token = new();
            /* öncelikle symmetric key oluşturmalıyız
             * değerleri appsetting jsondan alcağımız için ICOnfigurationdan DI yapıyoruz
             */
            //SEcurity key in simetriğinin alıyoruz
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //şifrelenmiş kimliği oluşturuyoruz
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //oluşturulacak token ayarlarını veriyoruz
            token.Expiration = DateTime.UtcNow.AddMinutes(minute);

            //Tokenla ilgilli ayarları oluşturabilmek için ve 
            ////oluşturulacak tokenın hangi değerlerde oluturulacağını belirliyoruz
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow, // token üretildikten ne kadar süre sonra devreye girsin.Buradaki davranışta üretilir üretilmez devreye girecek
                signingCredentials:signingCredentials //security key i oluşturduğumuz bilgiler doğrultusunda olacak 
                );

            //Token oluştrucu sınıfından bir örnek alalım
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            return token;
            
            //ADım 4 loginUserCommandhandler da
        }
    }
}
