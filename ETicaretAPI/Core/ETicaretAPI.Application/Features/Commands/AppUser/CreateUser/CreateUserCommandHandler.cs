using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        // Identity mekanızmasını Ioc den talep etmek için UserManager kullanılır
        readonly UserManager<AppUsers> _userManager;

        public CreateUserCommandHandler(UserManager<AppUsers> userManager)
        {
            _userManager = userManager;
        }

        async Task<CreateUserCommandResponse> IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>.Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                //identity de string key kullandığımız için Id tataması yaptık default olsaydı Id atmamıza gerek kalmazdı
                Id = Guid.NewGuid().ToString(),
                UserName = request.Username,
                Email = request.Email,
                NameSurname = request.NameSurname,
            }, request.Password) ;


            CreateUserCommandResponse response = new() { Succeeded = result.Succeeded };


            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturulmuştur";
            else
            {
                foreach (var err in result.Errors)
                    response.Message += $"{err.Code} - {err.Description}\n";
            }

            return response;

            //throw new UserCreateFailedException();


        }
    }
}
