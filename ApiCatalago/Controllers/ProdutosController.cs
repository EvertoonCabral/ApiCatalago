using ApiCatalago.DTOs;
using ApiCatalago.Models;
using ApiCatalago.Pagination;
using ApiCatalago.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiCatalago.Controllers
{
    /// <summary>
    /// Controller responsible for handling operations related to "Produtos".
    /// Provides endpoints for creating, reading, updating, and deleting product data,
    /// as well as retrieving products filtered by specific criteria such as category.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        /// <summary>
        /// Represents an instance of the UnitOfWork class, which is responsible for providing
        /// access to repository objects and managing database transactions in the application.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Provides an instance of the AutoMapper <see cref="IMapper"/> used to handle object-object mapping within the controller.
        /// </summary>
        /// <remarks>
        /// The <c>_mapper</c> field is responsible for transforming data between domain entities and DTOs.
        /// It facilitates converting complex data models to DTOs or mapping incoming data to domain models in operations such as
        /// GET, POST, PUT, and PATCH requests, ensuring cleaner and more maintainable code by abstracting the mapping logic.
        /// </remarks>
        private readonly IMapper _mapper;


        /// <summary>
        /// Controller responsible for managing operations related to products.
        /// </summary>
        public ProdutosController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        /// Retrieves a list of all products in the catalog, ordered by their ID.
        /// <returns>An ActionResult containing an IEnumerable of ProdutoDTOs representing the list of all products.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAllProdutos()
        {
            var produtos = await _unitOfWork.ProdutoRepository.GetAllAsync();
            if (produtos is null)
            {
                return NotFound();
            }
            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos.OrderBy((p => p.ProdutoId)));
            return Ok(produtosDto);

        }

        [HttpGet("paginado", Name = "Produtos Paginados")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPaginado([FromQuery]QueryStringParameters queryStringParameters)
        {
            var produtos = await _unitOfWork.ProdutoRepository.GetProdutosPaginado(queryStringParameters);

            if (produtos is null)
            {
                return BadRequest();
            }

            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
            
            return Ok(produtosDto);
        }

        [HttpGet("filtro/paginado/preco")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosFiltroPrecoAsync(
            [FromQuery] ProdutosFiltroPreco produtosFiltroPreco)
        {
            var produtos = await _unitOfWork.ProdutoRepository.GetProdutosFiltroPreco(produtosFiltroPreco);
            if (produtos is null)
            {
                return BadRequest();
            }
            
            return ObterProduto(produtos);
        }

        private ActionResult<IEnumerable<ProdutoDTO>> ObterProduto(PagedList<Produto> produtos)
        {
            var metadata = new
            {
                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
            return Ok(produtosDto);
        }


        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>An <see cref="ActionResult{ProdutoDTO}"/> containing the product details if found; otherwise, a NotFound response.</returns>
        [HttpGet("{id}", Name ="Obter Produtos")]
        public async Task<ActionResult<ProdutoDTO>>GetProdutoByIdAsync (int id)
        {
            var produto = await _unitOfWork.ProdutoRepository.GetByIdAsync(id);
            if (produto is null)
            {
                return NotFound("Produto nao encontrado");
            }
            var produtoDto = _mapper.Map<ProdutoDTO>(produto);
            return Ok(produtoDto);

        }

        /// <summary>
        /// Retrieves a list of products associated with a specific category.
        /// </summary>
        /// <param name="idCategoria">The identifier of the category to retrieve products for.</param>
        /// <returns>A list of products belonging to the specified category if found, otherwise a NotFound result.</returns>
        [HttpGet("categoria/{idCategoria}", Name = "Obter Produtos por Categoria")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPorCategoriaAsync(int idCategoria)
        {
            var produtos =  await _unitOfWork.ProdutoRepository.GetProdutosPorCategoria(idCategoria);
            if (produtos is null)
            {
                return NotFound("Produto nao encontrado");
            }
            return Ok(produtos);
        }

        /// <summary>
        /// Adds a new product to the database and returns the created product with its details.
        /// </summary>
        /// <param name="produtoDto">The product data transfer object containing the product details to be added.</param>
        /// <returns>The created product data transfer object, including its unique identifier and other details.</returns>
        [HttpPost]
        public ActionResult<ProdutoDTO> AddProduto(ProdutoDTO produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            var novoProduto = _unitOfWork.ProdutoRepository.Create(produto);
            _unitOfWork.CommitAsync();
            var novoProdutoDto = _mapper.Map<ProdutoDTO>(novoProduto);
            return new CreatedAtRouteResult("Obter Produtos", new { id = novoProduto.ProdutoId }, novoProdutoDto);
        }


        /// <summary>
        /// Updates the stock information of a product using a partial update request.
        /// </summary>
        /// <param name="id">The unique identifier of the product to be updated.</param>
        /// <param name="request">
        /// The JSON Patch document containing the partial update for the product's stock information.
        /// </param>
        /// <returns>
        /// An <see cref="ActionResult"/> containing the updated product details as a <see cref="ProdutoDTOUpdateResponse"/> if the update is successful,
        /// or a corresponding HTTP error response if the update fails.
        /// </returns>
        [HttpPatch("{id}/UpdateEstoque", Name = "Atualizar Estoque")]
        public async Task<ActionResult<ProdutoDTOUpdateResponse>> PatchEstoque(int id, JsonPatchDocument<ProdutoDTOUpdateRequest> request)
        {
            if (id <= 0 || request is null)
            {
                return BadRequest();
            }
            var produto = await _unitOfWork.ProdutoRepository.GetByIdAsync(id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado...");
            }
            
            var produtoDto = _mapper.Map<ProdutoDTOUpdateRequest>(produto);
            request.ApplyTo(produtoDto, ModelState);
            if (!TryValidateModel(produtoDto))
            {
                return BadRequest(ModelState);
            }
            
            _mapper.Map(produtoDto, produto);
            _unitOfWork.ProdutoRepository.Update(produto);
            _unitOfWork.CommitAsync();
            return Ok(
                
                _mapper.Map<ProdutoDTOUpdateResponse>(produto)
                
                );
            
        }

        /// <summary>
        /// Updates an existing product with the provided data.
        /// </summary>
        /// <param name="id">The unique identifier of the product to be updated.</param>
        /// <param name="produtoDto">The updated product data encapsulated in a ProdutoDTO object.</param>
        /// <returns>A ProdutoDTO object representing the updated product, or a NotFound result if the product does not exist.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoDTO>> UpdateProduto(int id, ProdutoDTO produtoDto)
        {
        
            var produto = await _unitOfWork.ProdutoRepository.GetByIdAsync(id);
            if (produto is null)
            {
                return NotFound();
            }
            _mapper.Map(produtoDto, produto);
            _unitOfWork.ProdutoRepository.Update(produto);
            _unitOfWork.CommitAsync();
            return Ok(_mapper.Map<ProdutoDTO>(produto));

        }


        /// <summary>
        /// Removes a product identified by the given id.
        /// </summary>
        /// <param name="id">The ID of the product to be removed.</param>
        /// <returns>
        /// An <see cref="ActionResult{ProdutoDTO}"/> containing the details of the removed product
        /// if the operation is successful, or a NotFound result if the product does not exist.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProdutoDTO>> RemoveProduto(int id)
        {
            var produto = await _unitOfWork.ProdutoRepository.GetByIdAsync(id);
            if (produto is null)
            {
                return NotFound();
            }
            _unitOfWork.ProdutoRepository.Delete(produto);
            _unitOfWork.CommitAsync();
            var produtoDto = _mapper.Map<ProdutoDTO>(produto);
            return Ok(produtoDto);


        }
    }
}
