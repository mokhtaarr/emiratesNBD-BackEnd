using DAL.Context;
using DAL.Dto;
using DAL.IRepository;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Emiratesnbd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IGenericRepository<MsCustomer> _customerRepository;
        private readonly EmiratesContext _db;
        public CustomersController(IGenericRepository<MsCustomer> customerRepository, EmiratesContext db)
        {
          _customerRepository = customerRepository;
            _db = db;
        }

        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            return Ok(await _customerRepository.GetAll());
        }

        [HttpDelete("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            ResponseDto res = new ResponseDto();
            try
            {
                await _customerRepository.DeleteAsync(customerId);
                res.messageEn = "Deleted Successfully";
                res.status = true;
                
                return Ok(res);
                
            }catch(Exception ex)
            {
                res.messageEn = "Something went wrong";
                res.status = false;
                return Ok(res);

            }
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer(customerDto dto)
        {
            ResponseDto res = new ResponseDto();
            try
            {
                MsCustomer getCustomer = await _db.MsCustomers.FindAsync(dto.CustomerId);
                if(getCustomer == null)
                {
                    MsCustomer existingCustomerCode = await _db.MsCustomers.FirstOrDefaultAsync(c=>c.CustomerCode == dto.CustomerCode);
                    if(existingCustomerCode is not null)
                    {
                        res.messageEn = $"this customer code already exists , please change it";
                        res.status = false;
                        return Ok(res);
                    }

                    MsCustomer NewCustomer = new MsCustomer
                    {
                        CustomerCode = dto.CustomerCode,
                        CustomerDescA = dto.CustomerDescA,
                        CustomerDescE = dto.CustomerDescE,
                        IsActive = dto.IsActive,
                        Remarks = dto.Remarks,
                        Email = dto.Email
                    };

                    _db.MsCustomers.Add(NewCustomer);
                    await _db.SaveChangesAsync();

                    res.messageEn = "the customer has been added successfully";
                    res.status = true;
                    res.id = NewCustomer.CustomerId;
                    return Ok(res);
                }
                else
                {
                    getCustomer.CustomerCode = dto.CustomerCode;
                    getCustomer.CustomerDescA = dto.CustomerDescA;
                    getCustomer.CustomerDescE = dto.CustomerDescE;
                    getCustomer.IsActive = dto.IsActive;
                    getCustomer.Remarks = dto.Remarks;
                    getCustomer.Email = dto.Email;

                    await _db.SaveChangesAsync();

                    res.messageEn = "Modified successfully";
                    res.status = true;
                    res.id = getCustomer.CustomerId;


                    return Ok(res);
                }

            }
            catch (Exception ex)
            {
                res.messageEn = "Something went wrong";
                res.status = false;
                return Ok(res);

            }

        }
    }
}
