using System;
using IB.Api.Client.IBKR;
using IB.Api.Client.Implementation.Model;

namespace IB.Api.Client.Implementation
{
    //Orders
    public partial class IbClient
    {
        public event EventHandler<CommissionUpdate> CommissionUpdateReceived;
        public event EventHandler<ExecutionUpdate> ExecutionUpdateReceived;
        public event EventHandler<OrderUpdate> OrderUpdateReceived;
        public event EventHandler<OpenOrderUpdate> OpenOrderUpdateReceived;
        public event EventHandler<OpenOrderUpdate> WhatIfOpenOrderUpdateReceived;
        public int NextOrderId { get; set; }
        
        public void ReqAllOpenOrders()
        {
            ClientSocket.reqAllOpenOrders();
        }
        public void PlaceOrder(int orderId, Contract contract, Order order)
        {
            ClientSocket.placeOrder(orderId, contract, order);
        }
        public void WhatIf(int orderId, Contract contract, Order order)
        {
            order.WhatIf = true;
            ClientSocket.placeOrder(orderId, contract, order);
        }
        public void CancelOrder(int orderId, OrderCancel orderCancel)
        {
            Notify($"Cancel Order Id ({orderId}) requested");
            ClientSocket.cancelOrder(orderId, orderCancel);
        }
        public void ReqGlobalCancel()
        {
            ClientSocket.reqGlobalCancel();
        }
        public void RequestExecutions(int reqId)
        {
            ClientSocket.reqExecutions(reqId, new ExecutionFilter());
        }

        void IEWrapper.nextValidId(int orderId)
        {
            NextOrderId = orderId;
            Notify($"Next valid Order Id ({orderId})");
        }
        
        void IEWrapper.execDetails(int reqId, Contract contract, Execution execution)
        {
            ExecutionUpdateReceived?.Invoke(this, new ExecutionUpdate
            {
                ReqId = reqId,
                Account = execution.AcctNumber,
                Symbol = contract.Symbol,
                SecType = contract.SecType,
                ExecutionId = execution.ExecId,
                OrderRef = execution.OrderRef,
                Side = execution.Side,
                AvgPrice = execution.AvgPrice
            });
        }
        void IEWrapper.execDetailsEnd(int reqId)
        {
            DiscardImplementation(reqId);
        }
        void IEWrapper.openOrderEnd()
        {
            DiscardImplementation();
        }
        void IEWrapper.orderStatus(int orderId, string status, decimal filled, decimal remaining, double avgFillPrice,
            int permId, int parentId, double lastFillPrice, int clientId, string whyHeld, double mktCapPrice)
        {
            var orderUpdate = new OrderUpdate
            {
                OrderId = orderId,
                Status = status,
                FilledAmount = filled,
                RemainingAmount = remaining,
                AvgFillPrice = avgFillPrice,
                PermId = permId,
                ParentId = parentId,
                LastFillPrice = lastFillPrice,
                ClientId = clientId,
                WhyHeld = whyHeld,
                MktCapPrice = mktCapPrice
            };
            OrderUpdateReceived?.Invoke(this, orderUpdate);
        }
        void IEWrapper.commissionReport(CommissionReport commissionReport)
        {
            CommissionUpdateReceived?.Invoke(this, new CommissionUpdate
            {
                ExecutionId = commissionReport.ExecId,
                Commission = commissionReport.Commission
            });
        }
        void IEWrapper.openOrder(int orderId, Contract contract, Order order, OrderState orderState)
        {
            if (order.WhatIf)
                WhatIfOpenOrderUpdateReceived?.Invoke(this, new OpenOrderUpdate
                {
                    OrderId = orderId,
                    Contract = contract,
                    Order = order,
                    OrderState = orderState
                });
            else
                OpenOrderUpdateReceived?.Invoke(this, new OpenOrderUpdate
                {
                    OrderId = orderId,
                    Contract = contract,
                    Order = order,
                    OrderState = orderState
                });
        }
    }
}
