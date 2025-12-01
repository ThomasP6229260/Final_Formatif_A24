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
        // Arrange
        var serviceMock = new Mock<SeatsService>();

        // Mock du service pour renvoyer une Seat
        serviceMock.Setup(s => s.ReserveSeat("1", 1))
            .Returns(new Seat { Id = 1, Number = 47, ExamenUserId = "1" });

        // Mock du controller avec CallBase = true
        var controller = new Mock<SeatsController>(serviceMock.Object) { CallBase = true };

        // Mock de la propriété UserId
        controller.Setup(c => c.UserId).Returns("1");

        // Act
        var actionResult = controller.Object.ReserveSeat(1);

        // Assert
        var result = actionResult.Result as OkObjectResult; // Vérification que le résultat est Ok
        Assert.IsNotNull(result); // Vérifie que le résultat n'est pas null

        // Vérifie que la Seat retournée dans le OkObjectResult est celle attendue
        var seat = result!.Value as Seat;
        Assert.IsNotNull(seat); // Vérifie que l'objet est bien une Seat
        Assert.AreEqual(47, seat!.Number); // Vérifie que le numéro du siège est correct
        Assert.AreEqual("1", seat.ExamenUserId); // Vérifie que l'ExamenUserId est correct
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


       

        //serviceMock.Setup(s => s.ReserveSeat("1", 1)).Throws(new SeatAlreadyTakenException());

         var actionResult = controller.Object.ReserveSeat(1);

        var result = actionResult.Result as UnauthorizedResult;

        Assert.IsNotNull(result);

        //Assert.AreEqual("Cat is not yours", result.Value);
    }
}
