using System.Threading.Tasks;
using Kanbersky.Customer.Business.Commands;
using Kanbersky.Customer.Business.DTO.Request;
using Kanbersky.Customer.Business.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanbersky.Customer.Api.Controllers
{
    /// <summary>
    /// Customers Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        #region fields

        private readonly IMediator _mediator;

        #endregion

        #region ctor

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region methods

        /// <summary>
        /// Gets the list of all Customers
        /// </summary>
        /// <returns>Returns All Customers List</returns>
        /// <response code="200">Returns All Customers List</response>
        /// <response code="404">If the item is null</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _mediator.Send(new GetAllCustomersQuery());
            return StatusCode(result.StatusCode,result);
        }

        /// <summary>
        /// Get Customer Given By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the Given By Id Customer</returns>
        /// <response code="200">Returns the Given By Id Customer</response>
        /// <response code="404">If the item is null</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery(id));
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Create an Customer
        /// </summary>
        /// <param name="createCustomerRequest"></param>
        /// <returns>A newly created customer</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest createCustomerRequest)
        {
            var result = await _mediator.Send(new CreateCustomerCommand(createCustomerRequest));
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Update an Customer
        /// </summary>
        /// <param name="updateCustomerRequest"></param>
        /// <returns>A newly updated customer</returns>
        /// <response code="200">Returns the newly updated item</response>
        /// <response code="400">If the item is null</response> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerRequest updateCustomerRequest)
        {
            var result = await _mediator.Send(new UpdateCustomerCommand(updateCustomerRequest));
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Delete Customer Given By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns boolean</returns>
        /// <response code="204">Customer Successfully Deleted</response>
        /// <response code="400">If the item is null</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCustomer([FromQuery] int id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand(id));
            return StatusCode(result.StatusCode, result);
        }

        #endregion
    }
}