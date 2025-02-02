using Moq;
using Tanks.Application.Interfaces;
using Tanks.Application.Services;
using Tanks.Domain.Models;
using Tanks.Infrastructure.Interfaces;
using Tanks.Infrastructure.Repositories;

namespace Tanks.Tests;

public class BattleServiceTests
{
    private readonly Mock<IBattleRepository> _mockBattleRepository;
    private readonly Mock<ITankRepository> _mockTankRepository;
    private readonly BattleService _battleService;

    public BattleServiceTests()
    {
        _mockBattleRepository = new Mock<IBattleRepository>();
        _mockTankRepository = new Mock<ITankRepository>();
        _battleService = new BattleService(_mockBattleRepository.Object, _mockTankRepository.Object);
    }

    [Fact]
    public async Task SimulateBattleAsync_ShouldReturnBattle_WhenTanksExist()
    {
        // Arrange
        var tank1 = new Tank { Id = Guid.NewGuid(), Name = "Tank1", Health = 100 };
        var tank2 = new Tank { Id = Guid.NewGuid(), Name = "Tank2", Health = 80 };
        _mockTankRepository.Setup(r => r.GetByIdAsync(tank1.Id)).ReturnsAsync(tank1);
        _mockTankRepository.Setup(r => r.GetByIdAsync(tank2.Id)).ReturnsAsync(tank2);

        // Act
        var result = await _battleService.SimulateBattleAsync(tank1.Id, tank2.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(tank1.Id, result.WinnerId);
        _mockBattleRepository.Verify(r => r.AddAsync(It.IsAny<Battle>()), Times.Once);
    }

    [Fact]
    public async Task SimulateBattleAsync_ShouldThrowException_WhenTankNotFound()
    {
        // Arrange
        _mockTankRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Tank)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _battleService.SimulateBattleAsync(Guid.NewGuid(), Guid.NewGuid()));
    }
}