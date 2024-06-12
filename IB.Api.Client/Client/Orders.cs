using System;
using IB.Api.Client.Model;
using IBApi;

namespace IB.Api.Client
{
    //Orders
    public partial class IBClient
    {
        public event EventHandler<CommissionUpdate> CommissionUpdateReceived;
        public event EventHandler<ExecutionUpdate> ExecutionUpdateReceived;
        public event EventHandler<OrderUpdate> OrderUpdateReceived;
        public event EventHandler<OpenOrderUpdate> OpenOrderUpdateReceived;
        public event EventHandler<OpenOrderUpdate> WhatIfOpenOrderUpdateReceived;
        public int NextOrderId { get; set; }
        
        public void RequestOrders()
        {
            ClientSocket.reqAllOpenOrders();
        }
        public void PlaceOrder(int orderId, Contract contract, Order order)
        {
            ClientSocket.PlaceOrder(orderId, contract, order);
        }
        public void WhatIf(int orderId, Contract contract, Order order)
        {
            order.WhatIf = true;
            ClientSocket.PlaceOrder(orderId, contract, order);
        }
        public void CancelOrder(int orderId)
        {
            Notify($"Cancel Order Id ({orderId}) requested");
            ClientSocket.CancelOrder(orderId, string.Empty);
        }
        public void CancellAllOrders()
        {
            ClientSocket.reqGlobalCancel();
        }
        public void RequestExecutions(int reqId)
        {
            ClientSocket.reqExecutions(reqId, new ExecutionFilter());
        }

        void IEWrapper.NextValidId(int orderId)
        {
            NextOrderId = orderId;
            Notify($"Next valid Order Id ({orderId})");
        }
        
        void IEWrapper.ExecDetails(int reqId, Contract contract, Execution execution)
        {
            ExecutionUpdateReceived?.Invoke(this, new ExecutionUpdate
            {
                Account = execution.AcctNumber,
                Symbol = contract.Symbol,
                SecType = contract.SecType,
                ExecutionId = execution.ExecId,
                OrderRef = execution.OrderRef,
                Side = execution.Side,
                AvgPrice = execution.AvgPrice
            });
        }
        void IEWrapper.ExecDetailsEnd(int reqId)
        {
            _ = string.Empty;
        }
        void IEWrapper.OpenOrderEnd()
        {
            _ = string.Empty;
        }
        void IEWrapper.OrderStatus(int orderId, string status, decimal filled, decimal remaining, double avgFillPrice,
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
        void IEWrapper.CommissionReport(CommissionReport commissionReport)
        {
            CommissionUpdateReceived?.Invoke(this, new CommissionUpdate
            {
                ExecutionId = commissionReport.ExecId,
                Commission = commissionReport.Commission
            });
        }
        void IEWrapper.OpenOrder(int orderId, Contract contract, Order order, OrderState orderState)
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
