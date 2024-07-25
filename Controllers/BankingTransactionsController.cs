using BankingTransactionAPI.Models;
using Microsoft.AspNetCore.Mvc;
using BankingTransactionAPI.Services;
using BankingTransactionAPI.Exceptions;

[ApiController]
[Route("api/[controller]")]
public class BankingTransactionsController : ControllerBase
{
    private readonly IUserService _userService;

    public BankingTransactionsController (IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("users")]
    public async Task<ActionResult<User>> CreateUser([FromBody] User user)
    {   
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            await _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while creating the user.", Details = ex.Message });
        }
        
    }

    [HttpGet("users/{id}")]
    public async Task<ActionResult<User>> GetUser(string id)
    {
        try
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }
        catch(InvalidAccountIdException ex)
        {
            return NotFound(new {ex.Message});
        }
        catch(Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while getting the user.", Details = ex.Message });
        }
    }

    [HttpPut("transfers")]
    public async Task<ActionResult<List<User>>> TransferFunds(
        [FromBody] Transaction transfer)
    {   
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            List<User> userList = await _userService.TransferFunds(transfer);
            return Ok(userList);
        }
        catch(InsufficientFundsException ex)
        {
            return BadRequest(new { ex.Message }); 
        }
        catch(InvalidAccountIdException ex)
        {
            return NotFound(new {ex.Message});
        }
        catch(Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while processing the transaction.", Details = ex.Message });
        }
    }

    [HttpGet("transfers/users/{userId}")]
    public async Task<ActionResult<List<Transaction>>> GetTransfersByUserId(string userId)
    {
        try
        {
            var transfers = await _userService.GetTransactionsByUserId(userId);

            if (transfers.Count == 0)
            {
                return Ok("User has no transactions."); 
            }
            return Ok(transfers);
        }
        catch(InvalidAccountIdException ex)
        {
            return NotFound(new {ex.Message}); 
        }
        catch(Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while getting the user's transactions.", Details = ex.Message });
        }
    }
}