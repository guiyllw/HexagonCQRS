using Arch.EntityFrameworkCore.UnitOfWork;
using Infrastructure.Common.Contexts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/seed")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly IUnitOfWork<WriteContext> unitOfWorkWrite;
        private readonly IUnitOfWork<ReadContext> unitOfWorkRead;

        public SeedController(IUnitOfWork<WriteContext> unitOfWorkWrite, IUnitOfWork<ReadContext> unitOfWorkRead)
        {
            this.unitOfWorkWrite = unitOfWorkWrite;
            this.unitOfWorkRead = unitOfWorkRead;
        }

        [HttpGet("seed")]
        public IActionResult Seed()
        {
            unitOfWorkWrite.GetRepository<Infrastructure.Order.Entities.Order>().Insert(new Infrastructure.Order.Entities.Order { Number = 1 });
            unitOfWorkWrite.GetRepository<Infrastructure.Order.Entities.Order>().Insert(new Infrastructure.Order.Entities.Order { Number = 2 });
            unitOfWorkWrite.GetRepository<Infrastructure.Order.Entities.Order>().Insert(new Infrastructure.Order.Entities.Order { Number = 3 });
            unitOfWorkWrite.GetRepository<Infrastructure.Order.Entities.Order>().Insert(new Infrastructure.Order.Entities.Order { Number = 4 });
            unitOfWorkWrite.GetRepository<Infrastructure.Order.Entities.Order>().Insert(new Infrastructure.Order.Entities.Order { Number = 5 });

            unitOfWorkWrite.SaveChanges();

            unitOfWorkRead.GetRepository<Infrastructure.Order.Entities.Order>().Insert(new Infrastructure.Order.Entities.Order { Number = 1 });
            unitOfWorkRead.GetRepository<Infrastructure.Order.Entities.Order>().Insert(new Infrastructure.Order.Entities.Order { Number = 2 });
            unitOfWorkRead.GetRepository<Infrastructure.Order.Entities.Order>().Insert(new Infrastructure.Order.Entities.Order { Number = 3 });
            unitOfWorkRead.GetRepository<Infrastructure.Order.Entities.Order>().Insert(new Infrastructure.Order.Entities.Order { Number = 4 });
            unitOfWorkRead.GetRepository<Infrastructure.Order.Entities.Order>().Insert(new Infrastructure.Order.Entities.Order { Number = 5 });

            unitOfWorkRead.SaveChanges();

            return Ok();
        }

        [HttpGet("replicate")]
        [Obsolete]
        public IActionResult Replicate()
        {

            foreach (Infrastructure.Order.Entities.Order order in unitOfWorkWrite.GetRepository<Infrastructure.Order.Entities.Order>().GetAll())
            {
                unitOfWorkRead.GetRepository<Infrastructure.Order.Entities.Order>().Update(order);
            }

            unitOfWorkRead.SaveChanges();

            return Ok();
        }

        [HttpGet("list")]
        [Obsolete]
        public IActionResult FindAll()
        {
            IEnumerable<Infrastructure.Order.Entities.Order> ordersW = unitOfWorkWrite.GetRepository<Infrastructure.Order.Entities.Order>()
                .GetAll();

            IEnumerable<Infrastructure.Order.Entities.Order> ordersR = unitOfWorkRead.GetRepository<Infrastructure.Order.Entities.Order>()
                .GetAll();

            return Ok(new
            {
                Write = ordersW,
                Read = ordersR
            });
        }
    }
}
