using Moq;
using Tanks.Application.Services;
using Tanks.Domain.Models;
using Tanks.Domain.DTOs;
using Tanks.Infrastructure.Interfaces;

namespace Tanks.Tests;

public class TankServiceTests
{
    private readonly Mock<ITankRepository> _mockTankRepository;
    private readonly TankService _tankService;

    public TankServiceTests()
    {
        _mockTankRepository = new Mock<ITankRepository>();
        _tankService = new TankService(_mockTankRepository.Object);
    }

    [Fact]
    public async Task GetTankByIdAsync_ShouldReturnTank_WhenTankExists()
    {
        // Arrange
        var tankId = Guid.NewGuid();
        var tank = new Tank { Id = tankId, Name = "Test Tank", Health = 100 };
        _mockTankRepository.Setup(r => r.GetByIdAsync(tankId)).ReturnsAsync(tank);

        // Act
        var result = await _tankService.GetTankByIdAsync(tankId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(tankId, result.Id);
        _mockTankRepository.Verify(r => r.GetByIdAsync(tankId), Times.Once);
    }

    [Fact]
    public async Task GetTankByIdAsync_ShouldReturnNull_WhenTankDoesNotExist()
    {
        // Arrange
        var tankId = Guid.NewGuid();
        _mockTankRepository.Setup(r => r.GetByIdAsync(tankId)).ReturnsAsync((Tank)null);

        // Act
        var result = await _tankService.GetTankByIdAsync(tankId);

        // Assert
        Assert.Null(result);
        _mockTankRepository.Verify(r => r.GetByIdAsync(tankId), Times.Once);
    }

    [Fact]
    public async Task GetAllTanksAsync_ShouldReturnAllTanks()
    {
        // Arrange
        var tanks = new List<Tank>
        {
            new Tank { Id = Guid.NewGuid(), Name = "Tank 1", Health = 100 },
            new Tank { Id = Guid.NewGuid(), Name = "Tank 2", Health = 150 }
        };
        _mockTankRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(tanks);

        // Act
        var result = await _tankService.GetAllTanksAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        _mockTankRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task AddTankAsync_ShouldAddTank()
    {
        // Arrange
        var tankDto = new TankDto { Name = "New Tank", Health = 200 };
        _mockTankRepository.Setup(r => r.AddAsync(It.IsAny<Tank>())).Returns(Task.CompletedTask);

        // Act
        await _tankService.AddTankAsync(tankDto);

        // Assert
        _mockTankRepository.Verify(r => r.AddAsync(It.Is<Tank>(t => t.Name == tankDto.Name && t.Health == tankDto.Health)), Times.Once);
    }

    [Fact]
    public async Task UpdateTankAsync_ShouldUpdateTank_WhenTankExists()
    {
        // Arrange
        var tankId = Guid.NewGuid();
        var existingTank = new Tank { Id = tankId, Name = "Old Tank", Health = 100 };
        var tankDto = new TankDto { Name = "Updated Tank", Health = 150 };

        _mockTankRepository.Setup(r => r.GetByIdAsync(tankId)).ReturnsAsync(existingTank);
        _mockTankRepository.Setup(r => r.UpdateAsync(It.IsAny<Tank>())).Returns(Task.CompletedTask);

        // Act
        await _tankService.UpdateTankAsync(tankId, tankDto);

        // Assert
        _mockTankRepository.Verify(r => r.GetByIdAsync(tankId), Times.Once);
        _mockTankRepository.Verify(r => r.UpdateAsync(It.Is<Tank>(t => t.Name == tankDto.Name && t.Health == tankDto.Health)), Times.Once);
    }

    [Fact]
    public async Task UpdateTankAsync_ShouldThrowException_WhenTankDoesNotExist()
    {
        // Arrange
        var tankId = Guid.NewGuid();
        var tankDto = new TankDto { Name = "Updated Tank", Health = 150 };

        _mockTankRepository.Setup(r => r.GetByIdAsync(tankId)).ReturnsAsync((Tank)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _tankService.UpdateTankAsync(tankId, tankDto));
        _mockTankRepository.Verify(r => r.GetByIdAsync(tankId), Times.Once);
        _mockTankRepository.Verify(r => r.UpdateAsync(It.IsAny<Tank>()), Times.Never);
    }

    [Fact]
    public async Task DeleteTankAsync_ShouldDeleteTank_WhenTankExists()
    {
        // Arrange
        var tankId = Guid.NewGuid();
        _mockTankRepository.Setup(r => r.DeleteAsync(tankId)).Returns(Task.CompletedTask);

        // Act
        await _tankService.DeleteTankAsync(tankId);

        // Assert
        _mockTankRepository.Verify(r => r.DeleteAsync(tankId), Times.Once);
    }
}