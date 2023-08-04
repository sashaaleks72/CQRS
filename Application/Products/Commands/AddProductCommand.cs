using Application.Common.Exceptions;
using Application.Interfaces.Repositories;
using Application.Products.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.Products.Commands
{
    public record AddProductCommand(AddProductRequestDto Data) : IRequest<string>;

    public class AddProductHandler : IRequestHandler<AddProductCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IProductsRepository _repository;
        //private readonly AmazonSimpleEmailServiceClient _emailService;
        //private readonly IConfiguration _configuration;

        public AddProductHandler(IMapper mapper, IProductsRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<string> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<TeapotEntity>(request.Data);

            var isAdded = await _repository.AddProductAsync(productEntity);

            if (!isAdded) 
            {
                throw new HttpException("The product hasn't been added!", HttpStatusCode.BadRequest);
            }

            return "The product has been added!";
        }
    }
}

/*EMAIL SENDER*/
//var sender = _configuration["AWS:SES:EmailFrom"];

//var emailRequest = new SendEmailRequest
//{
//    Source = sender,
//    Destination = new Destination
//    {
//        ToAddresses = new List<string> { "oleksandr.tretiakov@nure.ua" }
//    },
//    Message = new Message
//    {
//        Body = new Body
//        {
//            Html = new Content
//            {
//                Charset = "UTF-8",
//                Data = "<!DOCTYPE html>" +
//                "<html>" +
//                    "<head></head>" +
//                    "<body>Hello</body>" +
//                "</html>"
//            },
//        },
//        Subject = new Content
//        {
//            Charset = "UTF-8",
//            Data = "Testing"
//        }
//    }
//};


//var response = await _emailService.SendEmailAsync(emailRequest);