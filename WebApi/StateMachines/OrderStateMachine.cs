using Domain.Order.Enums;
using Domain.Order.Exceptions;
using Domain.Order.Models;
using Domain.Order.Ports;
using Stateless;
using Stateless.Graph;
using System.Threading.Tasks;
using WebApi.Interfaces;

namespace WebApi.StateMachines
{
    public enum OrderTrigger
    {
        Approve,
        Delivery,
        Cancel
    }

    public class OrderStateMachine : IStateMachine<Order, OrderStatus, OrderTrigger>
    {
        private readonly IApproveOrderPort approveOrderPort;
        private readonly ICancelOrderPort cancelOrderPort;
        private readonly IDeliveryOrderPort deliveryOrderPort;

        private StateMachine<OrderStatus, OrderTrigger> stateMachine;
        private Order order;

        public OrderStateMachine(
            IApproveOrderPort approveOrderPort,
            ICancelOrderPort cancelOrderPort,
            IDeliveryOrderPort deliveryOrderPort)
        {
            this.approveOrderPort = approveOrderPort;
            this.cancelOrderPort = cancelOrderPort;
            this.deliveryOrderPort = deliveryOrderPort;
        }

        public StateMachine<OrderStatus, OrderTrigger> BuildFor(Order order)
        {
            this.order = order;

            return Configure();
        }

        private StateMachine<OrderStatus, OrderTrigger> Configure()
        {
            stateMachine = new StateMachine<OrderStatus, OrderTrigger>(order.Status);

            stateMachine.Configure(OrderStatus.New)
                .Permit(OrderTrigger.Approve, OrderStatus.Approved)
                .Permit(OrderTrigger.Cancel, OrderStatus.Canceled);

            stateMachine.Configure(OrderStatus.Approved)
                .Permit(OrderTrigger.Delivery, OrderStatus.Delivered)
                .Permit(OrderTrigger.Cancel, OrderStatus.Canceled);

            stateMachine.Configure(OrderStatus.Approved)
                .OnEntryAsync(ApproveOrderAsync);

            stateMachine.Configure(OrderStatus.Delivered)
                .OnEntryAsync(DeliveryOrderAsync);

            stateMachine.Configure(OrderStatus.Canceled)
                .OnEntryAsync(CancelOrderAsync);

            var graph = UmlDotGraph.Format(stateMachine.GetInfo());

            return stateMachine;
        }

        private async Task ApproveOrderAsync()
        {
            bool succedded = await approveOrderPort.ApproveOrderAsync(order.Number);
            if (!succedded)
            {
                throw new AdvanceOrderException($"Transition error for state {OrderStatus.Approved}");
            }
        }

        private async Task CancelOrderAsync()
        {
            bool succedded = await cancelOrderPort.CancelOrderAsync(order.Number);
            if (!succedded)
            {
                throw new AdvanceOrderException($"Transition error for state {OrderStatus.Canceled}");
            }
        }

        private async Task DeliveryOrderAsync()
        {
            bool succedded = await deliveryOrderPort.DeliveryOrderAsync(order.Number);
            if (!succedded)
            {
                throw new AdvanceOrderException($"Transition error for state {OrderStatus.Delivered}");
            }
        }
    }
}
