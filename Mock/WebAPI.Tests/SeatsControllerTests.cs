using System;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Tests;

[TestClass]
public class SeatsControllerTests
{
    [TestMethod]
    public void ReserveSeatOK()
    {
        Mock<SeatsService> serviceMock = new Mock<SeatsService>();
        // Notez l'utilisation de CallBase = true
        // On veut un véritable objet CatsController et changer son comportement seulement pour la propriété UserId!
        // L'option CallBase = true nous permet de garder le comportement normal des méthode de la classe. 
        Mock<SeatsController> controller = new Mock<SeatsController>(serviceMock.Object) { CallBase = true };


        Seat s = new Seat()
        {
            Id = 1,
            Number = 47,
       
        };


        controller.Setup(c => c.UserId).Returns("1");


        var actionResult = controller.Object.ReserveSeat(1);

        var result = actionResult.Result as OkObjectResult;

        Assert.IsNotNull(result);

        Seat? catresult = (Seat?)result!.Value;

    }

    [TestMethod]
    public void ReserveSeatAlreadyTaken()
    {
        Mock<SeatsService> serviceMock = new Mock<SeatsService>();
        // Notez l'utilisation de CallBase = true
        // On veut un véritable objet CatsController et changer son comportement seulement pour la propriété UserId!
        // L'option CallBase = true nous permet de garder le comportement normal des méthode de la classe. 
        Mock<SeatsController> controller = new Mock<SeatsController>(serviceMock.Object) { CallBase = true };


        Seat s = new Seat()
        {
            Id = 1,
            Number = 48,
            ExamenUserId = "2",

        };
        Seat sFree = new Seat()
        {
            Id = 2,
            Number = 47,
            

        };

        controller.Setup(c => c.UserId).Returns("1");


       

        serviceMock.Setup(s => s.ReserveSeat("1", 1)).Throws(new SeatAlreadyTakenException());

       // var actionResult = controller.Object.ReserveSeat(2);

        //Assert.IsNotNull(result);

        //Assert.AreEqual("Cat is not yours", result.Value);
    }
}
