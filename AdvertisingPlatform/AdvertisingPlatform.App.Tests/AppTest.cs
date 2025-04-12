using AdvertisingPlatform.App.Exceptions;
using AdvertisingPlatform.App.Repository;
using AdvertisingPlatform.App.Services;
using AdvertisingPlatform.App.Services.Interfaces;
using AdvertisingPlatform.App.Services.Interfaces.Providers;
using AdvertisingPlatform.App.Services.Providers;
using AdvertisingPlatform.App.Services.Validators;

namespace AdvertisingPlatform.App.Tests;

public class AppTest
{
    private readonly IPlatformRepository _repository;
    private readonly IAdvService _service;
    private readonly ILocationsProvider _locationsProvider;
    private readonly IAdvertisingInfoProvider _advertisingInfoProvider;

    public AppTest()
    {
        _repository = new PlatformRepository();

        _locationsProvider = new LocationsProvider(new LocationValidator());

        var lineValidator = new AdvertisingLineValidator();
        var platformLocationsValidator = new PlatformLocationsValidator();
        var platformValidator = new PlatformValidator();
        var locationsLineValidator = new LocationsLineValidator();

        _advertisingInfoProvider = new AdvertisingInfoProvider(
            lineValidator, platformLocationsValidator, platformValidator, locationsLineValidator);

        _service = new AdvService(_repository, _locationsProvider, _advertisingInfoProvider);
    }

    [Fact]
    public void AddAdvertisement_EmptyLine_ThrowsAdvertisingLineValidationException()
    {
        // Arrange
        // Act
        // Assert
        Assert.Throws<AdvertisingLineValidationException>(() => _service.AddPlatform(""));
    }

    [Fact]
    public void AddAdvertisement_MissingColon_ThrowsAdvertisingLineValidationException()
    {

        // Arrange
        var line = "Яндекс.Директ /ru/msk";

        // Act
        // Assert
        Assert.Throws<AdvertisingLineValidationException>(() => _service.AddPlatform(line));
    }

    [Fact]
    public void AddAdvertisement_TooManyColons_ThrowsAdvertisingLineValidationException()
    {
        // Arrange
        var line = "Яндекс.Директ:/ru/msk,Гугл.Директ:/ru/svrd";

        // Act
        // Assert
        Assert.Throws<AdvertisingLineValidationException>(() => _service.AddPlatform(line));
    }

    [Fact]
    public void AddAdvertisement_MissingPlatformName_ThrowsAdvertisingLineValidationException()
    {
        // Arrange
        var line = "   :/ru/msk";

        // Act
        // Assert
        Assert.Throws<AdvertisingLineValidationException>(() => _service.AddPlatform(line));
    }

    [Fact]
    public void AddAdvertisement_MissingLocations_ThrowsAdvertisingLineValidationException()
    {
        // Arrange
        var line = "Яндекс.Директ:";

        // Act
        // Assert
        Assert.Throws<AdvertisingLineValidationException>(() => _service.AddPlatform(line));
    }

    [Fact]
    public void AddAdvertisement_IncorrectLocations_ThrowsLocationValidationException()
    {
        // Arrange
        var line = "Яндекс.Директ:ru";

        // Act
        // Assert
        Assert.Throws<LocationValidationException>(() => _service.AddPlatform(line));
    }

    [Fact]
    public void AddAdvertisement_IncorrectLocations2_ThrowsLocationValidationException()
    {
        // Arrange
        var line = "Яндекс.Директ:/ru,/ru/spb/";

        // Act
        // Assert
        Assert.Throws<LocationValidationException>(() => _service.AddPlatform(line));
    }

    [Fact]
    public void AddLocation_SingleLevel_AddsPlatformCorrectly()
    {
        // Arrange
        _service.AddPlatform("Яндекс.Директ:/ru");

        // Act
        var platforms = _service.GetPlatforms("/ru");

        // Assert
        Assert.Contains("Яндекс.Директ", platforms);
        Assert.Single(platforms);
    }

