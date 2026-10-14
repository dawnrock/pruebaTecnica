using AutoMapper;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Carglass.TechnicalAssessment.Backend.BL;

internal class ProductAppService : IProductAppService
{
    private readonly ICrudRepository<Product> _productRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ProductDto> _validator;

    public ProductAppService(
        ICrudRepository<Product> productRepository,
        IMapper mapper,
        IValidator<ProductDto> validator)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public IEnumerable<ProductDto> GetAll()
    {
        var products = _productRepository.GetAll();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public ProductDto GetById(params object[] keyValues)
    {
        var product = _productRepository.GetById(keyValues);
        return _mapper.Map<ProductDto>(product);
    }

    public void Create(ProductDto product)
    {
        if (null != _productRepository.GetById(product.Id))
        {
            throw new Exception("Ya existe un producto con este Id");
        }
        
        ValidateDto(product);

        var entity = _mapper.Map<Product>(product);
        _productRepository.Create(entity);
    }

    public void Update(ProductDto product)
    {
        if (null == _productRepository.GetById(product.Id))
        {
            throw new Exception("No existe ningún cliente con este Id");
        }

        ValidateDto(product);

        var entity = _mapper.Map<Product>(product);
        _productRepository.Update(entity);
    }

    public void Delete(ProductDto product)
    {
        var entity = _productRepository.GetById(product.Id);
        if (null == entity)
        {
            throw new Exception("El producto no existe");
        }

        _productRepository.Delete(entity);
    }

    private void ValidateDto(ProductDto item)
    {
        var validationResult = _validator.Validate(item);
        if (validationResult.Errors.Any())
        {
            string toShowErrors = string.Join("; ", validationResult.Errors.Select(s => s.ErrorMessage));
            throw new Exception($"El producto especificado no cumple los requisitos de validación. Errores: '{toShowErrors}'");
        }
    }
}