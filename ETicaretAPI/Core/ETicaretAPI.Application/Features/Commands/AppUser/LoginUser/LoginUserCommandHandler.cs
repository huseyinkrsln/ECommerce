using ETicaretAPI.Application.Abstractions.Tokens;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{

    /*JWT ADIM4 
     * ürettiğimiz token ıartık kullanalım
     */
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<AppUsers> _appUser;
        readonly SignInManager<AppUsers> _signInManager;
        readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandler(UserManager<AppUsers> appUser, SignInManager<AppUsers> signInManager, ITokenHandler tokenHandler)
        {
            _appUser = appUser;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        async Task<LoginUserCommandResponse> IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>.Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
           AppUsers user =  await _appUser.FindByNameAsync(request.UsernameOrEmail);
            if (user == null)
                user = await _appUser.FindByEmailAsync(request.Password);

            if (user == null)
                throw new UserNotFoundException();

            SignInResult result= await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if(result.Succeeded)
            {
                //JWt token üretimi
                Token token = _tokenHandler.CreateAccessToken(5);
                //geriye LoginUserCommandResponse döndüğü için bu sınıfın içinde Token türünde response olması yeterli
                return new LoginUserSuccessCommandResponse()
                {
                    Token = token
                };
            }
            //return new LoginUserErrorCommandResponse()
            //{
            //    Message = "Kullanıcı Adı veya Şifre hatalı"
            //};
            
                throw new AuthenticationErrorException();
        }
    }
}