    [Fact]
    public void AddLocation_MultiLevel_AddsPlatformCorrectly()
    {
        // Arrange
        _service.AddPlatform("Ревдинский рабочий:/ru/svrd/revda");

        // Act
        var platforms = _service.GetPlatforms("/ru/svrd/revda");

        Assert.Contains("Ревдинский рабочий", platforms);
    }

    [Fact]
    public void GetPlatforms_ReturnsAllInheritedPlatforms()
    {
        // Arrange
        _service.AddPlatform("Яндекс.Директ:/ru");
        _service.AddPlatform("Крутая реклама:/ru/svrd");
        _service.AddPlatform("Ревдинский рабочий:/ru/svrd/revda");

        // Act
        var platforms = _service.GetPlatforms("/ru/svrd/revda");

        // Assert
        Assert.Contains("Яндекс.Директ", platforms);
        Assert.Contains("Крутая реклама", platforms);
        Assert.Contains("Ревдинский рабочий", platforms);
        Assert.Equal(3, platforms.Count());
    }

    [Fact]
    public void GetPlatforms_ReturnsOneInheritedPlatform()
    {
        // Arrange
        _service.AddPlatform("Яндекс.Директ:/ru");
        _service.AddPlatform("Крутая реклама:/ru/svrd");
        _service.AddPlatform("Смузи на Фонтанке:/ru/spb/fontanka");

        // Act
        var platforms = _service.GetPlatforms("/ru/spb");

        // Assert
        Assert.Contains("Яндекс.Директ", platforms);
        Assert.Single(platforms);
    }

    [Fact]
    public void GetPlatforms_ReturnsSeveralInheritedPlatform()
    {
        // Arrange
        _service.AddPlatform("Яндекс.Директ:/ru");
        _service.AddPlatform("Крутая реклама:/ru/svrd");
        _service.AddPlatform("Смузи на Фонтанке:/ru/spb/fontanka");
        _service.AddPlatform("Ревдинский рабочий:/ru/svrd/revda");
        _service.AddPlatform("Афиша:/ru/msk/center,/ru/spb");
        _service.AddPlatform("Mail.ru:/ru/msk,/ru/spb");
        _service.AddPlatform("Ekb today:/ru/ekb");
        _service.AddPlatform("Daily Florida:/us/spb");

        // Act
        var platforms = _service.GetPlatforms("/ru/spb");

        // Assert
        Assert.Contains("Яндекс.Директ", platforms);
        Assert.Contains("Афиша", platforms);
        Assert.Contains("Mail.ru", platforms);
        Assert.Equal(3, platforms.Count());
    }

    [Fact]
    public void GetAdPlatforms_UnknownLocation_ReturnsClosestMatch()
    {
        // Arrange
        _service.AddPlatform("Яндекс.Директ:/ru");
        _service.AddPlatform("Крутая реклама:/ru/svrd");

        // Act
        var platforms = _service.GetPlatforms("/ru/svrd/unknown");

        // Assert
        Assert.Contains("Яндекс.Директ", platforms);
        Assert.Contains("Крутая реклама", platforms);
        Assert.Equal(2, platforms.Count());
    }

    [Fact]
    public void GetAdPlatforms_EmptyTrie_ReturnsEmptySet()
    {
        // Arrange

        // Act
        var platforms = _service.GetPlatforms("/ru/svrd");

        // Assert
        Assert.Empty(platforms);
    }

    [Fact]
    public void GetPlatforms_ReturnsEmptySet_WhenNoMatchingLocation()
    {
        // Arrange
        _service.AddPlatform("Яндекс.Директ:/ru");
        _service.AddPlatform("Крутая реклама:/ru/svrd");
        _service.AddPlatform("Ревдинский рабочий:/ru/svrd/revda");

        // Act
        var result = _service.GetPlatforms("/aus/unknown/nowhere");

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void AddAdvertisement_PlatformWithMultipleLocations_RegistersAllLocations()
    {
        // Arrange
        _service.AddPlatform("Газета:/ru/msk,/ru/svrd/revda");

        // Act
        var fromMsk = _service.GetPlatforms("/ru/msk");
        var fromRevda = _service.GetPlatforms("/ru/svrd/revda");

        // Assert
        Assert.Contains("Газета", fromMsk);
        Assert.Contains("Газета", fromRevda);
    }
}