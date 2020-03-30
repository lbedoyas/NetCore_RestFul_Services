using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using Northwind.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.WebApi.Controllers
{
    [Route("api/customer")]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitofWork;
        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitofWork = unitOfWork;
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_unitofWork.Customer.GetById(id));
        }

        [HttpGet]
        [Route("GetPaginatedCustomer/{page:int}/{rows:int}")]
        public IActionResult GetPaginatedCustomer(int page, int rows)
        {
            return Ok(_unitofWork.Customer.CustomerPagedList(page, rows));
        }

        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            } else
            {
                return Ok(_unitofWork.Customer.Insert(customer));
            }
        }


        [HttpPut]
        public IActionResult Put ([FromBody] Customer customer)
        {
            if (ModelState.IsValid && _unitofWork.Customer.Update(customer))
            {
                return Ok(new { Message = "El cliente esta actualizado" });
            } else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Delete ([FromBody] Customer customer)
        {
            if (customer.Id > 0)
            {
                return Ok(_unitofWork.Customer.Delete(customer));
            }else
            {
                return BadRequest();
            }
        }







    }
}
