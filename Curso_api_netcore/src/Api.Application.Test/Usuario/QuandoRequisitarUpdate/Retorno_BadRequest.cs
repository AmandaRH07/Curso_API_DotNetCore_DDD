﻿using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interface.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequisitarUpdate
{
    public class Retorno_BadRequest
    {
        private UsersController _controller;
        [Fact(DisplayName = "É Possivel Realizar o Updated")]

        public async Task E_Possivel_Invocar_a_Controller_Update()
        {
            var serviceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock.Setup(m => m.Put(It.IsAny<UserDtoUpdate>())).ReturnsAsync(
                new UserDtoUpdateResult
                {
                    Id = Guid.NewGuid(),
                    Name = nome,
                    Email = email,
                    UpdateAt = DateTime.UtcNow
                }
            );

            _controller = new UsersController(serviceMock.Object);
            _controller.ModelState.AddModelError("Email", "Email é um campo obrigatório");

            var userDtoUpdate = new UserDtoUpdate
            {
                Id = Guid.NewGuid(),
                Name = nome,
                Email = email,
            };

            var result = await _controller.Put(userDtoUpdate);
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
        }
    }
}
