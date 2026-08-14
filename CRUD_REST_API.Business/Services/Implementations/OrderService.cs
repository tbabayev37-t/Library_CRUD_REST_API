using AutoMapper;
using CRUD_REST_API.Business.DTOs.OrderDto;
using CRUD_REST_API.Business.Services.Abstractions;
using CRUD_REST_API.Core.Models;
using CRUD_REST_API.DataAccess.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository, IMapper mapper, IBookRepository bookRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _bookRepository = bookRepository;
        }
        public async Task CreateAsync(OrderCreateDto orderCreateDto)
        {
            if (orderCreateDto == null || orderCreateDto.Items == null || orderCreateDto.Items.Count == 0)
                throw new ArgumentException("Sifarişdə ən azı bir kitab olmalıdır.");
            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                OrderItems = new List<OrderItem>()
            };
            decimal calculatedTotalPrice = 0;
            foreach (var itemDto in orderCreateDto.Items)
            {
                var book = await _bookRepository.GetByIdAsync(itemDto.BookId);
                if (book == null)
                    throw new KeyNotFoundException($"ID-si {itemDto.BookId} olan kitab tapilmadi.");
                decimal itemUnitPrice = book.Price ?? 10.0m;

                var orderItem = new OrderItem
                {
                    BookId = book.Id,
                    Quantity = itemDto.Quantity,
                    UnitPrice = itemUnitPrice,
                };
                calculatedTotalPrice += itemUnitPrice* itemDto.Quantity;
                order.OrderItems.Add(orderItem);
            }
            order.TotalPrice = calculatedTotalPrice;

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var deletedOrder = await _orderRepository.GetByIdAsync(id);
            if (deletedOrder == null) throw new KeyNotFoundException("Order is not found");
             _orderRepository.Delete(deletedOrder);
            await _orderRepository.SaveAsync();
        }

        public async Task<IEnumerable<OrderGetDto>> GetAllAsync()
        {
            var allOrder = await _orderRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable <OrderGetDto>>(allOrder);
        }

        public async Task<OrderGetDto> GetByIdAsync(int id)
        {
            var findingOrder = await _orderRepository.GetOrderWithDetailsAsync(id);
            if (findingOrder == null) throw new KeyNotFoundException("Order is not found");
            return _mapper.Map<OrderGetDto>(findingOrder);
        }

        public async Task UpdateAsync(OrderUpdateDto orderUpdateDto)
        {
            var updatedOrder = await _orderRepository.GetByIdAsync(orderUpdateDto.Id);
            if (updatedOrder == null) throw new KeyNotFoundException("Order is not found");
            _mapper.Map(orderUpdateDto, updatedOrder); 
            _orderRepository.Update(updatedOrder);
            await _orderRepository.SaveAsync();
        }
    }
}
