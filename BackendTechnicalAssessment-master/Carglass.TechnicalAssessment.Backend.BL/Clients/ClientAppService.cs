using AutoMapper;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Carglass.TechnicalAssessment.Backend.BL;

internal class ClientAppService : IClientAppService
{
    private readonly ICrudRepository<Client> _clientRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ClientDto> _validator;

    public ClientAppService(
        ICrudRepository<Client> clientRepository,
        IMapper mapper,
        IValidator<ClientDto> validator)
    {
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public IEnumerable<ClientDto> GetAll()
    {
        var clients = _clientRepository.GetAll();
        return _mapper.Map<IEnumerable<ClientDto>>(clients);
    }

    public ClientDto GetById(params object[] keyValues)
    {
        var client = _clientRepository.GetById(keyValues);
        return _mapper.Map<ClientDto>(client);
    }

    public void Create(ClientDto client)
    {
        if (null != _clientRepository.GetById(client.Id))
        {
            throw new Exception("Ya existe un cliente con este Id");
        }
        
        ValidateDto(client);

        if (_clientRepository.GetAll().Any(c =>
            string.Equals(c.DocNum, client.DocNum, StringComparison.OrdinalIgnoreCase)))
        {
            throw new Exception("Ya existe un cliente con este número de documento");
        }

        var entity = _mapper.Map<Client>(client);
        _clientRepository.Create(entity);
    }

    public void Update(ClientDto client)
    {
        if (null == _clientRepository.GetById(client.Id))
        {
            throw new Exception("No existe ningún cliente con este Id");
        }

        ValidateDto(client);

        var entity = _mapper.Map<Client>(client);
        _clientRepository.Update(entity);
    }

    public void Delete(ClientDto client)
    {
        var entity = _clientRepository.GetById(client.Id);
        if (null == entity)
        {
            throw new Exception("El cliente no existe");
        }

        _clientRepository.Delete(entity);
    }

    private void ValidateDto(ClientDto item)
    {
        var validationResult = _validator.Validate(item);
        if (validationResult.Errors.Any())
        {
            string toShowErrors = string.Join("; ", validationResult.Errors.Select(s => s.ErrorMessage));
            throw new Exception($"El cliente especificado no cumple los requisitos de validación. Errores: '{toShowErrors}'");
        }
    }
}