using Arch.EntityFrameworkCore.UnitOfWork;
using Domain.Enums.Order;
using Domain.Exceptions.Order;
using Domain.Interfaces.Ports;
using Domain.Models;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> logger;
        private readonly IAdvanceOrderStatusPort advanceOrderStatusPort;

        private readonly IUnitOfWork<WriteContext> unitOfWorkWrite;
        private readonly IUnitOfWork<ReadContext> unitOfWorkRead;

        public OrderController(ILogger<OrderController> logger, IAdvanceOrderStatusPort advanceOrderStatusPort, IUnitOfWork<WriteContext> unitOfWorkWrite, IUnitOfWork<ReadContext> unitOfWorkRead)
        {
            this.logger = logger;
            this.advanceOrderStatusPort = advanceOrderStatusPort;
            this.unitOfWorkWrite = unitOfWorkWrite;
            this.unitOfWorkRead = unitOfWorkRead;
        }

        [HttpGet("seed")]
        public IActionResult Seed()
        {
            unitOfWorkWrite.GetRepository<OrderEntity>().Insert(new OrderEntity { Number = 1 });
            unitOfWorkWrite.GetRepository<OrderEntity>().Insert(new OrderEntity { Number = 2 });
            unitOfWorkWrite.GetRepository<OrderEntity>().Insert(new OrderEntity { Number = 3 });
            unitOfWorkWrite.GetRepository<OrderEntity>().Insert(new OrderEntity { Number = 4 });
            unitOfWorkWrite.GetRepository<OrderEntity>().Insert(new OrderEntity { Number = 5 });

            unitOfWorkWrite.SaveChanges();

            unitOfWorkRead.GetRepository<OrderEntity>().Insert(new OrderEntity { Number = 1 });
            unitOfWorkRead.GetRepository<OrderEntity>().Insert(new OrderEntity { Number = 2 });
            unitOfWorkRead.GetRepository<OrderEntity>().Insert(new OrderEntity { Number = 3 });
            unitOfWorkRead.GetRepository<OrderEntity>().Insert(new OrderEntity { Number = 4 });
            unitOfWorkRead.GetRepository<OrderEntity>().Insert(new OrderEntity { Number = 5 });

            unitOfWorkRead.SaveChanges();

            return Ok();
        }

        [HttpGet("replicate")]
        public IActionResult Replicate()
        {

            foreach (OrderEntity order in unitOfWorkWrite.GetRepository<OrderEntity>().GetAll())
            {
                unitOfWorkRead.GetRepository<OrderEntity>().Update(order);
            }

            unitOfWorkRead.SaveChanges();

            return Ok();
        }

        [HttpGet("list")]
        public IActionResult FindAll()
        {
            IEnumerable<Order> ordersW = unitOfWorkWrite.GetRepository<OrderEntity>()
                .GetAll();

            IEnumerable<Order> ordersR = unitOfWorkRead.GetRepository<OrderEntity>()
                .GetAll();

            return Ok(new
            {
                Write = ordersW,
                Read = ordersR
            });
        }

        [HttpPatch("status")]
        public async Task<IActionResult> AdvanceOrderStatusPort([FromQuery] int orderNumber, [FromQuery] Status status)
        {
            try
            {
                Order order = await advanceOrderStatusPort.AdvanceOrderStatus(orderNumber, status);
                return Ok(order);
            }
            catch (OrderNotFoundException ex)
            {
                logger.LogError(ex, ex.Message);
                return NotFound(orderNumber);
            }
            catch (StatusNotAllowedException ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(status);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
