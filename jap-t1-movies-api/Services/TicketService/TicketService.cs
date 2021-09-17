using JAP_Task_1_MoviesApi.Data;
using JAP_Task_1_MoviesApi.DTO.Ticket;
using JAP_Task_1_MoviesApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.Services.TicketService
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TicketService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<bool>> BuyTickets(BuyTicketDto buyTicketDto)
        {
            var serviceResponse = new ServiceResponse<bool>();

            if (buyTicketDto.NumberOfTickets <= 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Data = false;
                serviceResponse.Message = "Number of tickets cannot be zero or negative!";
                return serviceResponse;
            }

            var userId = GetUserId();
            var screening = await _context.Screenings.FirstOrDefaultAsync(x => x.Id == buyTicketDto.ScreeningId);

            serviceResponse.Success = false;
            serviceResponse.Data = false;

            if (screening == null)
                serviceResponse.Message = "Screening does not exist!";
            else if (screening.ScreeningDate <= DateTime.Now)
                serviceResponse.Message = "Screening is in the past!";
            else
            {
                //await _context.SaveChangesAsync();

                await _context.Tickets
                    .AddAsync(new Ticket
                    {
                        ScreeningId = buyTicketDto.ScreeningId,
                        UserId = userId,
                        BoughtTickets = buyTicketDto.NumberOfTickets
                    });
                await _context.SaveChangesAsync();

                serviceResponse.Success = true;
                serviceResponse.Data = true;
                serviceResponse.Message = "Successfully bought tickets!";
            }

            return serviceResponse;
        }
    }
}
