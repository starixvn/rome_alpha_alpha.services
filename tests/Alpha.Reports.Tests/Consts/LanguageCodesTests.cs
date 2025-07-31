using Alpha.Reports.Consts;
using Xunit;

namespace Alpha.Reports.Tests.Consts;

public class LanguageCodesTests
{
    [Fact]
    public void SupportLanguages_ShouldContainAllDefinedLanguages()
    {
        // Arrange
        var expectedLanguages = new[]
        {
                LanguageCodes.English,
                LanguageCodes.ChineseTraditional,
                LanguageCodes.ChineseSimplified,
                LanguageCodes.Thai,
                LanguageCodes.Vietnamese
            };

        // Act
        var supportLanguages = LanguageCodes.SupportLanguages;

        // Assert
        Assert.Equal(expectedLanguages.Length, supportLanguages.Length);
        foreach (var language in expectedLanguages)
        {
            Assert.Contains(language, supportLanguages);
        }
    }

    [Fact]
    public void LanguageKeys_ShouldContainAllSupportedLanguages()
    {
        // Arrange
        var supportLanguages = LanguageCodes.SupportLanguages;

        // Act & Assert
        foreach (var language in supportLanguages)
        {
            Assert.True(LanguageCodes.LanguageKeys.ContainsKey(language));
        }
    }

    [Theory]
    [InlineData("zh-CN", "zh-Hans")]
    [InlineData("zh-TW", "zh-Hant")]
    public void MapLanguage_WithMappableLanguage_ShouldReturnMappedLanguage(string input, string expected)
    {
        // Act
        var result = LanguageCodes.MapLanguage(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("en-US")]
    [InlineData("th-TH")]
    [InlineData("vi-VN")]
    public void MapLanguage_WithSupportedLanguage_ShouldReturnSameLanguage(string input)
    {
        // Act
        var result = LanguageCodes.MapLanguage(input);

        // Assert
        Assert.Equal(input, result);
    }

    [Theory]
    [InlineData("fr-FR")]
    [InlineData("de-DE")]
    [InlineData("es-ES")]
    public void MapLanguage_WithUnsupportedLanguage_ShouldReturnEnglish(string input)
    {
        // Act
        var result = LanguageCodes.MapLanguage(input);

        // Assert
        Assert.Equal(LanguageCodes.English, result);
    }

    [Theory]
    [InlineData("zh-Hans", "zh-CN")]
    [InlineData("zh-Hant", "zh-TW")]
    public void MapWebAppLanguage_WithMappableLanguage_ShouldReturnMappedLanguage(string input, string expected)
    {
        // Act
        var result = LanguageCodes.MapWebAppLanguage(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("en-US")]
    [InlineData("th-TH")]
    [InlineData("vi-VN")]
    public void MapWebAppLanguage_WithSupportedLanguage_ShouldReturnSameLanguage(string input)
    {
        // Act
        var result = LanguageCodes.MapWebAppLanguage(input);

        // Assert
        Assert.Equal(input, result);
    }

    [Theory]
    [InlineData("fr-FR")]
    [InlineData("de-DE")]
    [InlineData("es-ES")]
    public void MapWebAppLanguage_WithUnsupportedLanguage_ShouldReturnEnglish(string input)
    {
        // Act
        var result = LanguageCodes.MapWebAppLanguage(input);

        // Assert
        Assert.Equal(LanguageCodes.English, result);
    }
}
