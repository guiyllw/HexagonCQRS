using Domain.Order.Enums;
using Domain.Order.Exceptions;
using Domain.Order.Models;
using Domain.Order.Ports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using WebApi.Interfaces;
using WebApi.StateMachines;

namespace WebApi.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> logger;

        private readonly IStateMachine<Order, OrderStatus, OrderTrigger> stateMachine;

        private readonly IFindOrderByNumberPort findOrderByNumberPort;
        private readonly ICancelOrderPort cancelOrderPort;

        public OrderController(
            ILogger<OrderController> logger,
            IStateMachine<Order, OrderStatus, OrderTrigger> stateMachine,
            IFindOrderByNumberPort findOrderByNumberPort,
            ICancelOrderPort cancelOrderPort)
        {
            this.logger = logger;
            this.stateMachine = stateMachine;
            this.findOrderByNumberPort = findOrderByNumberPort;
            this.cancelOrderPort = cancelOrderPort;
        }

        [HttpGet("{orderNumber}")]
        public async Task<IActionResult> FindOrderByNumberAsync([FromRoute] int orderNumber)
        {
            Order order = await findOrderByNumberPort.FindByNumber(orderNumber);

            return Ok(order);
        }

        [HttpPatch("{orderNumber}/advance")]
        public async Task<IActionResult> AdvanceOrderAsync([FromRoute] int orderNumber, [FromQuery] OrderTrigger trigger)
        {
            try
            {
                Order order = await findOrderByNumberPort.FindByNumber(orderNumber);

                await stateMachine.BuildFor(order).FireAsync(trigger);

                return Ok(order.Number);
            }
            catch (OrderNotFoundException ex)
            {
                logger.LogError(ex, ex.Message);
                return NotFound(orderNumber);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPatch("{orderNumber}/cancel")]
        public async Task<IActionResult> CancelOrderAsync([FromRoute] int orderNumber)
        {
            try
            {
                Order order = await findOrderByNumberPort.FindByNumber(orderNumber);

                await stateMachine.BuildFor(order).FireAsync(OrderTrigger.Cancel);

                return Ok();
            }
            catch (OrderNotFoundException ex)
            {
                logger.LogError(ex, ex.Message);
                return NotFound(orderNumber);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
