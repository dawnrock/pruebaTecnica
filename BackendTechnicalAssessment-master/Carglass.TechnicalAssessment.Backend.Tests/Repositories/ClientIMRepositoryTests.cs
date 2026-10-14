using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.Entities;
using FluentAssertions;

namespace Carglass.TechnicalAssessment.Backend.Tests.Repositories;

public class ClientIMRepositoryTests
{
    private readonly ClientIMRepository _sut = new();

    private static Client NewClient(int id = 0) => new()
    {
        Id = id,
        DocType = "nif",
        DocNum = "99887766X",
        Email = "test@sample.com",
        GivenName = "Test",
        FamilyName1 = "User",
        Phone = "600600600"
    };

    // GetAll

    [Fact]
    public void GetAll_ReturnsSeededClient()
    {
        var result = _sut.GetAll();

        result.Should().ContainSingle(x => x.Id == 1);
    }

    [Fact]
    public void GetAll_ReturnsCopy_ModifyingItDoesNotAffectRepository()
    {
        var result = _sut.GetAll().ToList();
        result.Clear();

        _sut.GetAll().Should().HaveCount(1);
    }

    // GetById

    [Fact]
    public void GetById_ExistingId_ReturnsClient()
    {
        var result = _sut.GetById(1);

        result.Should().NotBeNull();
        result!.GivenName.Should().Be("Enriqueta");
    }

    [Fact]
    public void GetById_NonExistingId_ReturnsNull()
    {
        _sut.GetById(999).Should().BeNull();
    }

    // Create

    [Fact]
    public void Create_AssignsNewIdAndAddsClient()
    {
        var client = NewClient();

        _sut.Create(client);

        client.Id.Should().Be(2);
        _sut.GetAll().Should().HaveCount(2);
        _sut.GetById(2).Should().NotBeNull();
    }

    // Update

    [Fact]
    public void Update_ExistingClient_ChangesProperties()
    {
        var updated = NewClient(id: 1);
        updated.Email = "nuevo@sample.com";

        _sut.Update(updated);

        _sut.GetById(1)!.Email.Should().Be("nuevo@sample.com");
    }

    [Fact]
    public void Update_NonExistingClient_Throws()
    {
        var act = () => _sut.Update(NewClient(id: 999));

        act.Should().Throw<KeyNotFoundException>();
    }

    // Delete

    [Fact]
    public void Delete_ExistingClient_RemovesIt()
    {
        var existing = _sut.GetById(1)!;

        _sut.Delete(existing);

        _sut.GetAll().Should().BeEmpty();
    }
}